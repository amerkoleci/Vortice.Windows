// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Numerics;
using SharpDXGI;
using SharpGen.Runtime;

namespace SharpD3D11
{
    public partial class ID3D11DeviceContext
    {
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

        public void ClearRenderTargetView(ID3D11RenderTargetView renderTargetView, Color color)
        {
            Guard.NotNull(renderTargetView, nameof(renderTargetView));

            var colorRGBA = new Vector4(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
            ClearRenderTargetView(renderTargetView, colorRGBA);
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
