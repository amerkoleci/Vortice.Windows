// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vortice.Direct3D12;
using Vortice.Direct3D12.Debug;
using Vortice.DXGI;
using Vortice.DirectML;

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

        var bufferTensorDesc = new BufferTensorDescription(tensorSizes)
        {
            DataType = TensorDataType.Float32,
            Flags = TensorFlags.None,
        };

        bufferTensorDesc.TotalTensorSizeInBytes = Dmlx.CalculateBufferTensorSize(
            bufferTensorDesc.DataType,
            bufferTensorDesc.DimensionCount,
            bufferTensorDesc.Sizes,
            bufferTensorDesc.Strides
            );


        var identityOperatorDesc = new ElementWiseIdentityOperatorDescription(bufferTensorDesc, bufferTensorDesc);

        var dmlOperator = DMLDevice.CreateOperator(identityOperatorDesc);
        var dmlCompiledOperator = DMLDevice.CompileOperator(dmlOperator, ExecutionFlags.None);

        var dmlOperatorInitializer = DMLDevice.CreateOperatorInitializer(new IDMLCompiledOperator[] { dmlCompiledOperator });

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
        var descriptorHeap = D3D12Device.CreateDescriptorHeap(descriptorHeapDesc);

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
        var dmlBindingTable = DMLDevice.CreateBindingTable(bindingTableDesc);


        // Create the temporary and persistent resources that are necessary for executing an operator.

        // The temporary resource is scratch memory (used internally by DirectML), whose contents you don't need to define.
        // The persistent resource is long-lived, and you need to initialize it using the IDMLOperatorInitializer.
        var temporaryResourceSize = Math.Max(initializeBindingProperties.TemporaryResourceSize, executeBindingProperties.TemporaryResourceSize);
        var persistentResourceSize = executeBindingProperties.PersistentResourceSize;

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
            //dmlBindingTable.BindOutputs()
        }


    }

    public static long CalculateBufferTensorSize(TensorDataType dataType, int dimensionCount, int[] sizes, int[]? strides)
    {
        return 0;
    }

    public void Dispose()
    {

    }
}
