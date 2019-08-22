// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Interop;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12GraphicsCommandList
    {
        public unsafe void ResourceBarrierTransition(
            ID3D12Resource resource,
            ResourceStates stateBefore,
            ResourceStates stateAfter,
            int subresource = Direct3D12.ResourceBarrier.AllSubResources,
            ResourceBarrierFlags flags = ResourceBarrierFlags.None)
        {
            var barrier = new ResourceBarrier(
                new ResourceTransitionBarrier(resource, stateBefore, stateAfter, subresource),
                flags);
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrierAliasing(ID3D12Resource resourceBefore, ID3D12Resource resourceAfter)
        {
            var barrier = new ResourceBarrier(new ResourceAliasingBarrier(resourceBefore, resourceAfter));
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrierUnorderedAccessView(ID3D12Resource resource)
        {
            var barrier = new ResourceBarrier(new ResourceUnorderedAccessViewBarrier(resource));
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrier(ResourceBarrier barrier)
        {
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrier(params ResourceBarrier[] barriers)
        {
            fixed (void* pBarriers = barriers)
            {
                ResourceBarrier(barriers.Length, new IntPtr(pBarriers));
            }
        }

        public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, RawColor4 colorRGBA, params RawRectangle[] rectangles)
        {
            if (rectangles.Length == 0)
            {
                ClearRenderTargetView(renderTargetView, colorRGBA, 0, null);
            }
            else
            {
                ClearRenderTargetView(renderTargetView, colorRGBA, rectangles.Length, rectangles);
            }
        }

        public void ClearDepthStencilView(CpuDescriptorHandle depthStencilView, ClearFlags clearFlags, float depth, byte stencil, params InteropRect[] rectangles)
        {
            if (rectangles.Length == 0)
            {
                ClearDepthStencilView(depthStencilView, clearFlags, depth, stencil, 0, null);
            }
            else
            {
                ClearDepthStencilView(depthStencilView, clearFlags, depth, stencil, rectangles.Length, rectangles);
            }
        }

        public unsafe void ClearUnorderedAccessView(
            GpuDescriptorHandle viewGpuHandleInCurrentHeap,
            CpuDescriptorHandle viewCpuHandle,
            ID3D12Resource resource,
            RawColor4 clearValue,
            params RawRectangle[] rectangles)
        {
            if (rectangles.Length == 0)
            {
                ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, clearValue, 0, null);
            }
            else
            {
                ClearUnorderedAccessViewFloat(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, clearValue, rectangles.Length, rectangles);
            }
        }

        public unsafe void ClearUnorderedAccessView(
            GpuDescriptorHandle viewGpuHandleInCurrentHeap,
            CpuDescriptorHandle viewCpuHandle,
            ID3D12Resource resource,
            RawInt4 clearValue,
            params RawRectangle[] rectangles)
        {
            if (rectangles.Length == 0)
            {
                ClearUnorderedAccessViewUint(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, clearValue, 0, null);
            }
            else
            {
                ClearUnorderedAccessViewUint(viewGpuHandleInCurrentHeap, viewCpuHandle, resource, clearValue, rectangles.Length, rectangles);
            }
        }

        public unsafe void RSSetViewport(RawViewport viewport)
        {
            RSSetViewports(1, new IntPtr(&viewport));
        }

        public void RSSetViewports(params RawViewport[] viewports)
        {
            unsafe
            {
                fixed (void* pViewPorts = viewports)
                {
                    RSSetViewports(viewports.Length, (IntPtr)pViewPorts);
                }
            }
        }

        public unsafe void RSSetScissorRect(RawRectangle rectangle)
        {
            RSSetScissorRects(1, new IntPtr(&rectangle));
        }

        public void RSSetScissorRects(params RawRectangle[] rectangles)
        {
            unsafe
            {
                fixed (void* pRects = rectangles)
                {
                    RSSetScissorRects(rectangles.Length, (IntPtr)pRects);
                }
            }
        }

        public unsafe void OMSetRenderTargets(CpuDescriptorHandle renderTargetDescriptor, CpuDescriptorHandle? depthStencilDescriptor = null)
        {
            OMSetRenderTargets(1, new IntPtr(&renderTargetDescriptor), false, depthStencilDescriptor);
        }

        public unsafe void OMSetRenderTargets(CpuDescriptorHandle[] renderTargetDescriptors, CpuDescriptorHandle? depthStencilDescriptor = null)
        {
            fixed (void* pRT = renderTargetDescriptors)
            {
                OMSetRenderTargets(renderTargetDescriptors?.Length ?? 0, new IntPtr(pRT), false, depthStencilDescriptor);
            }
        }

        public unsafe void OMSetRenderTargets(int renderTargetDescriptorsCount, CpuDescriptorHandle[] renderTargetDescriptors, CpuDescriptorHandle? depthStencilDescriptor = null)
        {
            fixed (void* pRT = renderTargetDescriptors)
            {
                OMSetRenderTargets(renderTargetDescriptorsCount, new IntPtr(pRT), false, depthStencilDescriptor);
            }
        }

        public unsafe void IASetVertexBuffers(int startSlot, int viewsCount, VertexBufferView[] vertexBufferViews)
        {
            fixed (void* descPtr = vertexBufferViews)
            {
                IASetVertexBuffers(startSlot, viewsCount, new IntPtr(descPtr));
            }
        }

        public unsafe void IASetVertexBuffers(int startSlot, params VertexBufferView[] vertexBufferViews)
        {
            fixed (void* descPtr = vertexBufferViews)
            {
                IASetVertexBuffers(startSlot, vertexBufferViews.Length, new IntPtr(descPtr));
            }
        }

        public unsafe void IASetVertexBuffers(int startSlot, VertexBufferView vertexBufferView)
        {
            IASetVertexBuffers(startSlot, 1, (IntPtr)(&vertexBufferView));
        }

        public unsafe void IASetVertexBuffers(VertexBufferView vertexBufferView)
        {
            IASetVertexBuffers(0, 1, (IntPtr)(&vertexBufferView));
        }

        public void BeginEvent(string name)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            var handle = IntPtr.Zero;
            try
            {
                handle = Marshal.StringToHGlobalUni(name);
                BeginEvent(1, handle, name.Length);
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;
                }
            }
        }

        public void SetMarker(string name)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            var handle = IntPtr.Zero;
            try
            {
                handle = Marshal.StringToHGlobalUni(name);
                SetMarker(1, handle, name.Length);
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// This method uses the GPU to copy texture data between two locations. 
        /// Both the source and the destination may reference texture data located within either a buffer resource or a texture resource.
        /// </summary>
        /// <param name="destination">Specifies the destination <see cref="TextureCopyLocation"/>. The subresource referred to must be in the <see cref="ResourceStates.CopyDestination"/> state.</param>
        /// <param name="destinationX">The x-coordinate of the upper left corner of the destination region.</param>
        /// <param name="destinationY">The y-coordinate of the upper left corner of the destination region. For a 1D subresource, this must be zero.</param>
        /// <param name="destinationZ">The z-coordinate of the upper left corner of the destination region. For a 1D or 2D subresource, this must be zero.</param>
        /// <param name="source">Specifies the source D3D12_TEXTURE_COPY_LOCATION. The subresource referred to must be in the D3D12_RESOURCE_STATE_COPY_SOURCE state.</param>
        /// <param name="sourceBox">Specifies an optional <see cref="Box"/> that sets the size of the source texture to copy.</param>
        public void CopyTextureRegion(
            TextureCopyLocation destination,
            int destinationX, int destinationY, int destinationZ,
            TextureCopyLocation source, Box? sourceBox = null)
        {
            CopyTextureRegion_(destination, destinationX, destinationY, destinationZ, source, sourceBox);
        }

        /// <summary>
        /// Discards an entire resource.
        /// </summary>
        /// <param name="resource">The resource to discard.</param>
        public void DiscardResource(ID3D12Resource resource)
        {
            DiscardResource(resource, null);
        }

        /// <summary>
        /// Discards a resource.
        /// </summary>
        /// <param name="resource">The resource to discard.</param>
        /// <param name="firstSubresource">Index of the first subresource in the resource to discard.</param>
        /// <param name="numSubresources">The number of subresources in the resource to discard.</param>
        public void DiscardResource(ID3D12Resource resource, int firstSubresource, int numSubresources)
        {
            DiscardResource(resource, new DiscardRegion
            {
                NumRects = 0,
                PRects = IntPtr.Zero,
                FirstSubresource = firstSubresource,
                NumSubresources = numSubresources
            });
        }

        /// <summary>
        /// Discards a resource.
        /// </summary>
        /// <param name="resource">The resource to discard.</param>
        /// <param name="rects">An array of  rectangles in the resource to discard. If null, DiscardResource discards the entire resource.</param>
        /// <param name="firstSubresource">Index of the first subresource in the resource to discard.</param>
        /// <param name="numSubresources">The number of subresources in the resource to discard.</param>
        public unsafe void DiscardResource(ID3D12Resource resource, RawRectangle[] rects, int firstSubresource, int numSubresources)
        {
            DiscardResource(resource, new DiscardRegion
            {
                NumRects = rects.Length,
                PRects = (IntPtr)Unsafe.AsPointer(ref rects[0]),
                FirstSubresource = firstSubresource,
                NumSubresources = numSubresources
            });
        }


        public void Reset(ID3D12CommandAllocator commandAllocator)
        {
            Guard.NotNull(commandAllocator, nameof(commandAllocator));

            Reset(commandAllocator, null);
        }
    }
}
