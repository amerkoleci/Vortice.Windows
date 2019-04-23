// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using SharpDXGI;

namespace SharpDirect3D12
{
    public partial class ID3D12GraphicsCommandList
    {
        public unsafe void ResourceBarrierTransition(
            ID3D12Resource resource,
            ResourceStates stateBefore,
            ResourceStates stateAfter,
            int subresource = -1,
            ResourceBarrierFlags flags = ResourceBarrierFlags.None)
        {
            var barrier = new ResourceBarrier
            {
                Type = ResourceBarrierType.Transition,
                Flags = flags,
                Transition = new ResourceTransitionBarrier(resource, stateBefore, stateAfter, subresource)
            };

            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrierAliasing(ID3D12Resource resourceBefore, ID3D12Resource resourceAfter)
        {
            var barrier = new ResourceBarrier
            {
                Type = ResourceBarrierType.Aliasing,
                Aliasing = new ResourceAliasingBarrier(resourceBefore, resourceAfter)
            };

            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrierUnorderedAccessView(ID3D12Resource resource)
        {
            var barrier = new ResourceBarrier
            {
                Type = ResourceBarrierType.UnorderedAccessView,
                UnorderedAccessView = new ResourceUnorderedAccessViewBarrier(resource)
            };

            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public unsafe void ResourceBarrier(ResourceBarrier barrier)
        {
            ResourceBarrier(1, new IntPtr(&barrier));
        }

        public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 colorRGBA, params InteropRect[] rectangles)
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
            Color4 clearValue,
            params InteropRect[] rectangles)
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
            Int4 clearValue,
            params InteropRect[] rectangles)
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

        public unsafe void RSSetViewport(Viewport viewport)
        {
            RSSetViewports(1, new IntPtr(&viewport));
        }

        public void RSSetViewports(params Viewport[] viewports)
        {
            unsafe
            {
                fixed (void* pViewPorts = viewports)
                {
                    RSSetViewports(viewports.Length, (IntPtr)pViewPorts);
                }
            }
        }

        public unsafe void RSSetScissorRect(InteropRect rectangle)
        {
            RSSetScissorRects(1, new IntPtr(&rectangle));
        }

        public void RSSetScissorRects(params InteropRect[] rectangles)
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
    }
}
