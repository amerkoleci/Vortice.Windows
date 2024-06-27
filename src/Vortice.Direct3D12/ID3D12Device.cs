// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime.Win32;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12Device
{
    private const int GENERIC_ALL = 0x10000000;
    private RootSignatureVersion? _highestRootSignatureVersion;

    public long AdapterLuid
    {
        get
        {
            long result;
            ((delegate* unmanaged[Stdcall]<IntPtr, long*, void>)this[GetAdapterLuid__vtbl_index])(NativePointer, &result);
            return result;
        }
    }

    public RootSignatureVersion HighestRootSignatureVersion
    {
        get
        {
            if (!_highestRootSignatureVersion.HasValue)
            {
                var featureData = new FeatureDataRootSignature
                {
                    HighestVersion = RootSignatureVersion.Version11
                };

                if (CheckFeatureSupport(Feature.RootSignature, &featureData, sizeof(FeatureDataRootSignature)).Failure)
                {
                    _highestRootSignatureVersion = RootSignatureVersion.Version11;
                }
                else
                {
                    _highestRootSignatureVersion = featureData.HighestVersion;
                }
            }

            return _highestRootSignatureVersion.Value;
        }
    }

    public T CheckFeatureSupport<T>() where T : unmanaged
    {
        Feature feature = default(T) switch
        {
            FeatureDataD3D12Options => Feature.Options,
            FeatureDataArchitecture => Feature.Architecture,
            FeatureDataFeatureLevels => Feature.FeatureLevels,
            FeatureDataFormatSupport => Feature.FormatSupport,
            FeatureDataMultisampleQualityLevels => Feature.MultisampleQualityLevels,
            FeatureDataFormatInfo => Feature.FormatInfo,
            FeatureDataGpuVirtualAddressSupport => Feature.GpuVirtualAddressSupport,
            FeatureDataShaderModel => Feature.ShaderModel,
            FeatureDataD3D12Options1 => Feature.Options1,
            FeatureDataProtectedResourceSessionSupport => Feature.ProtectedResourceSessionSupport,
            FeatureDataRootSignature => Feature.RootSignature,
            FeatureDataArchitecture1 => Feature.Architecture1,
            FeatureDataD3D12Options2 => Feature.Options2,
            FeatureDataShaderCache => Feature.ShaderCache,
            FeatureDataCommandQueuePriority => Feature.CommandQueuePriority,
            FeatureDataD3D12Options3 => Feature.Options3,
            FeatureDataExistingHeaps => Feature.ExistingHeaps,
            FeatureDataD3D12Options4 => Feature.Options4,
            FeatureDataSerialization => Feature.Serialization,
            FeatureDataCrossNode => Feature.CrossNode,
            FeatureDataD3D12Options5 => Feature.Options5,
            FeatureDataDisplayable => Feature.Displayable,
            FeatureDataD3D12Options6 => Feature.Options6,
            FeatureDataQueryMetaCommand => Feature.QueryMetaCommand,
            FeatureDataD3D12Options7 => Feature.Options7,
            FeatureDataProtectedResourceSessionTypeCount => Feature.ProtectedResourceSessionTypeCount,
            FeatureDataProtectedResourceSessionTypes => Feature.ProtectedResourceSessionTypes,
            FeatureDataPredication => Feature.Predication,
            FeatureDataPlacedResourceSupportInfo => Feature.PlacedResourceSupportInfo,
            FeatureDataHardwareCopy => Feature.HardwareCopy,
            FeatureDataD3D12Options8 => Feature.Options8,
            FeatureDataD3D12Options9 => Feature.Options9,
            FeatureDataD3D12Options10 => Feature.Options10,
            FeatureDataD3D12Options11 => Feature.Options11,
            FeatureDataD3D12Options12 => Feature.Options12,
            FeatureDataD3D12Options13 => Feature.Options13,
            FeatureDataD3D12Options14 => Feature.Options14,
            FeatureDataD3D12Options15 => Feature.Options15,
            FeatureDataD3D12Options16 => Feature.Options16,
            FeatureDataD3D12Options17 => Feature.Options17,
            FeatureDataD3D12Options18 => Feature.Options18,
            FeatureDataD3D12Options19 => Feature.Options19,
            FeatureDataD3D12Options20 => Feature.Options20,
            FeatureDataD3D12Options21 => Feature.Options21,
            _ => throw new ArgumentException(nameof(T)),
        };

        return CheckFeatureSupport<T>(feature);
    }

    public T CheckFeatureSupport<T>(Feature feature) where T : unmanaged
    {
        T featureSupport = default;
        CheckFeatureSupport(feature, &featureSupport, sizeof(T));
        return featureSupport;
    }

    public bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
    {
        fixed (T* featureSupportPtr = &featureSupport)
        {
            return CheckFeatureSupport(feature, featureSupportPtr, sizeof(T)).Success;
        }
    }

    public FeatureLevel CheckMaxSupportedFeatureLevel() => CheckMaxSupportedFeatureLevel(D3D12.FeatureLevels);

    public FeatureLevel CheckMaxSupportedFeatureLevel(FeatureLevel[] featureLevels)
    {
        fixed (FeatureLevel* levelsPtr = featureLevels)
        {
            var featureData = new FeatureDataFeatureLevels
            {
                NumFeatureLevels = featureLevels.Length,
                FeatureLevelsRequested = new IntPtr(levelsPtr),
                MaxSupportedFeatureLevel = FeatureLevel.Level_11_0
            };

            if (CheckFeatureSupport(Feature.FeatureLevels, &featureData, sizeof(FeatureDataFeatureLevels)).Success)
            {
                return featureData.MaxSupportedFeatureLevel;
            }

            return FeatureLevel.Level_11_0;
        }
    }

    public FeatureDataD3D12Options Options
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options>(Feature.Options);
    }
    public FeatureDataD3D12Options1 Options1
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options1>(Feature.Options1);
    }
    public FeatureDataD3D12Options2 Options2
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options2>(Feature.Options2);
    }
    public FeatureDataD3D12Options3 Options3
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options3>(Feature.Options3);
    }
    public FeatureDataD3D12Options4 Options4
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options4>(Feature.Options4);
    }
    public FeatureDataD3D12Options5 Options5
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options5>(Feature.Options5);
    }

    public FeatureDataD3D12Options6 Options6
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options6>(Feature.Options6);
    }
    public FeatureDataD3D12Options7 Options7
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options7>(Feature.Options7);
    }
    public FeatureDataD3D12Options8 Options8
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options8>(Feature.Options8);
    }
    public FeatureDataD3D12Options9 Options9
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options9>(Feature.Options9);
    }
    public FeatureDataD3D12Options10 Options10
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options10>(Feature.Options10);
    }
    public FeatureDataD3D12Options11 Options11
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options11>(Feature.Options11);
    }
    public FeatureDataD3D12Options12 Options12
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options12>(Feature.Options12);
    }
    public FeatureDataD3D12Options13 Options13
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options13>(Feature.Options13);
    }
    public FeatureDataD3D12Options14 Options14
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options14>(Feature.Options14);
    }
    public FeatureDataD3D12Options15 Options15
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options15>(Feature.Options15);
    }
    public FeatureDataD3D12Options16 Options16
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options16>(Feature.Options16);
    }
    public FeatureDataD3D12Options17 Options17
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options17>(Feature.Options17);
    }
    public FeatureDataD3D12Options18 Options18
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options18>(Feature.Options18);
    }
    public FeatureDataD3D12Options19 Options19
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options19>(Feature.Options19);
    }
    public FeatureDataD3D12Options20 Options20
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options20>(Feature.Options20);
    }
    public FeatureDataD3D12Options21 Options21
    {
        get => CheckFeatureSupport<FeatureDataD3D12Options21>(Feature.Options21);
    }
    public FeatureDataArchitecture Architecture
    {
        get => CheckFeatureSupport<FeatureDataArchitecture>(Feature.Architecture);
    }

    public FeatureDataGpuVirtualAddressSupport GpuVirtualAddressSupport
    {
        get => CheckFeatureSupport<FeatureDataGpuVirtualAddressSupport>(Feature.GpuVirtualAddressSupport);
    }

    public FeatureDataProtectedResourceSessionSupport ProtectedResourceSessionSupport
    {
        get => CheckFeatureSupport<FeatureDataProtectedResourceSessionSupport>(Feature.ProtectedResourceSessionSupport);
    }

    public FeatureDataArchitecture1 Architecture1
    {
        get => CheckFeatureSupport<FeatureDataArchitecture1>(Feature.Architecture1);
    }

    public FeatureDataShaderCache ShaderCache
    {
        get => CheckFeatureSupport<FeatureDataShaderCache>(Feature.ShaderCache);
    }

    public FeatureDataExistingHeaps ExistingHeaps
    {
        get => CheckFeatureSupport<FeatureDataExistingHeaps>(Feature.ExistingHeaps);
    }

    public FeatureDataSerialization Serialization
    {
        get => CheckFeatureSupport<FeatureDataSerialization>(Feature.Serialization);
    }

    public FeatureDataCrossNode CrossNode
    {
        get => CheckFeatureSupport<FeatureDataCrossNode>(Feature.CrossNode);
    }

    public ShaderModel CheckHighestShaderModel(ShaderModel highestShaderModel)
    {
        var featureData = new FeatureDataShaderModel
        {
            HighestShaderModel = highestShaderModel
        };

        if (CheckFeatureSupport(Feature.ShaderModel, &featureData, sizeof(FeatureDataShaderModel)).Success)
        {
            return featureData.HighestShaderModel;
        }

        return ShaderModel.Model5_1;
    }

    public RootSignatureVersion CheckHighestRootSignatureVersion(RootSignatureVersion highestVersion)
    {
        var featureData = new FeatureDataRootSignature
        {
            HighestVersion = highestVersion
        };

        if (CheckFeatureSupport(Feature.RootSignature, &featureData, sizeof(FeatureDataRootSignature)).Success)
        {
            return featureData.HighestVersion;
        }

        return RootSignatureVersion.Version10;
    }

    public bool CheckFormatSupport(Format format, out FormatSupport1 formatSupport1, out FormatSupport2 formatSupport2)
    {
        FeatureDataFormatSupport featureData = new()
        {
            Format = format
        };

        if (CheckFeatureSupport(Feature.FormatSupport, &featureData, sizeof(FeatureDataFormatSupport)).Failure)
        {
            formatSupport1 = FormatSupport1.None;
            formatSupport2 = FormatSupport2.None;
            return false;
        }

        formatSupport1 = featureData.Support1;
        formatSupport2 = featureData.Support2;
        return true;
    }

    public int CheckMultisampleQualityLevels(Format format, int sampleCount, MultisampleQualityLevelFlags flags = MultisampleQualityLevelFlags.None)
    {
        FeatureDataMultisampleQualityLevels featureData = new()
        {
            Format = format,
            SampleCount = sampleCount,
            Flags = flags
        };

        if (CheckFeatureSupport(Feature.MultisampleQualityLevels, &featureData, sizeof(FeatureDataMultisampleQualityLevels)).Failure)
        {
            return 0;
        }

        return featureData.NumQualityLevels;
    }

    public byte GetFormatPlaneCount(Format format)
    {
        var featureData = new FeatureDataFormatInfo
        {
            Format = format
        };

        if (CheckFeatureSupport(Feature.FormatInfo, &featureData, sizeof(FeatureDataFormatInfo)).Failure)
        {
            return 0;
        }

        return featureData.PlaneCount;
    }

    public FeatureDataCommandQueuePriority CheckCommandQueuePriority(CommandListType commandListType)
    {
        FeatureDataCommandQueuePriority featureData = new()
        {
            CommandListType = commandListType,
        };

        if (CheckFeatureSupport(Feature.CommandQueuePriority, &featureData, sizeof(FeatureDataFormatInfo)).Failure)
        {
            return default;
        }

        return featureData;
    }

    #region CreateCommittedResource
    public ID3D12Resource CreateCommittedResource(HeapType heapType,
        HeapFlags heapFlags,
        ResourceDescription description,
        ResourceStates initialResourceState,
        ClearValue? optimizedClearValue = null)
    {
        HeapProperties heapProperties = new(heapType);
        CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            optimizedClearValue,
            typeof(ID3D12Resource).GUID,
            out IntPtr nativePtr).CheckError();

        return new(nativePtr);
    }

    public ID3D12Resource CreateCommittedResource(HeapType heapType,
        ResourceDescription description,
        ResourceStates initialResourceState,
        ClearValue? optimizedClearValue = null)
    {
        HeapProperties heapProperties = new(heapType);
        CreateCommittedResource(
            ref heapProperties,
            HeapFlags.None,
            ref description,
            initialResourceState,
            optimizedClearValue,
            typeof(ID3D12Resource).GUID,
            out IntPtr nativePtr).CheckError();

        return new(nativePtr);
    }

    public ID3D12Resource CreateCommittedResource(HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription description,
        ResourceStates initialResourceState,
        ClearValue? optimizedClearValue = null)
    {
        CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            optimizedClearValue,
            typeof(ID3D12Resource).GUID,
            out IntPtr nativePtr).CheckError();

        return new(nativePtr);
    }

    public Result CreateCommittedResource(HeapProperties heapProperties, HeapFlags heapFlags, ResourceDescription description, ResourceStates initialResourceState, out ID3D12Resource? resource)
    {
        Result result = CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            null,
            typeof(ID3D12Resource).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = new ID3D12Resource(nativePtr);
        return result;
    }

    public Result CreateCommittedResource(HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription description,
        ResourceStates initialResourceState,
        ClearValue? optimizedClearValue,
        out ID3D12Resource? resource)
    {
        Result result = CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            optimizedClearValue,
            typeof(ID3D12Resource).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = new ID3D12Resource(nativePtr);
        return result;
    }

    public T CreateCommittedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription description,
        ResourceStates initialResourceState,
        ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            optimizedClearValue,
            typeof(T).GUID,
            out IntPtr nativePtr).CheckError();

        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateCommittedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(HeapProperties heapProperties, HeapFlags heapFlags, ResourceDescription description, ResourceStates initialResourceState, out T? resource) where T : ID3D12Resource
    {
        Result result = CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            null,
            typeof(T).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public Result CreateCommittedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription description,
        ResourceStates initialResourceState,
        ClearValue? optimizedClearValue,
        out T? resource) where T : ID3D12Resource
    {
        Result result = CreateCommittedResource(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            optimizedClearValue,
            typeof(T).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateCommandQueue
    public Result CreateCommandQueue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in CommandQueueDescription description, out T? commandQueue) where T : ID3D12CommandQueue
    {
        Result result = CreateCommandQueue(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandQueue = default;
            return result;
        }

        commandQueue = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T CreateCommandQueue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in CommandQueueDescription description) where T : ID3D12CommandQueue
    {
        CreateCommandQueue(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateCommandQueue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandListType type, int priority = 0, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0) where T : ID3D12CommandQueue
    {
        return CreateCommandQueue<T>(new CommandQueueDescription(type, priority, flags, nodeMask));
    }

    public T CreateCommandQueue<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandListType type, CommandQueuePriority priority, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0) where T : ID3D12CommandQueue
    {
        return CreateCommandQueue<T>(new CommandQueueDescription(type, priority, flags, nodeMask));
    }

    public Result CreateCommandQueue(in CommandQueueDescription description, out ID3D12CommandQueue? commandQueue)
    {
        Result result = CreateCommandQueue(description, typeof(ID3D12CommandQueue).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandQueue = default;
            return result;
        }

        commandQueue = new(nativePtr);
        return result;
    }

    public ID3D12CommandQueue CreateCommandQueue(in CommandQueueDescription description)
    {
        CreateCommandQueue(description, typeof(ID3D12CommandQueue).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12CommandQueue(nativePtr);
    }

    public ID3D12CommandQueue CreateCommandQueue(CommandListType type, int priority = 0, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0)
    {
        return CreateCommandQueue(new CommandQueueDescription(type, priority, flags, nodeMask));
    }

    public ID3D12CommandQueue CreateCommandQueue(CommandListType type, CommandQueuePriority priority, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0)
    {
        return CreateCommandQueue(new CommandQueueDescription(type, priority, flags, nodeMask));
    }
    #endregion

    #region CreateDescriptorHeap
    public ID3D12DescriptorHeap CreateDescriptorHeap(in DescriptorHeapDescription description)
    {
        CreateDescriptorHeap(description, typeof(ID3D12DescriptorHeap).GUID, out IntPtr nativePtr).CheckError();

        return new ID3D12DescriptorHeap(nativePtr);
    }

    public Result CreateDescriptorHeap(in DescriptorHeapDescription description, out ID3D12DescriptorHeap? descriptorHeap)
    {
        Result result = CreateDescriptorHeap(description, typeof(ID3D12DescriptorHeap).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            descriptorHeap = default;
            return result;
        }

        descriptorHeap = new ID3D12DescriptorHeap(nativePtr);
        return result;
    }

    public T CreateDescriptorHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in DescriptorHeapDescription description) where T : ID3D12DescriptorHeap
    {
        CreateDescriptorHeap(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDescriptorHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in DescriptorHeapDescription description, out T? descriptorHeap) where T : ID3D12DescriptorHeap
    {
        Result result = CreateDescriptorHeap(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            descriptorHeap = default;
            return result;
        }

        descriptorHeap = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateCommandAllocator
    public ID3D12CommandAllocator CreateCommandAllocator(CommandListType type)
    {
        CreateCommandAllocator(type, typeof(ID3D12CommandAllocator).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12CommandAllocator(nativePtr);
    }

    public Result CreateCommandAllocator(CommandListType type, out ID3D12CommandAllocator? commandAllocator)
    {
        Result result = CreateCommandAllocator(type, typeof(ID3D12CommandAllocator).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandAllocator = default;
            return result;
        }

        commandAllocator = new ID3D12CommandAllocator(nativePtr);
        return result;
    }

    public T CreateCommandAllocator<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandListType type) where T : ID3D12CommandAllocator
    {
        CreateCommandAllocator(type, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateCommandAllocator<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandListType type, out T? commandAllocator) where T : ID3D12CommandAllocator
    {
        Result result = CreateCommandAllocator(type, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandAllocator = default;
            return result;
        }

        commandAllocator = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateCommandList
    public T CreateCommandList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState? initialState = default) where T : ID3D12CommandList
    {
        CreateCommandList(0, type, commandAllocator, initialState, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateCommandList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState? initialState = default) where T : ID3D12CommandList
    {
        CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateCommandList<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState initialState, out T? commandList) where T : ID3D12CommandList
    {
        Result result = CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandList = default;
            return result;
        }

        commandList = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateFence
    public ID3D12Fence CreateFence(ulong initialValue = 0, FenceFlags flags = FenceFlags.None)
    {
        CreateFence(initialValue, flags, typeof(ID3D12Fence).GUID, out IntPtr nativePtr).CheckError();

        return new ID3D12Fence(nativePtr);
    }

    public Result CreateFence(ulong initialValue, FenceFlags flags, out ID3D12Fence? fence)
    {
        Result result = CreateFence(initialValue, flags, typeof(ID3D12Fence).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            fence = default;
            return result;
        }

        fence = new ID3D12Fence(nativePtr);
        return result;
    }

    public T CreateFence<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ulong initialValue = 0, FenceFlags flags = FenceFlags.None) where T : ID3D12Fence
    {
        CreateFence(initialValue, flags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateFence<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ulong initialValue, FenceFlags flags, out T? fence) where T : ID3D12Fence
    {
        Result result = CreateFence(initialValue, flags, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            fence = default;
            return result;
        }

        fence = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateHeap
    public T CreateHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in HeapDescription description) where T : ID3D12Heap
    {
        CreateHeap(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in HeapDescription description, out T? heap) where T : ID3D12Heap
    {
        Result result = CreateHeap(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            heap = default;
            return result;
        }

        heap = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateRootSignature
    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, IntPtr blobWithRootSignature, PointerSize blobLengthInBytes, out T? rootSignature) where T : ID3D12RootSignature
    {
        Result result = CreateRootSignature(nodeMask, blobWithRootSignature, blobLengthInBytes, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            rootSignature = default;
            return default;
        }

        rootSignature = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, Blob blob, out T? rootSignature) where T : ID3D12RootSignature
    {
        return CreateRootSignature(nodeMask, blob.BufferPointer, blob.BufferSize, out rootSignature);
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, byte[] blobWithRootSignature, out T? rootSignature) where T : ID3D12RootSignature
    {
        fixed (void* pBuffer = blobWithRootSignature)
        {
            return CreateRootSignature(nodeMask, (IntPtr)pBuffer, blobWithRootSignature.Length, out rootSignature);
        }
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, IntPtr blobWithRootSignature, PointerSize blobLengthInBytes) where T : ID3D12RootSignature
    {
        CreateRootSignature(nodeMask, blobWithRootSignature, blobLengthInBytes, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, Blob blob) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(nodeMask, blob.BufferPointer, blob.BufferSize);
    }

    public unsafe T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, byte[] blobWithRootSignature) where T : ID3D12RootSignature
    {
        fixed (void* pBuffer = blobWithRootSignature)
        {
            return CreateRootSignature<T>(nodeMask, (IntPtr)pBuffer, blobWithRootSignature.Length);
        }
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr blobWithRootSignature, PointerSize blobLengthInBytes) where T : ID3D12RootSignature
    {
        CreateRootSignature(0, blobWithRootSignature, blobLengthInBytes, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Blob blob) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, blob.BufferPointer, blob.BufferSize);
    }

    public unsafe T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(byte[] blobWithRootSignature) where T : ID3D12RootSignature
    {
        fixed (void* pBuffer = blobWithRootSignature)
        {
            return CreateRootSignature<T>(0, (IntPtr)pBuffer, blobWithRootSignature.Length);
        }
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in RootSignatureDescription description, RootSignatureVersion version) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, description, version);
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, in RootSignatureDescription description, RootSignatureVersion version) where T : ID3D12RootSignature
    {
        Result result = D3D12.D3D12SerializeRootSignature(description, version, out Blob blob, out Blob errorBlob);
        if (result.Failure)
        {
            if (errorBlob != null)
            {
                throw new SharpGenException(result, errorBlob.AsString());
            }

            throw new SharpGenException(result);
        }

        try
        {
            CreateRootSignature(nodeMask, blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }
        finally
        {
            errorBlob?.Dispose();
            blob.Dispose();
        }
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, in RootSignatureDescription description, RootSignatureVersion version, out T? rootSignature) where T : ID3D12RootSignature
    {
        Result result = D3D12.D3D12SerializeRootSignature(description, version, out Blob blob, out Blob errorBlob);
        if (result.Failure)
        {
            errorBlob?.Dispose();
            rootSignature = default;
            return result;
        }

        try
        {
            result = CreateRootSignature(nodeMask, blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                rootSignature = default;
                return default;
            }

            rootSignature = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }
        finally
        {
            errorBlob?.Dispose();
            blob.Dispose();
        }
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in RootSignatureDescription1 description) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, new VersionedRootSignatureDescription(description));
    }


    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, in RootSignatureDescription1 description) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(nodeMask, new VersionedRootSignatureDescription(description));
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in VersionedRootSignatureDescription description) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, description);
    }

    public T CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, in VersionedRootSignatureDescription description) where T : ID3D12RootSignature
    {
        CreateRootSignature(nodeMask, description, out T? rootSignature).CheckError();
        return rootSignature!;
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in RootSignatureDescription1 description, out T? rootSignature) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, new VersionedRootSignatureDescription(description), out rootSignature);
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, in RootSignatureDescription1 description, out T? rootSignature) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, new VersionedRootSignatureDescription(description), out rootSignature);
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in VersionedRootSignatureDescription description, out T? rootSignature) where T : ID3D12RootSignature
    {
        return CreateRootSignature<T>(0, description, out rootSignature);
    }

    public ID3D12RootSignature CreateRootSignature(in RootSignatureDescription1 description)
    {
        return CreateRootSignature(0, new VersionedRootSignatureDescription(description));
    }

    public ID3D12RootSignature CreateRootSignature(int nodeMask, in VersionedRootSignatureDescription description)
    {
        CreateRootSignature(nodeMask, description, out ID3D12RootSignature? rootSignature).CheckError();
        return rootSignature!;
    }

    public Result CreateRootSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(int nodeMask, in VersionedRootSignatureDescription description, out T? rootSignature) where T : ID3D12RootSignature
    {
        Result result = Result.Ok;
        Blob? signature = null;
        Blob? errorBlob = null;

        // D3DX12SerializeVersionedRootSignature
        switch (HighestRootSignatureVersion)
        {
            case RootSignatureVersion.Version10:
                switch (description.Version)
                {
                    case RootSignatureVersion.Version10:
                        result = D3D12.D3D12SerializeRootSignature(description.Description_1_0, RootSignatureVersion.Version1, out signature, out errorBlob);
                        break;

                    case RootSignatureVersion.Version11:
                        // Convert to version 1.0.
                        RootSignatureDescription1 desc_1_1 = description.Description_1_1!;
                        RootParameter[]? parameters_1_0 = null;

                        if (desc_1_1?.Parameters?.Length > 0)
                        {
                            parameters_1_0 = new RootParameter[desc_1_1.Parameters.Length];
                            for (int i = 0; i < parameters_1_0.Length; i++)
                            {
                                ShaderVisibility parameterShaderVisibility = desc_1_1.Parameters[i].ShaderVisibility;

                                switch (desc_1_1.Parameters[i].ParameterType)
                                {
                                    case RootParameterType.Constant32Bits:
                                        parameters_1_0[i] = new RootParameter(desc_1_1.Parameters[i].Constants, parameterShaderVisibility);
                                        break;

                                    case RootParameterType.ConstantBufferView:
                                    case RootParameterType.ShaderResourceView:
                                    case RootParameterType.UnorderedAccessView:
                                        parameters_1_0[i] = new RootParameter(desc_1_1.Parameters[i].ParameterType,
                                            new RootDescriptor(desc_1_1.Parameters[i].Descriptor.ShaderRegister, desc_1_1.Parameters[i].Descriptor.RegisterSpace),
                                            desc_1_1.Parameters[i].ShaderVisibility
                                            );
                                        break;

                                    case RootParameterType.DescriptorTable:
                                        RootDescriptorTable1 table_1_1 = desc_1_1.Parameters[i]!.DescriptorTable;
                                        DescriptorRange[] ranges = new DescriptorRange[table_1_1.Ranges?.Length ?? 0];

                                        for (int x = 0; x < ranges.Length; x++)
                                        {
                                            ranges[x] = new DescriptorRange(table_1_1.Ranges![x]);
                                        }

                                        parameters_1_0[i] = new RootParameter(new RootDescriptorTable(ranges), parameterShaderVisibility);
                                        break;
                                }
                            }
                        }

                        var desc_1_0 = new RootSignatureDescription(desc_1_1!.Flags, parameters_1_0, desc_1_1.StaticSamplers);
                        result = D3D12.D3D12SerializeRootSignature(desc_1_0, RootSignatureVersion.Version1, out signature, out errorBlob);
                        break;
                }
                break;

            case RootSignatureVersion.Version11:
                result = D3D12.D3D12SerializeVersionedRootSignature(description, out signature, out errorBlob);
                break;
        }

        if (result.Failure || signature == null)
        {
            errorBlob?.Dispose();

            rootSignature = default;
            return result;
        }

        try
        {
            result = CreateRootSignature(nodeMask, signature.BufferPointer, signature.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                rootSignature = default;
                return default;
            }

            rootSignature = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }
        finally
        {
            errorBlob?.Dispose();
            signature.Dispose();
        }
    }
    #endregion

    #region CreateCommandSignature
    public T CreateCommandSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandSignatureDescription description, ID3D12RootSignature? rootSignature) where T : ID3D12CommandSignature
    {
        CreateCommandSignature(description, rootSignature, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateCommandSignature<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(CommandSignatureDescription description, ID3D12RootSignature? rootSignature, out T? commandSignature) where T : ID3D12CommandSignature
    {
        Result result = CreateCommandSignature(description, rootSignature, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandSignature = default;
            return result;
        }

        commandSignature = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateComputePipelineState
    public T CreateComputePipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ComputePipelineStateDescription description) where T : ID3D12PipelineState
    {
        CreateComputePipelineState(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateComputePipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ComputePipelineStateDescription description, out T? pipelineState) where T : ID3D12PipelineState
    {
        Result result = CreateComputePipelineState(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineState = default;
            return result;
        }

        pipelineState = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public ID3D12PipelineState CreateComputePipelineState(ComputePipelineStateDescription description)
    {
        CreateComputePipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12PipelineState(nativePtr);
    }

    public Result CreateComputePipelineState(ComputePipelineStateDescription description, out ID3D12PipelineState? pipelineState)
    {
        Result result = CreateComputePipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineState = default;
            return result;
        }

        pipelineState = new ID3D12PipelineState(nativePtr);
        return result;
    }
    #endregion

    #region CreateGraphicsPipelineState
    public T CreateGraphicsPipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(GraphicsPipelineStateDescription description) where T : ID3D12PipelineState
    {
        CreateGraphicsPipelineState(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateGraphicsPipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(GraphicsPipelineStateDescription description, out T? pipelineState) where T : ID3D12PipelineState
    {
        Result result = CreateGraphicsPipelineState(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineState = default;
            return result;
        }

        pipelineState = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public ID3D12PipelineState CreateGraphicsPipelineState(GraphicsPipelineStateDescription description)
    {
        CreateGraphicsPipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12PipelineState(nativePtr);
    }

    public Result CreateGraphicsPipelineState(GraphicsPipelineStateDescription description, out ID3D12PipelineState? pipelineState)
    {
        Result result = CreateGraphicsPipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineState = default;
            return result;
        }

        pipelineState = new ID3D12PipelineState(nativePtr);
        return result;
    }
    #endregion

    #region CreateQueryHeap
    public T CreateQueryHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(QueryHeapDescription description) where T : ID3D12QueryHeap
    {
        CreateQueryHeap(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateQueryHeap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in QueryHeapDescription description, out T? queryHeap) where T : ID3D12QueryHeap
    {
        Result result = CreateQueryHeap(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            queryHeap = default;
            return result;
        }

        queryHeap = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreatePlacedResource
    public T CreatePlacedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        ID3D12Heap heap,
        ulong heapOffset,
        ResourceDescription resourceDescription,
        ResourceStates initialState,
        ClearValue? clearValue = null) where T : ID3D12Resource
    {
        CreatePlacedResource(heap, heapOffset, ref resourceDescription, initialState, clearValue, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreatePlacedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        ID3D12Heap heap,
        ulong heapOffset,
        ResourceDescription resourceDescription,
        ResourceStates initialState,
        out T? resource) where T : ID3D12Resource
    {
        Result result = CreatePlacedResource(heap, heapOffset, ref resourceDescription, initialState, null, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public Result CreatePlacedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        ID3D12Heap heap,
        ulong heapOffset,
        ResourceDescription resourceDescription,
        ResourceStates initialState,
        in ClearValue clearValue,
        out T? resource) where T : ID3D12Resource
    {
        Result result = CreatePlacedResource(heap, heapOffset, ref resourceDescription, initialState, clearValue, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CreateReservedResource
    public T CreateReservedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ResourceDescription resourceDescription, ResourceStates initialState, ClearValue? clearValue = null) where T : ID3D12Resource
    {
        CreateReservedResource(ref resourceDescription, initialState, clearValue, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateReservedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ResourceDescription resourceDescription, ResourceStates initialState, out T? resource) where T : ID3D12Resource
    {
        Result result = CreateReservedResource(ref resourceDescription, initialState, null, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public Result CreateReservedResource<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ResourceDescription resourceDescription, ResourceStates initialState, in ClearValue clearValue, out T? resource) where T : ID3D12Resource
    {
        Result result = CreateReservedResource(ref resourceDescription, initialState, clearValue, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
    #endregion

    #region CopyDescriptors

    public unsafe void CopyDescriptors(
        int numDestDescriptorRanges,
        CpuDescriptorHandle[] destDescriptorRangeStarts,
        int[] destDescriptorRangeSizes,
        int numSrcDescriptorRanges,
        CpuDescriptorHandle[] srcDescriptorRangeStarts,
        int[] srcDescriptorRangeSizes,
        DescriptorHeapType descriptorHeapsType)
    {
        fixed (CpuDescriptorHandle* pDestDescriptorRangeStarts = destDescriptorRangeStarts)
        fixed (int* pDestDescriptorRangeSizes = destDescriptorRangeSizes)
        fixed (CpuDescriptorHandle* pSrcDescriptorRangeStarts = srcDescriptorRangeStarts)
        fixed (int* pSrcDescriptorRangeSizes = srcDescriptorRangeSizes)
            CopyDescriptors(numDestDescriptorRanges, pDestDescriptorRangeStarts, pDestDescriptorRangeSizes,
                numSrcDescriptorRanges, pSrcDescriptorRangeStarts, pSrcDescriptorRangeSizes,
                descriptorHeapsType);
    }

    public unsafe void CopyDescriptors(
        int numDestDescriptorRanges,
        ReadOnlySpan<CpuDescriptorHandle> destDescriptorRangeStarts,
        ReadOnlySpan<int> destDescriptorRangeSizes,
        int numSrcDescriptorRanges,
        ReadOnlySpan<CpuDescriptorHandle> srcDescriptorRangeStarts,
        ReadOnlySpan<int> srcDescriptorRangeSizes,
        DescriptorHeapType descriptorHeapsType)
    {
        fixed (CpuDescriptorHandle* pDestDescriptorRangeStarts = destDescriptorRangeStarts)
        fixed (int* pDestDescriptorRangeSizes = destDescriptorRangeSizes)
        fixed (CpuDescriptorHandle* pSrcDescriptorRangeStarts = srcDescriptorRangeStarts)
        fixed (int* pSrcDescriptorRangeSizes = srcDescriptorRangeSizes)
            CopyDescriptors(numDestDescriptorRanges, pDestDescriptorRangeStarts, pDestDescriptorRangeSizes,
                numSrcDescriptorRanges, pSrcDescriptorRangeStarts, pSrcDescriptorRangeSizes,
                descriptorHeapsType);
    }

    #endregion

    #region GetCopyableFootprints
    public ulong GetRequiredIntermediateSize(ID3D12Resource resource, int firstSubresource, int numSubresources)
    {
        ResourceDescription desc = resource.GetDescription();
        GetCopyableFootprints(desc, firstSubresource, numSubresources, 0, out ulong requiredSize);
        return requiredSize;
    }

    public void GetCopyableFootprints(
        ResourceDescription resourceDesc,
        int firstSubresource,
        int numSubresources,
        ulong baseOffset,
        PlacedSubresourceFootPrint* pLayouts,
        int* pNumRows,
        ulong* pRowSizeInBytes,
        out ulong totalBytes)
    {
        GetCopyableFootprints(resourceDesc, firstSubresource, numSubresources, baseOffset,
            (void*)pLayouts, (void*)pNumRows, (void*)pRowSizeInBytes, out totalBytes);
    }

    public void GetCopyableFootprints(
        ResourceDescription resourceDesc,
        int firstSubresource,
        int numSubresources,
        ulong baseOffset,
        out ulong totalBytes)
    {
        GetCopyableFootprints(resourceDesc, firstSubresource, numSubresources, baseOffset, (void*)null, (void*)null, (void*)null, out totalBytes);
    }

    public void GetCopyableFootprints(ResourceDescription resourceDesc,
        int firstSubresource,
        int numSubresources,
        ulong baseOffset,
        PlacedSubresourceFootPrint[] layouts,
        int[] numRows,
        ulong[] rowSizeInBytes,
        out ulong totalBytes)
    {
        fixed (PlacedSubresourceFootPrint* pLayouts = layouts)
        fixed (int* pNumRows = numRows)
        fixed (ulong* pRowSizeInBytes = rowSizeInBytes)
        {
            GetCopyableFootprints(resourceDesc, firstSubresource, numSubresources, baseOffset, pLayouts, pNumRows, pRowSizeInBytes, out totalBytes);
        }
    }

    public void GetCopyableFootprints(
        ResourceDescription resourceDesc,
        int firstSubresource,
        int numSubresources,
        ulong baseOffset,
        Span<PlacedSubresourceFootPrint> layouts,
        Span<int> numRows,
        Span<ulong> rowSizeInBytes,
        out ulong totalBytes)
    {
        fixed (PlacedSubresourceFootPrint* pLayouts = layouts)
        fixed (int* pNumRows = numRows)
        fixed (ulong* pRowSizeInBytes = rowSizeInBytes)
        {
            GetCopyableFootprints(resourceDesc, firstSubresource, numSubresources, baseOffset, pLayouts, pNumRows, pRowSizeInBytes, out totalBytes);
        }
    }
    #endregion

    public IntPtr CreateSharedHandle(ID3D12DeviceChild deviceChild, SecurityAttributes? attributes, string name)
    {
        return CreateSharedHandlePrivate(deviceChild, attributes, GENERIC_ALL, name);
    }

    /// <summary>
    /// Opens a handle for shared resources, shared heaps, and shared fences.
    /// </summary>
    /// <typeparam name="T">The handle that was output by the call to <see cref="CreateSharedHandle(ID3D12DeviceChild, SecurityAttributes?, string)"/> </typeparam>
    /// <param name="handle"></param>
    /// <returns>Instance of <see cref="ID3D12Heap"/>, <see cref="ID3D12Resource"/> or <see cref="ID3D12Fence"/>.</returns>
    public T OpenSharedHandle<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr handle) where T : ComObject
    {
        OpenSharedHandle(handle, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    /// <summary>
    /// Opens a handle for shared resources, shared heaps, and shared fences.
    /// </summary>
    /// <typeparam name="T">The handle that was output by the call to <see cref="CreateSharedHandle(ID3D12DeviceChild, SecurityAttributes?, string)"/> </typeparam>
    /// <param name="handle"></param>
    /// <param name="sharedHandle">The shared handle instance.</param>
    /// <returns>The <see cref="Result"/> of the operation.</returns>
    public Result OpenSharedHandle<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr handle, out T? sharedHandle) where T : ComObject
    {
        Result result = OpenSharedHandle(handle, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            sharedHandle = default;
            return result;
        }

        sharedHandle = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    /// <summary>
    /// Opens a handle for shared resources, shared heaps, and shared fences, by using Name and Access.
    /// </summary>
    /// <param name="name">The name that was optionally passed as the name parameter in the call to <see cref="CreateSharedHandle(ID3D12DeviceChild, SecurityAttributes?, string)"/> </param>
    /// <returns>The shared handle.</returns>
    public IntPtr OpenSharedHandleByName(string name)
    {
        Result result = OpenSharedHandleByName(name, GENERIC_ALL, out IntPtr handleRef);
        if (result.Failure)
        {
            return IntPtr.Zero;
        }

        return handleRef;
    }

    public HeapProperties GetCustomHeapProperties(HeapType heapType)
    {
        return GetCustomHeapProperties(0, heapType);
    }

    /// <summary>
    /// Enables the page-out of data, which precludes GPU access of that data.
    /// </summary>
    /// <param name="objects"></param>
    public void Evict(params ID3D12Pageable[] objects)
    {
        Evict(objects.Length, objects);
    }

    /// <summary>
    /// Makes objects resident for the device.
    /// </summary>
    /// <param name="objects"></param>
    public void MakeResident(params ID3D12Pageable[] objects)
    {
        MakeResident(objects.Length, objects);
    }

    public ResourceAllocationInfo GetResourceAllocationInfo(int visibleMask, params ResourceDescription[] resourceDescriptions)
    {
        return GetResourceAllocationInfo(visibleMask, resourceDescriptions.Length, resourceDescriptions);
    }

    public ResourceAllocationInfo GetResourceAllocationInfo(params ResourceDescription[] resourceDescriptions)
    {
        return GetResourceAllocationInfo(0, resourceDescriptions.Length, resourceDescriptions);
    }
}
