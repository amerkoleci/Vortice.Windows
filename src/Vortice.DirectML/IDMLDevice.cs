// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.DirectML;

public partial class IDMLDevice
{
    /// <summary>
    /// Gets the highest supported feature level.
    /// </summary>
    public FeatureLevel HighestFeatureLevel => CheckFeatureLevelsSupport(Enum.GetValues<FeatureLevel>());

    /// <summary>
    /// Query for the feature levels supported by the device
    /// </summary>
    /// <param name="requestedFeatureLevels">An array of feature levels to query support for. The highest feature level in this array that is supported by the device is returned.</param>
    /// <returns>The highest feature level of the provided feature levels that is supported by this device.</returns>
    public unsafe FeatureLevel CheckFeatureLevelsSupport(params FeatureLevel[] requestedFeatureLevels)
    {
        fixed (FeatureLevel* featureLevels = requestedFeatureLevels)
        {
            var query = new FeatureQueryFeatureLevels()
            {
                RequestedFeatureLevelCount = (uint)requestedFeatureLevels.Length,
                RequestedFeatureLevels = new(featureLevels),
            };
            var data = new FeatureDataFeatureLevels();

            CheckFeatureSupport(Feature.FeatureLevels, (uint)sizeof(FeatureQueryFeatureLevels), new(&query), (uint)sizeof(FeatureDataFeatureLevels), new(&data)).CheckError();

            return data.MaxSupportedFeatureLevel;
        }
    }

    /// <summary>
    /// Query a DirectML device for its support for a particular data type within tensors
    /// </summary>
    /// <param name="dataType">The data type about which you're querying for support.</param>
    /// <returns>TRUE if the tensor data type is supported within tensors by the DirectML device. Otherwise, FALSE.</returns>
    public unsafe bool CheckTensorDataTypeSupport(TensorDataType dataType)
    {
        FeatureQueryTensorDataTypeSupport query = new()
        {
            DataType = dataType
        };
        FeatureDataTensorDataTypeSupport data = new();

        CheckFeatureSupport(Feature.TensorDataTypeSupport, (uint)sizeof(FeatureQueryTensorDataTypeSupport), new(&query), (uint)sizeof(FeatureDataTensorDataTypeSupport), new(&data));

        return data.IsSupported;
    }

    /// <summary>
    /// <para>Compiles an operator into an object that can be dispatched to the GPU.</para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// A compiled operator represents the efficient, baked form of an operator suitable for
    /// execution on the GPU. A compiled operator holds state (such as shaders and other objects)
    /// required for execution. Because a compiled operator implements the
    /// <see cref="IDMLPageable"/> interface, you're able to evict one from GPU memory if you wish.
    /// </para>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-compileoperator"/>
    /// </para>
    /// </remarks>
    /// <param name="operator"></param>
    /// <param name="executionFlags"></param>
    /// <returns></returns>
    public IDMLCompiledOperator CompileOperator(IDMLOperator @operator, ExecutionFlags executionFlags)
    {
        CompileOperator(@operator, executionFlags, typeof(IDMLCompiledOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCompiledOperator(nativePtr);
    }

    /// <summary>
    /// Creates a binding table, which is an object that can be used to bind resources (such as
    /// tensors) to the pipeline.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The binding table wraps a range of an application-managed descriptor heap using the
    /// provided descriptor handles and count. Binding tables are used by DirectML to manage the
    /// binding of resources by writing descriptors into the descriptor heap at the offset
    /// specified by the CPUDescriptorHandle, and binding those descriptors to the pipeline using
    /// the descriptors at the offset specified by the GPUDescriptorHandle. The order in which
    /// DirectML writes descriptors into the heap is unspecified, so your application must take
    /// care not to overwrite the descriptors wrapped by the binding table.
    /// </para>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-createbindingtable"/>
    /// </para>
    /// </remarks>
    /// <param name="description"></param>
    /// <returns></returns>
    public IDMLBindingTable CreateBindingTable(in BindingTableDescription description)
    {
        CreateBindingTable(description, typeof(IDMLBindingTable).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLBindingTable(nativePtr);
    }

    /// <summary>
    /// Creates a DirectML command recorder.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-createcommandrecorder"/>
    /// </para>
    /// </remarks>
    /// <returns></returns>
    public IDMLCommandRecorder CreateCommandRecorder()
    {
        CreateCommandRecorder(typeof(IDMLCommandRecorder).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCommandRecorder(nativePtr);
    }

    /// <summary>
    /// Creates a DirectML operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-createoperator"/>
    /// </para>
    /// </remarks>
    /// <param name="operatorDescription">The description of the operator to be created.</param>
    /// <returns></returns>
    public IDMLOperator CreateOperator(OperatorDescription operatorDescription)
    {
        CreateOperator(ref operatorDescription, typeof(IDMLOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLOperator(nativePtr);
    }

    /// <summary>
    /// Creates an object that can be used to initialize compiled operators.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-createoperatorinitializer"/>
    /// </para>
    /// </remarks>
    /// <param name="operators">
    /// An optional pointer to a constant array of IDMLCompiledOperator pointers containing the set
    /// of operators that this initializer will target. Upon execution of the initializer, the
    /// target operators become initialized. This array may be null or empty, indicating that the
    /// initializer has no target operators.
    /// </param>
    /// <returns></returns>
    public IDMLOperatorInitializer CreateOperatorInitializer(params IDMLCompiledOperator[]? operators)
    {
        CreateOperatorInitializer((uint)(operators?.Length ?? 0), operators, typeof(IDMLOperatorInitializer).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLOperatorInitializer(nativePtr);
    }

    /// <summary>
    /// Evicts one or more pageable objects from GPU memory. Also see
    /// <see cref="MakeResident(IDMLPageable[])"/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-evict"/>
    /// </para>
    /// </remarks>
    /// <param name="objects">The pageable objects to evict from GPU memory.</param>
    public unsafe void Evict(params IDMLPageable[] objects)
    {
        IntPtr* pObjects = stackalloc IntPtr[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            pObjects[i] = objects[i].NativePointer;
        }
        Evict((uint)objects.Length, new IntPtr(pObjects));
    }

    /// <summary>
    /// Causes one or more pageable objects to become resident in GPU memory. Also see
    /// <see cref="Evict(IDMLPageable[])"/>
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-makeresident"/>
    /// </para>
    /// </remarks>
    /// <param name="objects">The pageable objects to make resident in GPU memory.</param>
    public unsafe void MakeResident(params IDMLPageable[] objects)
    {
        IntPtr* pObjects = stackalloc IntPtr[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            pObjects[i] = objects[i].NativePointer;
        }
        MakeResident((uint)objects.Length, new IntPtr(pObjects));
    }

    /// <summary>
    /// Retrieves the Direct3D 12 device that was used to create this DirectML device.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-getparentdevice"/>
    /// </para>
    /// </remarks>
    /// <returns></returns>
    public ID3D12Device GetParentDevice()
    {
        GetParentDevice(typeof(ID3D12Device).GUID, out var pDevice).CheckError();
        return new ID3D12Device(pDevice);
    }
}
