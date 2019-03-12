// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using SharpGen.Runtime;
using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device
    {
        public static Result TryCreate(
            IUnknown adapter,
            FeatureLevel minFeatureLevel,
            out ID3D12Device device)
        {
            var result = D3D12.CreateDevice(
                adapter,
                minFeatureLevel,
                typeof(ID3D12Device).GUID,
                out var nativePtr);

            if (result.Failure)
            {
                device = null;
                return result;
            }

            device = new ID3D12Device(nativePtr);
            return result;
        }

        public static bool IsSupported(IUnknown adapter, FeatureLevel minFeatureLevel = FeatureLevel.Level_11_0)
        {
            var result = D3D12.CreateDevice(
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

        public ID3D12Resource CreateCommittedResource(HeapProperties heapProperties, HeapFlags heapFlags,
            ResourceDescription description, ResourceStates initialResourceState,
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

        public ID3D12Fence CreateFence(ulong initialValue, FenceFlags flags)
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
    }
}
