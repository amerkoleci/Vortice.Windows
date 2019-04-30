// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpDXGI;
using Vortice.Mathematics;

namespace SharpDirect3D11
{
    public partial class ID3D11DeviceContext1
    {
        public void ClearView(ID3D11View view, Color4 color)
        {
            ClearView(view, color);
        }

        public void ClearView(ID3D11View view, Color4 color, InteropRect[] rects)
        {
            ClearView(view, color, rects, rects.Length);
        }

        public void DiscardView1(ID3D11View view, InteropRect[] rects)
        {
            DiscardView1(view, rects, rects.Length);
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
