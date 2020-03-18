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
            ClearView(view, color, (IntPtr)Unsafe.AsPointer(ref rects[0]), rects.Length);
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
            ClearView(view, new Color4(color), (IntPtr)Unsafe.AsPointer(ref rects[0]), rects.Length);
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
                DiscardView1(view, (IntPtr)Unsafe.AsPointer(ref rects[0]), rects.Length);
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
        public unsafe void VSSetConstantBuffer1(int startSlot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            VSSetConstantBuffers1(startSlot,
                1,
                new IntPtr(&constantBufferPtr),
                (IntPtr)Unsafe.AsPointer(ref firstConstant[0]),
                (IntPtr)Unsafe.AsPointer(ref numConstants[0])
                );
        }

        public void VSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            VSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region PixelShader
        public unsafe void PSSetConstantBuffer1(int startSlot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            PSSetConstantBuffers1(startSlot,
                1,
                new IntPtr(&constantBufferPtr),
                (IntPtr)Unsafe.AsPointer(ref firstConstant[0]),
                (IntPtr)Unsafe.AsPointer(ref numConstants[0])
                );
        }

        public void PSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            PSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region DomainShader
        public unsafe void DSSetConstantBuffer1(int startSlot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            DSSetConstantBuffers1(startSlot,
                1,
                new IntPtr(&constantBufferPtr),
                (IntPtr)Unsafe.AsPointer(ref firstConstant[0]),
                (IntPtr)Unsafe.AsPointer(ref numConstants[0])
                );
        }

        public void DSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            DSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region HullShader
        public unsafe void HSSetConstantBuffer1(int startSlot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            HSSetConstantBuffers1(startSlot,
                1,
                new IntPtr(&constantBufferPtr),
                (IntPtr)Unsafe.AsPointer(ref firstConstant[0]),
                (IntPtr)Unsafe.AsPointer(ref numConstants[0])
                );
        }

        public void HSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            HSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region GeometryShader
        public unsafe void GSSetConstantBuffer1(int startSlot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            GSSetConstantBuffers1(startSlot,
                1,
                new IntPtr(&constantBufferPtr),
                (IntPtr)Unsafe.AsPointer(ref firstConstant[0]),
                (IntPtr)Unsafe.AsPointer(ref numConstants[0])
                );
        }

        public void GSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            GSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion

        #region CompueShader
        public unsafe void CSSetConstantBuffer1(int startSlot, ID3D11Buffer constantBuffer, int[] firstConstant, int[] numConstants)
        {
            var constantBufferPtr = constantBuffer.NativePointer;
            CSSetConstantBuffers1(startSlot,
                1,
                new IntPtr(&constantBufferPtr),
                (IntPtr)Unsafe.AsPointer(ref firstConstant[0]),
                (IntPtr)Unsafe.AsPointer(ref numConstants[0])
                );
        }

        public void CSSetConstantBuffers1(int startSlot, ID3D11Buffer[] constantBuffers, int[] firstConstant, int[] numConstants)
        {
            CSSetConstantBuffers1(startSlot, constantBuffers.Length, constantBuffers, firstConstant, numConstants);
        }
        #endregion
    }
}
