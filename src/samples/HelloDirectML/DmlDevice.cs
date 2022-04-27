// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;
using Vortice.Direct3D12.Debug;
using Vortice.DirectML;
using Vortice.DXGI;

namespace HelloDirectML;
public class DmlDevice : IDisposable
{
    public IDXGIFactory4 DXGIFactory;
    public ID3D12Device2 D3D12Device;
    public ID3D12CommandQueue D3D12CommandQueue; 
    public ID3D12CommandAllocator D3D12CommandAllocator;
    public ID3D12GraphicsCommandList4 D3D12CommandList;

    public IDMLDevice DMLDevice;

    public bool IsSupported() => D3D12.IsSupported(Vortice.Direct3D.FeatureLevel.Level_12_0);

    public void InitializeDirect3D12()
    {
        if (!IsSupported())
        {
            throw new InvalidOperationException("Direct3D12 is not supported on current OS");
        }

        var validation = false;

        if (D3D12.D3D12GetDebugInterface(out ID3D12Debug? debug).Success)
        {
            debug!.EnableDebugLayer();
            debug!.Dispose();
            validation = true;
        }

        DXGIFactory = DXGI.CreateDXGIFactory2<IDXGIFactory4>(validation);

        for (int adapterIndex = 0; DXGIFactory.EnumAdapters1(adapterIndex, out IDXGIAdapter1 adapter).Success; adapterIndex++)
        {
            AdapterDescription1 desc = adapter.Description1;

            // Don't select the Basic Render Driver adapter.
            if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
            {
                adapter.Dispose();

                continue;
            }

            if (D3D12.D3D12CreateDevice(adapter, Vortice.Direct3D.FeatureLevel.Level_11_0, out D3D12Device).Success)
            {
                adapter.Dispose();

                break;
            }
        }

        if (D3D12Device == null)
        {
            throw new InvalidOperationException("Direct3D12 device could not be created");
        }

        var commandQueueDesc = new CommandQueueDescription
        {
            Type = CommandListType.Direct,
            Flags = CommandQueueFlags.None,
        };

        D3D12CommandQueue = D3D12Device.CreateCommandQueue(commandQueueDesc);
        D3D12CommandAllocator = D3D12Device.CreateCommandAllocator(CommandListType.Direct);
        D3D12CommandList = D3D12Device.CreateCommandList<ID3D12GraphicsCommandList4>(CommandListType.Direct, D3D12CommandAllocator);
    }

    public void Run()
    {
        InitializeDirect3D12();

        var createFlags = CreateDeviceFlags.None;

#if DEBUG
        createFlags |= CreateDeviceFlags.Debug;
#endif

        DML.DMLCreateDevice(D3D12Device, createFlags, out DMLDevice);

        DMLDevice.SetName("hello world");

        var tensorSizes = new int[] { 1, 2, 3, 4 };
        var tensorElementCount = tensorSizes.Aggregate((a, b) => a * b);

        var bufferTensorDesc = new BufferTensorDescription()
        {
            DataType = TensorDataType.Float32,
            Sizes = tensorSizes,
            Flags = TensorFlags.None,
        };

        bufferTensorDesc.TotalTensorSizeInBytes = Dmlx.CalculateBufferTensorSize(
            bufferTensorDesc.DataType,
            bufferTensorDesc.DimensionCount,
            bufferTensorDesc.Sizes,
            bufferTensorDesc.Strides
            );

        // Create DirectML operator(s). Operators represent abstract functions such as "multiply", "reduce", "convolution", or even
        // compound operations such as recurrent neural nets. This example creates an instance of the Identity operator,
        // which applies the function f(x) = x for all elements in a tensor.
        var identityOperatorDesc = new ElementWiseIdentityOperatorDescription
        {
            InputTensor = bufferTensorDesc,
            OutputTensor = bufferTensorDesc,
        };

        // Like Direct3D 12, these DESC structs don't need to be long-lived. This means, for example, that it's safe to place
        // the DML_OPERATOR_DESC (and all the subobjects it points to) on the stack, since they're no longer needed after
        // CreateOperator returns.
        using var dmlOperator = DMLDevice.CreateOperator(identityOperatorDesc);

        // Compile the operator into an object that can be dispatched to the GPU. In this step, DirectML performs operator
        // fusion and just-in-time (JIT) compilation of shader bytecode, then compiles it into a Direct3D 12 pipeline state object (PSO).
        // The resulting compiled operator is a baked, optimized form of an operator suitable for execution on the GPU.
        using var dmlCompiledOperator = DMLDevice.CompileOperator(dmlOperator, ExecutionFlags.None);

        // 24 elements * 4 == 96 bytes.
        long tensorBufferSize = bufferTensorDesc.TotalTensorSizeInBytes;


        using var dmlOperatorInitializer = DMLDevice.CreateOperatorInitializer(new IDMLCompiledOperator[] { dmlCompiledOperator });

        // Query the operator for the required size (in descriptors) of its binding table.
        // You need to initialize an operator exactly once before it can be executed, and
        // the two stages require different numbers of descriptors for binding. For simplicity,
        // we create a single descriptor heap that's large enough to satisfy them both.
        var initializeBindingProperties = dmlOperatorInitializer.GetBindingProperties();
        var executeBindingProperties = dmlCompiledOperator.GetBindingProperties();

        var descriptorCount = Math.Max(initializeBindingProperties.RequiredDescriptorCount, executeBindingProperties.RequiredDescriptorCount);

        // Create descriptor heaps.
        var descriptorHeapDesc = new DescriptorHeapDescription
        {
            Type = DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView,
            DescriptorCount = descriptorCount,
            Flags = DescriptorHeapFlags.ShaderVisible,
        };
        using var descriptorHeap = D3D12Device.CreateDescriptorHeap(descriptorHeapDesc);

        // Set the descriptor heap(s).
        D3D12CommandList.SetDescriptorHeaps(1, new ID3D12DescriptorHeap[] { descriptorHeap });

        // Create a binding table over the descriptor heap we just created.
        var bindingTableDesc = new BindingTableDescription
        {
            Dispatchable = dmlOperatorInitializer,
            CPUDescriptorHandle = descriptorHeap.GetCPUDescriptorHandleForHeapStart(),
            GPUDescriptorHandle = descriptorHeap.GetGPUDescriptorHandleForHeapStart(),
            SizeInDescriptors = descriptorCount,
        };
        using var dmlBindingTable = DMLDevice.CreateBindingTable(bindingTableDesc);


        // Create the temporary and persistent resources that are necessary for executing an operator.

        // The temporary resource is scratch memory (used internally by DirectML), whose contents you don't need to define.
        // The persistent resource is long-lived, and you need to initialize it using the IDMLOperatorInitializer.
        var temporaryResourceSize = Math.Max(initializeBindingProperties.TemporaryResourceSize, executeBindingProperties.TemporaryResourceSize);
        var persistentResourceSize = executeBindingProperties.PersistentResourceSize;

        ID3D12Resource? temporaryBuffer = null;
        if (temporaryResourceSize != 0)
        {
            temporaryBuffer = D3D12Device.CreateCommittedResource(HeapProperties.DefaultHeapProperties, HeapFlags.None, ResourceDescription.Buffer(persistentResourceSize, ResourceFlags.AllowUnorderedAccess), ResourceStates.Common);

            if (initializeBindingProperties.TemporaryResourceSize != 0)
            {
                var bufferBinding = new BufferBinding
                {
                    Buffer = temporaryBuffer,
                    Offset = 0,
                    SizeInBytes = temporaryResourceSize,
                };
                dmlBindingTable.BindTemporaryResource(bufferBinding);
            }
        }


        // Bind and initialize the operator on the GPU.
        ID3D12Resource? persistentBuffer = null;
        if (persistentResourceSize != 0)
        {
            persistentBuffer = D3D12Device.CreateCommittedResource(HeapProperties.DefaultHeapProperties, HeapFlags.None, ResourceDescription.Buffer(persistentResourceSize), ResourceStates.Common);

            // The persistent resource should be bound as the output to the IDMLOperatorInitializer.
            var bufferBinding = new BufferBinding
            {
                Buffer = persistentBuffer,
                Offset = 0,
                SizeInBytes = persistentResourceSize,
            };
            dmlBindingTable.BindOutputs(bufferBinding);
        }

        // The command recorder is a stateless object that records Dispatches into an existing Direct3D 12 command list.
        using var dmlCommandRecorder = DMLDevice.CreateCommandRecorder();

        // Record execution of the operator initializer.
        dmlCommandRecorder.RecordDispatch(D3D12CommandList, dmlOperatorInitializer, dmlBindingTable);

        // Close the Direct3D 12 command list, and submit it for execution as you would any other command list. You could
        // in principle record the execution into the same command list as the initialization, but you need only to Initialize
        // once, and typically you want to Execute an operator more frequently than that.
        CloseExecuteResetWait();

        // 
        // Bind and execute the operator on the GPU.
        //

        D3D12CommandList.SetDescriptorHeaps(1, new[] { descriptorHeap });

        // Reset the binding table to bind for the operator we want to execute (it was previously used to bind for the
        // initializer).

        bindingTableDesc.Dispatchable = dmlCompiledOperator;

        dmlBindingTable.Reset(bindingTableDesc);

        if (temporaryResourceSize != 0)
        {
            var bufferBinding = new BufferBinding
            {
                Buffer = temporaryBuffer,
                Offset = 0,
                SizeInBytes = temporaryResourceSize,
            };
            dmlBindingTable.BindTemporaryResource(bufferBinding);
        }

        if (persistentResourceSize != 0)
        {
            var bufferBinding = new BufferBinding
            {
                Buffer = persistentBuffer,
                Offset = 0,
                SizeInBytes = persistentResourceSize,
            };
            dmlBindingTable.BindPersistentResource(bufferBinding);
        }

        // Create tensor buffers for upload/input/output/readback of the tensor elements.

        using ID3D12Resource uploadBuffer = D3D12Device.CreateCommittedResource(
            HeapProperties.UploadHeapProperties, HeapFlags.None, ResourceDescription.Buffer((ulong)tensorBufferSize), ResourceStates.GenericRead);

        using ID3D12Resource inputBuffer = D3D12Device.CreateCommittedResource(
            HeapProperties.DefaultHeapProperties, HeapFlags.None, ResourceDescription.Buffer((ulong)tensorBufferSize, ResourceFlags.AllowUnorderedAccess), ResourceStates.CopyDest);

        var random = new Random();

        float[] inputTensorElementArray = new float[tensorElementCount];
        unsafe
        {
            for (int i = 0; i < inputTensorElementArray.Length; i++)
            {
                inputTensorElementArray[i] = (float)(random.NextDouble() * 10);
            }

            Console.WriteLine(" Input: " + string.Join(", ", inputTensorElementArray.Select(x => x.ToString("0.00"))));

            uploadBuffer.SetData(inputTensorElementArray);

            D3D12CommandList.CopyResource(inputBuffer, uploadBuffer);
            D3D12CommandList.ResourceBarrierTransition(inputBuffer, ResourceStates.CopyDest, ResourceStates.UnorderedAccess);
        }

        var inputBufferBinding = new BufferBinding
        {
            Buffer = inputBuffer,
            Offset = 0,
            SizeInBytes = (ulong)tensorBufferSize,
        };
        dmlBindingTable.BindInputs(inputBufferBinding);

        using ID3D12Resource outputBuffer = D3D12Device.CreateCommittedResource(
            HeapProperties.DefaultHeapProperties, HeapFlags.None, ResourceDescription.Buffer((ulong)tensorBufferSize, ResourceFlags.AllowUnorderedAccess), ResourceStates.UnorderedAccess);

        var outputBufferBinding = new BufferBinding
        {
            Buffer = outputBuffer,
            Offset = 0,
            SizeInBytes = (ulong)tensorBufferSize,
        };
        dmlBindingTable.BindOutputs(outputBufferBinding);

        // Record execution of the compiled operator.
        dmlCommandRecorder.RecordDispatch(D3D12CommandList, dmlCompiledOperator, dmlBindingTable);

        CloseExecuteResetWait();

        // The output buffer now contains the result of the identity operator,
        // so read it back if you want the CPU to access it.

        using ID3D12Resource readbackBuffer = D3D12Device.CreateCommittedResource(
            HeapProperties.ReadbackHeapProperties, HeapFlags.None, ResourceDescription.Buffer((ulong)tensorBufferSize), ResourceStates.CopyDest);

        D3D12CommandList.ResourceBarrierTransition(outputBuffer, ResourceStates.UnorderedAccess, ResourceStates.CopySource);
        D3D12CommandList.CopyResource(readbackBuffer, outputBuffer);

        CloseExecuteResetWait();

        unsafe
        {
            float* outputBufferData = readbackBuffer.Map<float>(0);
            float[] outputTensorElementArray = new float[tensorElementCount];

            for (int i = 0; i < outputTensorElementArray.Length; i++)
            {
                outputTensorElementArray[i] = outputBufferData[i];
            }

            readbackBuffer.Unmap(0);

            Console.WriteLine("Output: " + string.Join(", ", outputTensorElementArray.Select(x => x.ToString("0.00"))));
        }

        temporaryBuffer?.Dispose();
        persistentBuffer?.Dispose();
    }

    public void CloseExecuteResetWait()
    {
        D3D12CommandList.Close();

        D3D12CommandQueue.ExecuteCommandList(D3D12CommandList);

        using var fence = D3D12Device.CreateFence();
        var waitHandle = new AutoResetEvent(false);

        fence.SetEventOnCompletion(1, waitHandle);

        D3D12CommandQueue.Signal(fence, 1);

        waitHandle.WaitOne();

        D3D12CommandAllocator.Reset();
        D3D12CommandList.Reset(D3D12CommandAllocator);

    }

    public void Dispose()
    {
        D3D12CommandList.Dispose();
        D3D12CommandAllocator.Dispose();
        D3D12CommandQueue.Dispose();

        DMLDevice.Dispose();
        D3D12Device.Dispose();
        DXGIFactory.Dispose();
    }
}
