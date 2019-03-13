// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Vortice.Direct3D12
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

        public void ClearRenderTargetView(CpuDescriptorHandle renderTargetView, Color4 colorRGBA, params RawRectangle[] rectangles)
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

        public void ClearDepthStencilView(CpuDescriptorHandle depthStencilView, ClearFlags clearFlags, float depth, byte stencil, params RawRectangle[] rectangles)
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
