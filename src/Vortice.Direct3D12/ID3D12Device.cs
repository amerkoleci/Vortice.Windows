// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device
    {
        private const int GENERIC_ALL = 0x10000000;
        private RootSignatureVersion? _highestRootSignatureVersion;

        public unsafe RootSignatureVersion HighestRootSignatureVersion
        {
            get
            {
                if (!_highestRootSignatureVersion.HasValue)
                {
                    var featureData = new FeatureDataRootSignature
                    {
                        HighestVersion = RootSignatureVersion.Version11
                    };

                    if (CheckFeatureSupport(Feature.RootSignature, new IntPtr(&featureData), sizeof(FeatureDataRootSignature)).Failure)
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

        public unsafe T CheckFeatureSupport<T>(Feature feature) where T : unmanaged
        {
            T featureSupport = default;
            CheckFeatureSupport(feature, new IntPtr(&featureSupport), sizeof(T));
            return featureSupport;
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
        {
            fixed (void* featureSupportPtr = &featureSupport)
            {
                return CheckFeatureSupport(feature, (IntPtr)featureSupportPtr, sizeof(T)).Success;
            }
        }

        public FeatureLevel CheckMaxSupportedFeatureLevel() => CheckMaxSupportedFeatureLevel(D3D12.FeatureLevels);

        public unsafe FeatureLevel CheckMaxSupportedFeatureLevel(FeatureLevel[] featureLevels)
        {
            fixed (FeatureLevel* levelsPtr = &featureLevels[0])
            {
                var featureData = new FeatureDataFeatureLevels
                {
                    NumFeatureLevels = featureLevels.Length,
                    PFeatureLevelsRequested = new IntPtr(levelsPtr),
                    MaxSupportedFeatureLevel = FeatureLevel.Level_11_0
                };

                if (CheckFeatureSupport(Feature.FeatureLevels, new IntPtr(&featureData), Unsafe.SizeOf<FeatureDataFeatureLevels>()).Success)
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

        public FeatureDataArchitecture Architecture
        {
            get => CheckFeatureSupport<FeatureDataArchitecture>(Feature.Architecture);
        }

        public FeatureDataGpuVirtualAddressSupport GpuVirtualAddressSupport
        {
            get => CheckFeatureSupport<FeatureDataGpuVirtualAddressSupport>(Feature.GpuVirtualAddressSupport);
        }

        public FeatureDataD3D12Options1 Options1
        {
            get => CheckFeatureSupport<FeatureDataD3D12Options1>(Feature.Options1);
        }

        public FeatureDataProtectedResourceSessionSupport ProtectedResourceSessionSupport
        {
            get => CheckFeatureSupport<FeatureDataProtectedResourceSessionSupport>(Feature.ProtectedResourceSessionSupport);
        }

        public FeatureDataArchitecture1 Architecture1
        {
            get => CheckFeatureSupport<FeatureDataArchitecture1>(Feature.Architecture1);
        }

        public FeatureDataD3D12Options2 Options2
        {
            get => CheckFeatureSupport<FeatureDataD3D12Options2>(Feature.Options2);
        }

        public FeatureDataShaderCache ShaderCache
        {
            get => CheckFeatureSupport<FeatureDataShaderCache>(Feature.ShaderCache);
        }

        public FeatureDataCommandQueuePriority CommandQueuePriority
        {
            get => CheckFeatureSupport<FeatureDataCommandQueuePriority>(Feature.CommandQueuePriority);
        }

        public FeatureDataD3D12Options3 Options3
        {
            get => CheckFeatureSupport<FeatureDataD3D12Options3>(Feature.Options3);
        }

        public FeatureDataExistingHeaps ExistingHeaps
        {
            get => CheckFeatureSupport<FeatureDataExistingHeaps>(Feature.ExistingHeaps);
        }

        public FeatureDataD3D12Options4 Options4
        {
            get => CheckFeatureSupport<FeatureDataD3D12Options4>(Feature.Options4);
        }

        public FeatureDataSerialization Serialization
        {
            get => CheckFeatureSupport<FeatureDataSerialization>(Feature.Serialization);
        }

        public FeatureDataCrossNode CrossNode
        {
            get => CheckFeatureSupport<FeatureDataCrossNode>(Feature.CrossNode);
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

        public FeatureDataQueryMetaCommand QueryMetaCommand
        {
            get => CheckFeatureSupport<FeatureDataQueryMetaCommand>(Feature.QueryMetaCommand);
        }

        public unsafe ShaderModel CheckHighestShaderModel(ShaderModel highestShaderModel)
        {
            var featureData = new FeatureDataShaderModel
            {
                HighestShaderModel = highestShaderModel
            };

            if (CheckFeatureSupport(Feature.ShaderModel, new IntPtr(&featureData), Unsafe.SizeOf<FeatureDataShaderModel>()).Success)
            {
                return featureData.HighestShaderModel;
            }

            return ShaderModel.Model51;
        }

        public unsafe RootSignatureVersion CheckHighestRootSignatureVersion(RootSignatureVersion highestVersion)
        {
            var featureData = new FeatureDataRootSignature
            {
                HighestVersion = highestVersion
            };

            if (CheckFeatureSupport(Feature.RootSignature, new IntPtr(&featureData), Unsafe.SizeOf<FeatureDataRootSignature>()).Success)
            {
                return featureData.HighestVersion;
            }

            return RootSignatureVersion.Version10;
        }

        public unsafe byte GetFormatPlaneCount(Format format)
        {
            var featureData = new FeatureDataFormatInfo
            {
                Format = format
            };

            if (CheckFeatureSupport(Feature.FormatInfo,
                new IntPtr(&featureData),
                sizeof(FeatureDataFormatInfo)).Failure)
            {
                return 0;
            }

            return featureData.PlaneCount;
        }

        #region CreateCommittedResource
        public ID3D12Resource CreateCommittedResource(HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription description,
            ResourceStates initialResourceState,
            ClearValue? optimizedClearValue = null)
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
                return default;

            return new ID3D12Resource(nativePtr);
        }

        public T CreateCommittedResource<T>(HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription description,
            ResourceStates initialResourceState,
            ClearValue? optimizedClearValue = null) where T : ID3D12Resource
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
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateCommittedResource<T>(HeapProperties heapProperties, HeapFlags heapFlags, ResourceDescription description, ResourceStates initialResourceState, out T resource) where T : ID3D12Resource
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

            resource = FromPointer<T>(nativePtr);
            return result;
        }

        public Result CreateCommittedResource<T>(HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription description,
            ResourceStates initialResourceState,
            ClearValue? optimizedClearValue,
            out T resource) where T : ID3D12Resource
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

            resource = FromPointer<T>(nativePtr);
            return result;
        }
        #endregion

        #region CreateCommandQueue
        public ID3D12CommandQueue CreateCommandQueue(in CommandQueueDescription description)
        {
            Result result = CreateCommandQueue(description, typeof(ID3D12CommandQueue).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

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

        public Result CreateCommandQueue<T>(in CommandQueueDescription description, out T commandQueue) where T : ID3D12CommandQueue
        {
            Result result = CreateCommandQueue(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                commandQueue = default;
                return result;
            }

            commandQueue = FromPointer<T>(nativePtr);
            return result;
        }

        public T CreateCommandQueue<T>(in CommandQueueDescription description) where T : ID3D12CommandQueue
        {
            Result result = CreateCommandQueue(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public T CreateCommandQueue<T>(CommandListType type, int priority = 0, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0) where T : ID3D12CommandQueue
        {
            return CreateCommandQueue<T>(new CommandQueueDescription(type, priority, flags, nodeMask));
        }

        public T CreateCommandQueue<T>(CommandListType type, CommandQueuePriority priority, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0) where T : ID3D12CommandQueue
        {
            return CreateCommandQueue<T>(new CommandQueueDescription(type, priority, flags, nodeMask));
        }
        #endregion

        #region CreateDescriptorHeap
        public ID3D12DescriptorHeap CreateDescriptorHeap(in DescriptorHeapDescription description)
        {
            Result result = CreateDescriptorHeap(description, typeof(ID3D12DescriptorHeap).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12DescriptorHeap(nativePtr);
        }

        public T CreateDescriptorHeap<T>(in DescriptorHeapDescription description) where T : ID3D12DescriptorHeap
        {
            Result result = CreateDescriptorHeap(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateDescriptorHeap<T>(in DescriptorHeapDescription description, out T descriptorHeap) where T : ID3D12DescriptorHeap
        {
            Result result = CreateDescriptorHeap(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                descriptorHeap = default;
                return result;
            }

            descriptorHeap = FromPointer<T>(nativePtr);
            return result;
        }
        #endregion

        #region CreateCommandAllocator
        public ID3D12CommandAllocator CreateCommandAllocator(CommandListType type)
        {
            Result result = CreateCommandAllocator(type, typeof(ID3D12CommandAllocator).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12CommandAllocator(nativePtr);
        }

        public T CreateCommandAllocator<T>(CommandListType type) where T : ID3D12CommandAllocator
        {
            Result result = CreateCommandAllocator(type, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateCommandAllocator<T>(CommandListType type, out T commandAllocator) where T : ID3D12CommandAllocator
        {
            Result result = CreateCommandAllocator(type, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                commandAllocator = default;
                return result;
            }

            commandAllocator = FromPointer<T>(nativePtr);
            return result;
        }
        #endregion

        #region CreateCommandList
        public ID3D12GraphicsCommandList CreateCommandList(int nodeMask, CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState initialState = null)
        {
            Result result = CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(ID3D12GraphicsCommandList).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12GraphicsCommandList(nativePtr);
        }

        public T CreateCommandList<T>(int nodeMask, CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState initialState = null) where T : ID3D12CommandList
        {
            Result result = CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateCommandList<T>(int nodeMask, CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState initialState, out T commandList) where T : ID3D12CommandList
        {
            Result result = CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                commandList = default;
                return result;
            }

            commandList = FromPointer<T>(nativePtr);
            return result;
        }
        #endregion

        #region CreateFence
        public ID3D12Fence CreateFence(long initialValue, FenceFlags flags = FenceFlags.None)
        {
            Result result = CreateFence(initialValue, flags, typeof(ID3D12Fence).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12Fence(nativePtr);
        }

        public T CreateFence<T>(long initialValue, FenceFlags flags = FenceFlags.None) where T : ID3D12Fence
        {
            Result result = CreateFence(initialValue, flags, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateFence<T>(long initialValue, FenceFlags flags, out T fence) where T : ID3D12Fence
        {
            Result result = CreateFence(initialValue, flags, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                fence = default;
                return result;
            }

            fence = FromPointer<T>(nativePtr);
            return result;
        }
        #endregion

        #region CreateHeap
        public T CreateHeap<T>(in HeapDescription description) where T : ID3D12Heap
        {
            Result result = CreateHeap(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateHeap<T>(in HeapDescription description, out T heap) where T : ID3D12Heap
        {
            Result result = CreateHeap(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                heap = default;
                return result;
            }

            heap = FromPointer<T>(nativePtr);
            return result;
        }
        #endregion

        public T CreateRootSignature<T>(int nodeMask, in RootSignatureDescription description, RootSignatureVersion version) where T : ID3D12RootSignature
        {
            Result result = D3D12.D3D12SerializeRootSignature(description, version, out Blob blob, out Blob errorBlob);
            if (result.Failure)
            {
                if (errorBlob != null)
                {
                    throw new SharpGenException(result, errorBlob.ConvertToString());
                }

                throw new SharpGenException(result);
            }

            try
            {
                result = CreateRootSignature(nodeMask, blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
                if (result.Failure)
                    return default;

                return FromPointer<T>(nativePtr);
            }
            finally
            {
                errorBlob?.Dispose();
                blob.Dispose();
            }
        }

        public ID3D12RootSignature CreateRootSignature(int nodeMask, in VersionedRootSignatureDescription description)
        {
            return CreateRootSignature<ID3D12RootSignature>(0, description);
        }

        public T CreateRootSignature<T>(int nodeMask, VersionedRootSignatureDescription rootSignatureDescription) where T : ID3D12RootSignature
        {
            Result result = Result.Ok;
            Blob signature = null;
            Blob errorBlob = null;

            // D3DX12SerializeVersionedRootSignature
            switch (HighestRootSignatureVersion)
            {
                case RootSignatureVersion.Version10:
                    switch (rootSignatureDescription.Version)
                    {
                        case RootSignatureVersion.Version10:
                            result = D3D12.D3D12SerializeRootSignature(rootSignatureDescription.Description_1_0, RootSignatureVersion.Version1, out signature, out errorBlob);
                            break;

                        case RootSignatureVersion.Version11:
                            // Convert to version 1.0.
                            var desc_1_1 = rootSignatureDescription.Description_1_1;
                            RootParameter[] parameters_1_0 = null;

                            if (desc_1_1.Parameters?.Length > 0)
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
                                            RootDescriptorTable1 table_1_1 = desc_1_1.Parameters[i].DescriptorTable;
                                            var ranges = new DescriptorRange[table_1_1.Ranges?.Length ?? 0];

                                            for (var x = 0; x < ranges.Length; x++)
                                            {
                                                ranges[x] = new DescriptorRange(
                                                    table_1_1.Ranges[x].RangeType,
                                                    table_1_1.Ranges[x].NumDescriptors,
                                                    table_1_1.Ranges[x].BaseShaderRegister,
                                                    table_1_1.Ranges[x].RegisterSpace,
                                                    table_1_1.Ranges[x].OffsetInDescriptorsFromTableStart);
                                            }

                                            parameters_1_0[i] = new RootParameter(new RootDescriptorTable(ranges), parameterShaderVisibility);
                                            break;
                                    }
                                }
                            }

                            var desc_1_0 = new RootSignatureDescription(desc_1_1.Flags, parameters_1_0, desc_1_1.StaticSamplers);
                            result = D3D12.D3D12SerializeRootSignature(desc_1_0, RootSignatureVersion.Version1, out signature, out errorBlob);
                            break;
                    }
                    break;

                case RootSignatureVersion.Version11:
                    result = D3D12.D3D12SerializeVersionedRootSignature(rootSignatureDescription, out signature, out errorBlob);
                    break;
            }

            if (result.Failure || signature == null)
            {
                if (errorBlob != null)
                {
                    throw new SharpGenException(result, errorBlob.ConvertToString());
                }

                throw new SharpGenException(result);
            }

            try
            {
                result = CreateRootSignature(nodeMask, signature.BufferPointer, signature.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
                if (result.Failure)
                    return default;

                return FromPointer<T>(nativePtr);
            }
            finally
            {
                errorBlob?.Dispose();
                signature.Dispose();
            }
        }

        public T CreateCommandSignature<T>(CommandSignatureDescription description, ID3D12RootSignature rootSignature) where T : ID3D12CommandSignature
        {
            Result result = CreateCommandSignature(description, rootSignature, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public T CreateComputePipelineState<T>(ComputePipelineStateDescription description) where T : ID3D12PipelineState
        {
            Result result = CreateComputePipelineState(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public ID3D12PipelineState CreateComputePipelineStat(ComputePipelineStateDescription description)
        {
            Result result = CreateComputePipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12PipelineState(nativePtr);
        }

        public T CreateGraphicsPipelineState<T>(GraphicsPipelineStateDescription description) where T : ID3D12PipelineState
        {
            Result result = CreateGraphicsPipelineState(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public ID3D12PipelineState CreateGraphicsPipelineState(GraphicsPipelineStateDescription description)
        {
            Result result = CreateGraphicsPipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return new ID3D12PipelineState(nativePtr);
        }

        public T CreateQueryHeap<T>(QueryHeapDescription description) where T : ID3D12QueryHeap
        {
            Result result = CreateQueryHeap(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreateQueryHeap<T>(in QueryHeapDescription description, out T queryHeap) where T : ID3D12QueryHeap
        {
            Result result = CreateQueryHeap(description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                queryHeap = default;
                return result;
            }

            queryHeap = FromPointer<T>(nativePtr);
            return result;
        }

        public T CreatePlacedResource<T>(
            ID3D12Heap heap,
            long heapOffset,
            ResourceDescription resourceDescription,
            ResourceStates initialState,
            ClearValue? clearValue = null) where T : ID3D12Resource
        {
            Result result = CreatePlacedResource(heap, heapOffset, ref resourceDescription, initialState, clearValue, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public T CreateReservedResource<T>(ResourceDescription resourceDescription, ResourceStates initialState, ClearValue? clearValue = null) where T : ID3D12Resource
        {
            Result result = CreateReservedResource(ref resourceDescription, initialState, clearValue, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

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
        public T OpenSharedHandle<T>(IntPtr handle) where T : ComObject
        {
            Result result = OpenSharedHandle(handle, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }

        /// <summary>
        /// Opens a handle for shared resources, shared heaps, and shared fences, by using Name and Access.
        /// </summary>
        /// <param name="name">The name that was optionally passed as the name parameter in the call to <see cref="CreateSharedHandle(ID3D12DeviceChild, SecurityAttributes?, string)"/> </param>
        /// <returns>The shared handle.</returns>
        public IntPtr OpenSharedHandleByName(string name)
        {
            var result = OpenSharedHandleByName(name, GENERIC_ALL, out var handleRef);
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
}
