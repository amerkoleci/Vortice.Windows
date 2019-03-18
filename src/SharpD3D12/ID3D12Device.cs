// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using SharpDXGI;
using SharpDXGI.Direct3D;

namespace SharpD3D12
{
    public partial class ID3D12Device
    {
        private const int GENERIC_ALL = 0x10000000;

        public static bool IsSupported(IUnknown adapter, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            var result = D3D12.D3D12CreateDevice(
               adapter,
               minFeatureLevel,
               typeof(ID3D12Device).GUID,
               out var nativePtr);
            return result.Success;
        }

        public unsafe bool CheckFeatureSupport<T>(Feature feature, ref T featureSupport) where T : struct
        {
            return CheckFeatureSupport(feature, new IntPtr(Unsafe.AsPointer(ref featureSupport)), Interop.SizeOf<T>()).Success;
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
            Guard.NotNull(commandAllocator, nameof(commandAllocator));

            var nativePtr = CreateCommandList(nodeMask, type, commandAllocator, initialState, typeof(ID3D12GraphicsCommandList).GUID);
            return new ID3D12GraphicsCommandList(nativePtr);
        }

        public ID3D12Fence CreateFence(ulong initialValue, FenceFlags flags = FenceFlags.None)
        {
            return CreateFence(initialValue, flags, typeof(ID3D12Fence).GUID);
        }

        public ID3D12Heap CreateHeap(HeapDescription description)
        {
            return CreateHeap(ref description, typeof(ID3D12Heap).GUID);
        }

        public ID3D12RootSignature CreateRootSignature(Blob blob)
        {
            return CreateRootSignature(0, blob.BufferPointer, blob.BufferSize, typeof(ID3D12RootSignature).GUID);
        }

        public ID3D12RootSignature CreateRootSignature(int nodeMask, Blob blob)
        {
            return CreateRootSignature(nodeMask, blob.BufferPointer, blob.BufferSize, typeof(ID3D12RootSignature).GUID);
        }

        public ID3D12CommandSignature CreateCommandSignature(CommandSignatureDescription description, ID3D12RootSignature rootSignature)
        {
            Guard.NotNull(rootSignature, nameof(rootSignature));

            return CreateCommandSignature(ref description, rootSignature, typeof(ID3D12CommandSignature).GUID);
        }

        public ID3D12PipelineState CreateComputePipelineState(ComputePipelineStateDescription description)
        {
            return CreateComputePipelineState(ref description, typeof(ID3D12PipelineState).GUID);
        }

        public ID3D12QueryHeap CreateQueryHeap(QueryHeapDescription description)
        {
            return CreateQueryHeap(description, typeof(ID3D12QueryHeap).GUID);
        }

        public ID3D12Resource CreatePlacedResource(
            ID3D12Heap heap,
            ulong heapOffset,
            ResourceDescription resourceDescription,
            ResourceStates initialState,
            ClearValue? clearValue = null)
        {
            Guard.NotNull(heap, nameof(heap));

            return CreatePlacedResource(heap, heapOffset, ref resourceDescription, initialState, clearValue, typeof(ID3D12Resource).GUID);
        }

        public ID3D12Resource CreateReservedResource(ResourceDescription resourceDescription, ResourceStates initialState, ClearValue? clearValue = null)
        {
            return CreateReservedResource(ref resourceDescription, initialState, clearValue, typeof(ID3D12Resource).GUID);
        }

        public IntPtr CreateSharedHandle(ID3D12DeviceChild deviceChild, SecurityAttributes? attributes, string name)
        {
            Guard.NotNull(deviceChild, nameof(deviceChild));
            Guard.NotNullOrEmpty(name, nameof(name));

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
            Guard.IsTrue(handle != IntPtr.Zero, nameof(handle), "Invalid handle");
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
    }
}
