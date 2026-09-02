// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;
using NUnit.Framework;
using Vortice.Direct3D12;
using Vortice.DXGI;
using static Vortice.DirectML.DML;
using static Vortice.DXGI.DXGI;

namespace Vortice.DirectML.Tests;

[TestFixture(TestOf = typeof(IDMLDevice))]
public class OperatorTests
{
    private IDXGIFactory4 _dxgiFactory = null!;
    private ID3D12Device2 _d3d12Device = null!;
    private ID3D12CommandQueue _commandQueue = null!;
    private ID3D12CommandAllocator _commandAllocator = null!;
    private ID3D12GraphicsCommandList4 _commandList = null!;
    private IDMLDevice _dmlDevice = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        if (!D3D12.IsSupported(Vortice.Direct3D.FeatureLevel.Level_12_0))
        {
            Assert.Ignore("Direct3D12 is not supported on current OS");
        }

        _dxgiFactory = CreateDXGIFactory2<IDXGIFactory4>(false);

        ID3D12Device2? device = default;

        for (uint adapterIndex = 0;
            _dxgiFactory.EnumAdapters1(adapterIndex, out IDXGIAdapter1? adapter).Success;
            adapterIndex++)
        {
            AdapterDescription1 desc = adapter.Description1;

            // Don't select the Basic Render Driver adapter.
            if ((desc.Flags & AdapterFlags.Software) != AdapterFlags.None)
            {
                adapter.Dispose();

                continue;
            }

            if (D3D12.D3D12CreateDevice(adapter, Vortice.Direct3D.FeatureLevel.Level_11_0, out device).Success)
            {
                adapter.Dispose();
                break;
            }
        }

        if (device == null)
        {
            // No hardware adapter; the software rasterizer still runs DirectML.
            using IDXGIAdapter warpAdapter = _dxgiFactory.EnumWarpAdapter<IDXGIAdapter>();
            if (D3D12.D3D12CreateDevice(warpAdapter, Vortice.Direct3D.FeatureLevel.Level_11_0, out device).Failure)
            {
                Assert.Ignore("Direct3D12 device could not be created");
            }
        }

        _d3d12Device = device!;
        _commandQueue = _d3d12Device.CreateCommandQueue(new CommandQueueDescription(CommandListType.Direct));
        _commandAllocator = _d3d12Device.CreateCommandAllocator(CommandListType.Direct);
        _commandList = _d3d12Device.CreateCommandList<ID3D12GraphicsCommandList4>(CommandListType.Direct, _commandAllocator);

        _dmlDevice = DMLCreateDevice(_d3d12Device, CreateDeviceFlags.None);
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _dmlDevice?.Dispose();
        _commandList?.Dispose();
        _commandAllocator?.Dispose();
        _commandQueue?.Dispose();
        _d3d12Device?.Dispose();
        _dxgiFactory?.Dispose();
    }

    [TestCase]
    public void ActivationGeluTest()
    {
        RequireFeatureLevel(FeatureLevel.Level5_1);

        BufferTensorDescription tensor = CreateTensor(1, 1, 1, 6);

        var description = new ActivationGeluOperatorDescription
        {
            InputTensor = tensor,
            OutputTensor = tensor,
        };

        float[] input = [-2.0f, -1.0f, -0.5f, 0.0f, 0.5f, 2.0f];
        float[] output = Dispatch(description, [(tensor, input)], tensor);

        // f(x) = 0.5 * x * (1 + erf(x / sqrt(2))), the exact form.
        float[] expected = [-0.04550026f, -0.15865525f, -0.15426877f, 0.0f, 0.34573123f, 1.95449974f];

        Assert.That(output, Is.EqualTo(expected).Within(1e-4f));
    }

    [TestCase]
    public void ActivationSoftmax1Test()
    {
        RequireFeatureLevel(FeatureLevel.Level5_1);

        BufferTensorDescription tensor = CreateTensor(1, 1, 2, 3);

        var description = new ActivationSoftmax1OperatorDescription
        {
            InputTensor = tensor,
            OutputTensor = tensor,
            Axes = [3],
        };

        float[] input = [1.0f, 2.0f, 3.0f, 0.5f, -0.5f, 0.0f];
        float[] output = Dispatch(description, [(tensor, input)], tensor);

        // Each row of three normalizes on its own: exp(x[i]) / sum(exp(x)).
        float[] expected = [0.09003057f, 0.24472847f, 0.66524096f, 0.50648039f, 0.18632372f, 0.30719589f];

        Assert.That(output, Is.EqualTo(expected).Within(1e-4f));
    }

    [TestCase]
    public void MultiheadAttentionTest()
    {
        RequireFeatureLevel(FeatureLevel.Level6_1);

        const int SequenceLength = 2;
        const int KeySequenceLength = 3;
        const int HeadCount = 2;
        const int HeadSize = 2;
        const int HiddenSize = HeadCount * HeadSize;
        float scale = 1.0f / MathF.Sqrt(HeadSize);

        BufferTensorDescription queryTensor = CreateTensor(1, SequenceLength, HiddenSize);
        BufferTensorDescription keyTensor = CreateTensor(1, KeySequenceLength, HiddenSize);
        BufferTensorDescription valueTensor = CreateTensor(1, KeySequenceLength, HiddenSize);
        BufferTensorDescription outputTensor = CreateTensor(1, SequenceLength, HiddenSize);

        var description = new MultiheadAttentionOperatorDescription
        {
            QueryTensor = queryTensor,
            KeyTensor = keyTensor,
            ValueTensor = valueTensor,
            OutputTensor = outputTensor,
            Scale = scale,
            HeadCount = HeadCount,
            MaskType = MultiheadAttentionMaskType.None,
        };

        float[] query = [0.1f, -0.2f, 0.3f, 0.4f, -0.5f, 0.6f, 0.7f, -0.8f];
        float[] key = [0.2f, 0.1f, -0.3f, 0.5f, 0.4f, -0.6f, 0.2f, 0.3f, -0.1f, 0.8f, -0.7f, 0.1f];
        float[] value = [1.0f, 2.0f, -1.0f, 0.5f, -0.5f, 1.5f, 2.0f, -2.0f, 0.25f, -0.75f, 1.25f, 0.0f];

        // The descriptor's inputs, in order: query, key and value, then the
        // three stacked forms, bias, mask, relative position bias and the past
        // key-value cache -- all left null here, bound as none.
        // The descriptor also declares two optional present key-value outputs.
        float[] output = Dispatch(description, [
            (queryTensor, query), (keyTensor, key), (valueTensor, value),
            null, null, null, null, null, null, null, null], outputTensor, outputCount: 3);

        float[] expected = AttentionReference(
            query, key, value, SequenceLength, KeySequenceLength, HeadCount, HeadSize, scale);

        Assert.That(output, Is.EqualTo(expected).Within(1e-4f));
    }

    [TestCase]
    public void DequantizeTest()
    {
        RequireFeatureLevel(FeatureLevel.Level6_3);

        // Two rows of eight int8 values, one scale per block of four.
        BufferTensorDescription inputTensor = CreateTensor(TensorDataType.Int8, 1, 1, 2, 8);
        BufferTensorDescription scaleTensor = CreateTensor(1, 1, 2, 2);
        BufferTensorDescription outputTensor = CreateTensor(1, 1, 2, 8);

        var description = new DequantizeOperatorDescription
        {
            InputTensor = inputTensor,
            QuantizationType = QuantizationType.Scale,
            QuantizationTensors = [scaleTensor],
            OutputTensor = outputTensor,
        };

        sbyte[] input = [-128, -1, 0, 1, 2, 3, 4, 127, 10, 20, 30, 40, -10, -20, -30, -40];
        float[] scales = [0.5f, 2.0f, 0.1f, 1.0f];

        // The harness uploads floats; the int8 values ride along packed four to a float.
        float[] packedInput = MemoryMarshal.Cast<sbyte, float>(input).ToArray();
        float[] output = Dispatch(description, [(inputTensor, packedInput), (scaleTensor, scales)], outputTensor);

        var expected = new float[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            expected[i] = input[i] * scales[i / 4];
        }

        Assert.That(output, Is.EqualTo(expected).Within(1e-6f));
    }

    /// <summary>
    /// softmax(query x transposed(key) * scale) x value, per head, in doubles.
    /// </summary>
    private static float[] AttentionReference(
        float[] query, float[] key, float[] value,
        int sequenceLength, int keySequenceLength, int headCount, int headSize, float scale)
    {
        int hiddenSize = headCount * headSize;
        float[] output = new float[sequenceLength * hiddenSize];

        for (int head = 0; head < headCount; head++)
        {
            for (int q = 0; q < sequenceLength; q++)
            {
                var scores = new double[keySequenceLength];
                for (int k = 0; k < keySequenceLength; k++)
                {
                    double score = 0.0;
                    for (int d = 0; d < headSize; d++)
                    {
                        score += query[q * hiddenSize + head * headSize + d] * key[k * hiddenSize + head * headSize + d];
                    }
                    scores[k] = score * scale;
                }

                double sum = scores.Sum(Math.Exp);

                for (int d = 0; d < headSize; d++)
                {
                    double attended = 0.0;
                    for (int k = 0; k < keySequenceLength; k++)
                    {
                        attended += Math.Exp(scores[k]) / sum * value[k * hiddenSize + head * headSize + d];
                    }
                    output[q * hiddenSize + head * headSize + d] = (float)attended;
                }
            }
        }

        return output;
    }

    private void RequireFeatureLevel(FeatureLevel featureLevel)
    {
        if (_dmlDevice.HighestFeatureLevel < featureLevel)
        {
            Assert.Ignore($"Device does not support {featureLevel}");
        }
    }

    private static BufferTensorDescription CreateTensor(params uint[] sizes) =>
        CreateTensor(TensorDataType.Float32, sizes);

    private static BufferTensorDescription CreateTensor(TensorDataType dataType, params uint[] sizes)
    {
        var tensor = new BufferTensorDescription
        {
            DataType = dataType,
            Sizes = sizes,
            Flags = TensorFlags.None,
        };
        tensor.TotalTensorSizeInBytes = tensor.CalculateMinimumImpliedSize();
        return tensor;
    }

    /// <summary>
    /// Compile <paramref name="description"/>, dispatch it once over
    /// <paramref name="inputs"/> -- one entry per input tensor of the operator,
    /// null for optional tensors left out of the description -- and read the
    /// output back. Everything a dispatch needs beyond that (initialization,
    /// descriptors, temporary memory, upload and readback) lives here.
    /// </summary>
    private float[] Dispatch(
        OperatorDescription description,
        (BufferTensorDescription Tensor, float[] Data)?[] inputs,
        BufferTensorDescription outputTensor,
        int outputCount = 1)
    {
        using IDMLOperator dmlOperator = _dmlDevice.CreateOperator(description);
        using IDMLCompiledOperator compiledOperator = _dmlDevice.CompileOperator(dmlOperator, ExecutionFlags.None);
        using IDMLOperatorInitializer initializer = _dmlDevice.CreateOperatorInitializer([compiledOperator]);

        BindingProperties initializeProperties = initializer.GetBindingProperties();
        BindingProperties executeProperties = compiledOperator.GetBindingProperties();

        var heapDescription = new DescriptorHeapDescription
        {
            Type = DescriptorHeapType.ConstantBufferViewShaderResourceViewUnorderedAccessView,
            DescriptorCount = Math.Max(initializeProperties.RequiredDescriptorCount, executeProperties.RequiredDescriptorCount),
            Flags = DescriptorHeapFlags.ShaderVisible,
        };
        using ID3D12DescriptorHeap descriptorHeap = _d3d12Device.CreateDescriptorHeap(heapDescription);
        _commandList.SetDescriptorHeaps(1, [descriptorHeap]);

        var bindingTableDescription = new BindingTableDescription
        {
            Dispatchable = initializer,
            CPUDescriptorHandle = descriptorHeap.GetCPUDescriptorHandleForHeapStart(),
            GPUDescriptorHandle = descriptorHeap.GetGPUDescriptorHandleForHeapStart(),
            SizeInDescriptors = heapDescription.DescriptorCount,
        };
        using IDMLBindingTable bindingTable = _dmlDevice.CreateBindingTable(bindingTableDescription);

        ulong temporarySize = Math.Max(initializeProperties.TemporaryResourceSize, executeProperties.TemporaryResourceSize);
        ID3D12Resource? temporaryBuffer = null;
        ID3D12Resource? persistentBuffer = null;
        var buffers = new List<ID3D12Resource>();

        try
        {
            if (temporarySize != 0)
            {
                temporaryBuffer = _d3d12Device.CreateCommittedResource(
                    HeapProperties.DefaultHeapProperties, HeapFlags.None,
                    ResourceDescription.Buffer(temporarySize, ResourceFlags.AllowUnorderedAccess), ResourceStates.Common);

                if (initializeProperties.TemporaryResourceSize != 0)
                {
                    bindingTable.BindTemporaryResource(new BufferBinding { Buffer = temporaryBuffer, SizeInBytes = temporarySize });
                }
            }

            if (executeProperties.PersistentResourceSize != 0)
            {
                persistentBuffer = _d3d12Device.CreateCommittedResource(
                    HeapProperties.DefaultHeapProperties, HeapFlags.None,
                    ResourceDescription.Buffer(executeProperties.PersistentResourceSize, ResourceFlags.AllowUnorderedAccess), ResourceStates.Common);

                // The initializer writes the persistent resource, so it binds as its output.
                bindingTable.BindOutputs(new BindingDescription(
                    new BufferBinding { Buffer = persistentBuffer, SizeInBytes = executeProperties.PersistentResourceSize }));
            }

            using IDMLCommandRecorder recorder = _dmlDevice.CreateCommandRecorder();
            recorder.RecordDispatch(_commandList, initializer, bindingTable);
            CloseExecuteResetWait();

            _commandList.SetDescriptorHeaps(1, [descriptorHeap]);

            bindingTableDescription.Dispatchable = compiledOperator;
            bindingTable.Reset(bindingTableDescription);

            if (executeProperties.TemporaryResourceSize != 0)
            {
                bindingTable.BindTemporaryResource(new BufferBinding { Buffer = temporaryBuffer, SizeInBytes = temporarySize });
            }

            if (persistentBuffer != null)
            {
                bindingTable.BindPersistentResource(new BufferBinding { Buffer = persistentBuffer, SizeInBytes = executeProperties.PersistentResourceSize });
            }

            var inputBindings = new BindingDescription[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                // A default BindingDescription is DML_BINDING_TYPE_NONE, which
                // is what an optional tensor left null in the description wants.
                if (inputs[i] == null)
                {
                    continue;
                }

                (BufferTensorDescription tensor, float[] data) = inputs[i]!.Value;

                ID3D12Resource uploadBuffer = _d3d12Device.CreateCommittedResource(
                    HeapProperties.UploadHeapProperties, HeapFlags.None,
                    ResourceDescription.Buffer(tensor.TotalTensorSizeInBytes), ResourceStates.GenericRead);
                buffers.Add(uploadBuffer);
                uploadBuffer.SetData(data);

                ID3D12Resource inputBuffer = _d3d12Device.CreateCommittedResource(
                    HeapProperties.DefaultHeapProperties, HeapFlags.None,
                    ResourceDescription.Buffer(tensor.TotalTensorSizeInBytes, ResourceFlags.AllowUnorderedAccess), ResourceStates.CopyDest);
                buffers.Add(inputBuffer);

                _commandList.CopyResource(inputBuffer, uploadBuffer);
                _commandList.ResourceBarrierTransition(inputBuffer, ResourceStates.CopyDest, ResourceStates.UnorderedAccess);

                inputBindings[i] = new BindingDescription(
                    new BufferBinding { Buffer = inputBuffer, SizeInBytes = tensor.TotalTensorSizeInBytes });
            }
            bindingTable.BindInputs(inputBindings);

            using ID3D12Resource outputBuffer = _d3d12Device.CreateCommittedResource(
                HeapProperties.DefaultHeapProperties, HeapFlags.None,
                ResourceDescription.Buffer(outputTensor.TotalTensorSizeInBytes, ResourceFlags.AllowUnorderedAccess), ResourceStates.UnorderedAccess);

            // Any optional output tensor beyond the first is left null in the
            // description, so it binds as none.
            var outputBindings = new BindingDescription[outputCount];
            outputBindings[0] = new BindingDescription(
                new BufferBinding { Buffer = outputBuffer, SizeInBytes = outputTensor.TotalTensorSizeInBytes });
            bindingTable.BindOutputs(outputBindings);

            recorder.RecordDispatch(_commandList, compiledOperator, bindingTable);

            using ID3D12Resource readbackBuffer = _d3d12Device.CreateCommittedResource(
                HeapProperties.ReadbackHeapProperties, HeapFlags.None,
                ResourceDescription.Buffer(outputTensor.TotalTensorSizeInBytes), ResourceStates.CopyDest);

            _commandList.ResourceBarrierTransition(outputBuffer, ResourceStates.UnorderedAccess, ResourceStates.CopySource);
            _commandList.CopyResource(readbackBuffer, outputBuffer);
            CloseExecuteResetWait();

            float[] output = new float[outputTensor.TotalTensorSizeInBytes / sizeof(float)];
            unsafe
            {
                float* outputData = readbackBuffer.Map<float>(0);
                for (int i = 0; i < output.Length; i++)
                {
                    output[i] = outputData[i];
                }
                readbackBuffer.Unmap(0);
            }

            return output;
        }
        finally
        {
            foreach (ID3D12Resource buffer in buffers)
            {
                buffer.Dispose();
            }

            temporaryBuffer?.Dispose();
            persistentBuffer?.Dispose();
        }
    }

    private void CloseExecuteResetWait()
    {
        _commandList.Close();
        _commandQueue.ExecuteCommandList(_commandList);

        using ID3D12Fence fence = _d3d12Device.CreateFence();
        using var waitHandle = new AutoResetEvent(false);
        fence.SetEventOnCompletion(1, waitHandle);
        _commandQueue.Signal(fence, 1);
        waitHandle.WaitOne();

        _commandAllocator.Reset();
        _commandList.Reset(_commandAllocator);
    }
}
