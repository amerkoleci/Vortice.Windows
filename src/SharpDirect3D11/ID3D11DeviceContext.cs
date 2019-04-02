// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;
using System.Numerics;
using System.Runtime.CompilerServices;
using SharpDXGI;
using SharpGen.Runtime;

namespace SharpDirect3D11
{
    public partial class ID3D11DeviceContext
    {
        public unsafe void RSSetViewport(ViewportF viewport)
        {
            RSSetViewports(1, new IntPtr(&viewport));
        }

        public void RSSetViewports(params ViewportF[] viewports)
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

        public unsafe void RSSetScissorRects(params RawRectangle[] rectangles)
        {
            fixed (void* pRects = rectangles)
            {
                RSSetScissorRects(rectangles.Length, (IntPtr)pRects);
            }
        }

        public unsafe void OMSetRenderTargets(ID3D11RenderTargetView renderTargetView, ID3D11DepthStencilView depthStencilView = null)
        {
            Guard.NotNull(renderTargetView, nameof(renderTargetView));
            var renderTargetViewPtr = renderTargetView.NativePointer;
            OMSetRenderTargets(1, new IntPtr(&renderTargetViewPtr), depthStencilView);
        }

        public unsafe void OMSetRenderTargets(int renderTargetViewsCount, ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView depthStencilView = null)
        {
            IntPtr* renderTargetViewsPtr = (IntPtr*)0;
            if (renderTargetViewsCount > 0)
            {
                IntPtr* tempPtr = stackalloc IntPtr[renderTargetViewsCount];
                renderTargetViewsPtr = tempPtr;
                for (int i = 0; i < renderTargetViewsCount; i++)
                {
                    renderTargetViewsPtr[i] = (renderTargetViews[i] == null) ? IntPtr.Zero : renderTargetViews[i].NativePointer;
                }
            }

            OMSetRenderTargets(renderTargetViewsCount, (IntPtr)renderTargetViewsPtr, depthStencilView);
        }

        public unsafe void OMSetRenderTargets(ID3D11RenderTargetView[] renderTargetViews, ID3D11DepthStencilView depthStencilView = null)
        {
            var renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
            }

            OMSetRenderTargets(renderTargetViews.Length, (IntPtr)renderTargetViewsPtr, depthStencilView);
        }

        public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(
            ID3D11RenderTargetView renderTargetView,
            ID3D11DepthStencilView depthStencilView,
            int startSlot,
            ID3D11UnorderedAccessView[] unorderedAccessViews)
        {
            Guard.NotNull(renderTargetView, nameof(renderTargetView));
            Guard.NotNullOrEmpty(unorderedAccessViews, nameof(unorderedAccessViews));

            // Marshal array.
            var renderTargetViewsPtr = renderTargetView.NativePointer;

            IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
            int* uavInitialCounts = stackalloc int[unorderedAccessViews.Length];
            for (int i = 0; i < unorderedAccessViews.Length; i++)
            {
                unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
                uavInitialCounts[i] = -1;
            }

            OMSetRenderTargetsAndUnorderedAccessViews(1, renderTargetViewsPtr,
                depthStencilView,
                startSlot, unorderedAccessViews.Length, (IntPtr)unorderedAccessViewsPtr,
                (IntPtr)uavInitialCounts);
        }

        public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(
            ID3D11RenderTargetView[] renderTargetViews,
            ID3D11DepthStencilView depthStencilView,
            int startSlot,
            ID3D11UnorderedAccessView[] unorderedAccessViews)
        {
            Guard.NotNullOrEmpty(renderTargetViews, nameof(renderTargetViews));
            Guard.NotNullOrEmpty(unorderedAccessViews, nameof(unorderedAccessViews));

            // Marshal array.
            var renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
            }

            IntPtr* unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
            int* uavInitialCounts = stackalloc int[unorderedAccessViews.Length];
            for (int i = 0; i < unorderedAccessViews.Length; i++)
            {
                unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
                uavInitialCounts[i] = -1;
            }

            OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViews.Length, (IntPtr)renderTargetViewsPtr,
                depthStencilView,
                startSlot, unorderedAccessViews.Length, (IntPtr)unorderedAccessViewsPtr,
                (IntPtr)uavInitialCounts);
        }

        public unsafe void OMSetRenderTargetsAndUnorderedAccessViews(
            ID3D11RenderTargetView[] renderTargetViews,
            ID3D11DepthStencilView depthStencilView,
            int startSlot,
            ID3D11UnorderedAccessView[] unorderedAccessViews,
            int[] uavInitialCounts)
        {
            Guard.NotNullOrEmpty(renderTargetViews, nameof(renderTargetViews));
            Guard.NotNullOrEmpty(unorderedAccessViews, nameof(unorderedAccessViews));
            Guard.NotNullOrEmpty(uavInitialCounts, nameof(uavInitialCounts));

            // Marshal array.
            var renderTargetViewsPtr = stackalloc IntPtr[renderTargetViews.Length];
            for (int i = 0; i < renderTargetViews.Length; i++)
            {
                renderTargetViewsPtr[i] = renderTargetViews[i].NativePointer;
            }

            var unorderedAccessViewsPtr = stackalloc IntPtr[unorderedAccessViews.Length];
            for (int i = 0; i < unorderedAccessViews.Length; i++)
            {
                unorderedAccessViewsPtr[i] = unorderedAccessViews[i].NativePointer;
            }

            OMSetRenderTargetsAndUnorderedAccessViews(renderTargetViews.Length, (IntPtr)renderTargetViewsPtr,
                depthStencilView,
                startSlot, unorderedAccessViews.Length, (IntPtr)unorderedAccessViewsPtr,
                (IntPtr)Unsafe.AsPointer(ref uavInitialCounts[0]));
        }

        public ID3D11CommandList FinishCommandList(bool restoreState)
        {
            var result = new ID3D11CommandList();
            FinishCommandListInternal(restoreState, result).CheckError();
            return result;
        }

        public Result FinishCommandList(bool restoreState, ID3D11CommandList commandList)
        {
            return FinishCommandListInternal(restoreState, commandList);
        }
    }
}
