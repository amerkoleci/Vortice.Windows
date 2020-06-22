// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D11
{
    public partial class ID3D11DeviceContext1
    {
        public void ClearView(ID3D11View view, Color4 color)
        {
            ClearView(view, color);
        }

        public unsafe void ClearView(ID3D11View view, Color4 color, RawRect[] rects)
        {
            fixed (RawRect* pRects = rects)
            {
                ClearView(view, color, (IntPtr)pRects, rects.Length);
            }
        }

        public unsafe void ClearView(ID3D11View view, Color4 color, ReadOnlySpan<RawRect> rects)
        {
            fixed (RawRect* pRects = rects)
            {
                ClearView(view, color, (IntPtr)pRects, rects.Length);
            }
        }

        public void ClearView(ID3D11View view, System.Drawing.Color color)
        {
            ClearView(view, new Color4(color));
        }

        public unsafe void ClearView(ID3D11View view, System.Drawing.Color color, RawRect[] rects)
        {
            fixed (RawRect* pRects = rects)
            {
                ClearView(view, new Color4(color), (IntPtr)pRects, rects.Length);
            }
        }

        public unsafe void ClearView(ID3D11View view, System.Drawing.Color color, ReadOnlySpan<RawRect> rects)
        {
            fixed (RawRect* pRects = rects)
            {
                ClearView(view, new Color4(color), (IntPtr)pRects, rects.Length);
            }
        }

        /// <summary>
        /// Discards the specified elements in a resource view from the device context.
        /// </summary>
        /// <param name="view">
        /// An instance of <see cref="ID3D11View"/> for the resource view to discard. 
        /// The resource that underlies the view must have been created with usage <see cref="Usage.Default"/> or <see cref="Usage.Dynamic"/>, otherwise the runtime drops the call to DiscardView1; 
        /// if the debug layer is enabled, the runtime returns an error message.
        /// </param>
        public void DiscardView1(ID3D11View view)
        {
            DiscardView1(view, IntPtr.Zero, 0);
        }

        /// <summary>
        /// Discards the specified elements in a resource view from the device context.
        /// </summary>
        /// <param name="view">
        /// An instance of <see cref="ID3D11View"/> for the resource view to discard. 
        /// The resource that underlies the view must have been created with usage <see cref="Usage.Default"/> or <see cref="Usage.Dynamic"/>, otherwise the runtime drops the call to DiscardView1; 
        /// if the debug layer is enabled, the runtime returns an error message.
        /// </param>
        /// <param name="rects">An array of <see cref="RawRect"/> structures for the rectangles in the resource view to discard.</param>
        public void DiscardView1(ID3D11View view, RawRect[] rects)
        {
            unsafe
            {
                fixed (RawRect* pRects = rects)
                {
                    DiscardView1(view, (IntPtr)pRects, rects.Length);
                }
            }
        }

        /// <summary>
        /// Discards the specified elements in a resource view from the device context.
        /// </summary>
        /// <param name="view">
        /// An instance of <see cref="ID3D11View"/> for the resource view to discard. 
        /// The resource that underlies the view must have been created with usage <see cref="Usage.Default"/> or <see cref="Usage.Dynamic"/>, otherwise the runtime drops the call to DiscardView1; 
        /// if the debug layer is enabled, the runtime returns an error message.
        /// </param>
        /// <param name="rects">An <see cref="ReadOnlySpan{RawRect}"/> for the rectangles in the resource view to discard.</param>
        public void DiscardView1(ID3D11View view, ReadOnlySpan<RawRect> rects)
        {
            unsafe
            {
                fixed (RawRect* pRects = rects)
                {
                    DiscardView1(view, (IntPtr)pRects, rects.Length);
                }
            }
        }

        #region VertexShader
        public void VSSetConstantBuffer1(int slot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
            unsafe
            {
                fixed (void* firstConstantPtr = &firstConstant[0])
                fixed (void* numConstantsPtr = &numConstants[0])
                {
                    VSSetConstantBuffers1(slot, 1, new IntPtr(&nativePtr),
                        (IntPtr)firstConstantPtr,
                        (IntPtr)numConstantsPtr);
                }
            }
        }

        public void VSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            VSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region PixelShader
        public unsafe void PSSetConstantBuffer1(int slot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
            unsafe
            {
                fixed (void* firstConstantPtr = &firstConstant[0])
                fixed (void* numConstantsPtr = &numConstants[0])
                {
                    PSSetConstantBuffers1(slot, 1, new IntPtr(&nativePtr),
                        (IntPtr)firstConstantPtr,
                        (IntPtr)numConstantsPtr);
                }
            }
        }

        public void PSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            PSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region DomainShader
        public void DSSetConstantBuffer1(int slot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
            unsafe
            {
                fixed (void* firstConstantPtr = &firstConstant[0])
                fixed (void* numConstantsPtr = &numConstants[0])
                {
                    DSSetConstantBuffers1(slot, 1, new IntPtr(&nativePtr),
                        (IntPtr)firstConstantPtr,
                        (IntPtr)numConstantsPtr);
                }
            }
        }

        public void DSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            DSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region HullShader
        public void HSSetConstantBuffer1(int slot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
            unsafe
            {
                fixed (void* firstConstantPtr = &firstConstant[0])
                fixed (void* numConstantsPtr = &numConstants[0])
                {
                    HSSetConstantBuffers1(slot, 1, new IntPtr(&nativePtr),
                        (IntPtr)firstConstantPtr,
                        (IntPtr)numConstantsPtr);
                }
            }
        }

        public void HSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            HSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region GeometryShader
        public void GSSetConstantBuffer1(int slot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
            unsafe
            {
                fixed (void* firstConstantPtr = &firstConstant[0])
                fixed (void* numConstantsPtr = &numConstants[0])
                {
                    GSSetConstantBuffers1(slot, 1, new IntPtr(&nativePtr),
                        (IntPtr)firstConstantPtr,
                        (IntPtr)numConstantsPtr);
                }
            }
        }

        public void GSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            GSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region ComputeShader
        public void CSSetConstantBuffer1(int slot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
            unsafe
            {
                fixed (void* firstConstantPtr = &firstConstant[0])
                fixed (void* numConstantsPtr = &numConstants[0])
                {
                    CSSetConstantBuffers1(slot, 1, new IntPtr(&nativePtr),
                        (IntPtr)firstConstantPtr,
                        (IntPtr)numConstantsPtr);
                }
            }
        }

        public void CSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            CSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion
    }
}
