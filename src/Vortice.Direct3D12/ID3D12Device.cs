// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Direct3D;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device
    {
        private const int GENERIC_ALL = 0x10000000;
        private RootSignatureVersion? _highestRootSignatureVersion;

        public static bool IsSupported(IUnknown adapter, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            try
            {
                return D3D12.D3D12CreateDeviceNoDevice(adapter, minFeatureLevel).Success;
            }
            catch (DllNotFoundException)
            {
                // On pre Windows 10 d3d12.dll is not present and therefore not supported.
                return false;
            }
        }

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

                    if (CheckFeatureSupport(Feature.RootSignature, new IntPtr(&featureData), Unsafe.SizeOf<FeatureDataRootSignature>()).Failure)
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
            CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), sizeof(T));
            return featureSupport;
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : unmanaged
        {
            return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), sizeof(T)).Success;
        }

        public unsafe Result CheckMaxSupportedFeatureLevel(FeatureLevel[] featureLevels, out FeatureLevel maxSupportedFeatureLevel)
        {
            fixed (FeatureLevel* levelsPtr = &featureLevels[0])
            {
                var featureData = new FeatureDataFeatureLevels
                {
                    NumFeatureLevels = featureLevels.Length,
                    PFeatureLevelsRequested = new IntPtr(levelsPtr)
                };

                var result = CheckFeatureSupport(Feature.FeatureLevels, new IntPtr(&featureData), Unsafe.SizeOf<FeatureDataFeatureLevels>());
                maxSupportedFeatureLevel = featureData.MaxSupportedFeatureLevel;
                return result;
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

        public ID3D12Resource CreateCommittedResource(HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription description,
            ResourceStates initialResourceState,
            ClearValue? optimizedClearValue = null)
        {
            return CreateCommittedResource(
                ref heapProperties,
                heapFlags,
                ref description,
                initialResourceState,
                optimizedClearValue,
                typeof(ID3D12Resource).GUID);
        }

        public ID3D12CommandQueue CreateCommandQueue(CommandListType type, int priority = 0, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0)
        {
            return CreateCommandQueue(new CommandQueueDescription(type, priority, flags, nodeMask), typeof(ID3D12CommandQueue).GUID);
        }

        public ID3D12CommandQueue CreateCommandQueue(CommandListType type, CommandQueuePriority priority, CommandQueueFlags flags = CommandQueueFlags.None, int nodeMask = 0)
        {
            return CreateCommandQueue(new CommandQueueDescription(type, priority, flags, nodeMask), typeof(ID3D12CommandQueue).GUID);
        }

        public ID3D12CommandQueue CreateCommandQueue(CommandQueueDescription description)
        {
            return CreateCommandQueue(description, typeof(ID3D12CommandQueue).GUID);
        }

        public ID3D12DescriptorHeap CreateDescriptorHeap(DescriptorHeapDescription description)
        {
            return CreateDescriptorHeap(description, typeof(ID3D12DescriptorHeap).GUID);
        }

        public ID3D12CommandAllocator CreateCommandAllocator(CommandListType type)
        {
            return CreateCommandAllocator(type, typeof(ID3D12CommandAllocator).GUID);
        }

        public ID3D12GraphicsCommandList CreateCommandList(CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState initialState = null)
        {
            return CreateCommandList(0, type, commandAllocator, initialState);
        }

        public ID3D12GraphicsCommandList CreateCommandList(int nodeMask, CommandListType type, ID3D12CommandAllocator commandAllocator, ID3D12PipelineState initialState = null)
        {
            var nativePtr = CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(ID3D12GraphicsCommandList).GUID);
            return new ID3D12GraphicsCommandList(nativePtr);
        }

        public ID3D12Fence CreateFence(long initialValue, FenceFlags flags = FenceFlags.None)
        {
            return CreateFence(initialValue, flags, typeof(ID3D12Fence).GUID);
        }

        public ID3D12Heap CreateHeap(HeapDescription description)
        {
            return CreateHeap(ref description, typeof(ID3D12Heap).GUID);
        }

        public ID3D12RootSignature CreateRootSignature(int nodeMask, RootSignatureDescription description, RootSignatureVersion version)
        {
            var result = D3D12.D3D12SerializeRootSignature(description, version, out var blob, out var errorBlob);
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
                return CreateRootSignature(nodeMask, blob.BufferPointer, blob.BufferSize, typeof(ID3D12RootSignature).GUID);
            }
            finally
            {
                errorBlob?.Dispose();
                blob.Dispose();
            }
        }

        public ID3D12RootSignature CreateRootSignature(RootSignatureDescription description, RootSignatureVersion version)
        {
            return CreateRootSignature(0, description, version);
        }

        public ID3D12RootSignature CreateRootSignature(int nodeMask, VersionedRootSignatureDescription rootSignatureDescription)
        {
            var result = Result.Ok;
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
                                for (var i = 0; i < parameters_1_0.Length; i++)
                                {
                                    var parameterShaderVisibility = desc_1_1.Parameters[i].ShaderVisibility;

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
                                            var table_1_1 = desc_1_1.Parameters[i].DescriptorTable;
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
                return CreateRootSignature(nodeMask, signature.BufferPointer, signature.BufferSize, typeof(ID3D12RootSignature).GUID);
            }
            finally
            {
                errorBlob?.Dispose();
                signature.Dispose();
            }
        }

        public ID3D12RootSignature CreateRootSignature(VersionedRootSignatureDescription rootSignatureDescription)
        {
            return CreateRootSignature(0, rootSignatureDescription);
        }

        public ID3D12CommandSignature CreateCommandSignature(CommandSignatureDescription description, ID3D12RootSignature rootSignature)
        {
            return CreateCommandSignature(description, rootSignature, typeof(ID3D12CommandSignature).GUID);
        }

        public ID3D12PipelineState CreateComputePipelineState(ComputePipelineStateDescription description)
        {
            return CreateComputePipelineState(description, typeof(ID3D12PipelineState).GUID);
        }

        public ID3D12PipelineState CreateGraphicsPipelineState(GraphicsPipelineStateDescription description)
        {
            return CreateGraphicsPipelineState(description, typeof(ID3D12PipelineState).GUID);
        }

        public ID3D12QueryHeap CreateQueryHeap(QueryHeapDescription description)
        {
            return CreateQueryHeap(description, typeof(ID3D12QueryHeap).GUID);
        }

        public ID3D12Resource CreatePlacedResource(
            ID3D12Heap heap,
            long heapOffset,
            ResourceDescription resourceDescription,
            ResourceStates initialState,
            ClearValue? clearValue = null)
        {
            return CreatePlacedResource(heap, heapOffset, ref resourceDescription, initialState, clearValue, typeof(ID3D12Resource).GUID);
        }

        public ID3D12Resource CreateReservedResource(ResourceDescription resourceDescription, ResourceStates initialState, ClearValue? clearValue = null)
        {
            return CreateReservedResource(ref resourceDescription, initialState, clearValue, typeof(ID3D12Resource).GUID);
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
            var result = OpenSharedHandle(handle, typeof(T).GUID, out var nativePtr);
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
