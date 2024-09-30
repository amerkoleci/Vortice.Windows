// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11DeviceContext1
{
    public void ClearView(ID3D11View view, Color4 color)
    {
        ClearView(view, color);
    }

    public void ClearView(ID3D11View view, Color4 color, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearView(view, color, pRects, (uint)rects.Length);
        }
    }

    public void ClearView(ID3D11View view, Color4 color, Span<RawRect> rects)
    {
        fixed (RawRect* pRects = rects)
        {
            ClearView(view, color, pRects, (uint)rects.Length);
        }
    }

    /// <summary>
    /// Discards the specified elements in a resource view from the device context.
    /// </summary>
    /// <param name="view">
    /// An instance of <see cref="ID3D11View"/> for the resource view to discard. 
    /// The resource that underlies the view must have been created with usage <see cref="ResourceUsage.Default"/> or <see cref="ResourceUsage.Dynamic"/>, otherwise the runtime drops the call to DiscardView1; 
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
    /// The resource that underlies the view must have been created with usage <see cref="ResourceUsage.Default"/> or <see cref="ResourceUsage.Dynamic"/>, otherwise the runtime drops the call to DiscardView1; 
    /// if the debug layer is enabled, the runtime returns an error message.
    /// </param>
    /// <param name="rects">An array of <see cref="RawRect"/> structures for the rectangles in the resource view to discard.</param>
    public void DiscardView1(ID3D11View view, RawRect[] rects)
    {
        fixed (RawRect* pRects = rects)
        {
            DiscardView1(view, (IntPtr)pRects, (uint)rects.Length);
        }
    }

    /// <summary>
    /// Discards the specified elements in a resource view from the device context.
    /// </summary>
    /// <param name="view">
    /// An instance of <see cref="ID3D11View"/> for the resource view to discard. 
    /// The resource that underlies the view must have been created with usage <see cref="ResourceUsage.Default"/> or <see cref="ResourceUsage.Dynamic"/>, otherwise the runtime drops the call to DiscardView1; 
    /// if the debug layer is enabled, the runtime returns an error message.
    /// </param>
    /// <param name="rects">An <see cref="ReadOnlySpan{RawRect}"/> for the rectangles in the resource view to discard.</param>
    public void DiscardView1(ID3D11View view, ReadOnlySpan<RawRect> rects)
    {
        fixed (RawRect* pRects = rects)
        {
            DiscardView1(view, (IntPtr)pRects, (uint)rects.Length);
        }
    }

    #region VertexShader
    public void VSSetConstantBuffer1(uint slot, ID3D11Buffer? constantBuffer, ReadOnlySpan<uint> firstConstant, ReadOnlySpan<uint> numConstants)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            VSSetConstantBuffers1(slot, 1, &nativePtr, firstConstantPtr, numConstantsPtr);
        }
    }

    public void VSSetConstantBuffers1(uint startSlot, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint> firstConstant, ReadOnlySpan<uint> numConstants)
    {
        VSSetConstantBuffers1(startSlot, (uint)constantBuffers.Length, constantBuffers, firstConstant, numConstants);
    }

    public void VSSetConstantBuffers1(uint startSlot, uint count, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint> firstConstant, ReadOnlySpan<uint> numConstants)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
            VSSetConstantBuffers1(startSlot, count, ppConstantBuffers, firstConstantPtr, numConstantsPtr);
    }
    #endregion

    #region PixelShader
    public void PSSetConstantBuffer1(uint slot, ID3D11Buffer? constantBuffer, ReadOnlySpan<uint> firstConstant, ReadOnlySpan<uint> numConstants)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            PSSetConstantBuffers1(slot, 1, &nativePtr, firstConstantPtr, numConstantsPtr);
        }
    }

    public void PSSetConstantBuffers1(uint startSlot, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint> firstConstant, ReadOnlySpan<uint> numConstants)
    {
        PSSetConstantBuffers1(startSlot, (uint)constantBuffers.Length, constantBuffers, firstConstant, numConstants);
    }

    public void PSSetConstantBuffers1(uint startSlot, uint count, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint> firstConstant, ReadOnlySpan<uint> numConstants)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            PSSetConstantBuffers1(startSlot, count, ppConstantBuffers, firstConstantPtr, numConstantsPtr);
        }
    }
    #endregion

    #region DomainShader
    public void DSSetConstantBuffer1(uint slot, ID3D11Buffer? constantBuffer, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            DSSetConstantBuffers1(slot, 1, &nativePtr, firstConstantPtr, numConstantsPtr);
        }
    }

    public void DSSetConstantBuffers1(uint startSlot, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        DSSetConstantBuffers1(startSlot, (uint)constantBuffers.Length, constantBuffers, firstConstant, numConstants);
    }

    public void DSSetConstantBuffers1(uint startSlot, uint count, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            DSSetConstantBuffers1(startSlot, count, ppConstantBuffers, firstConstantPtr, numConstantsPtr);
        }
    }
    #endregion

    #region HullShader
    public void HSSetConstantBuffer1(uint slot, ID3D11Buffer? constantBuffer, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            HSSetConstantBuffers1(slot, 1, &nativePtr, firstConstantPtr, numConstantsPtr);
        }
    }

    public void HSSetConstantBuffers1(uint startSlot, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        HSSetConstantBuffers1(startSlot, (uint)constantBuffers.Length, constantBuffers, firstConstant, numConstants);
    }

    public void HSSetConstantBuffers1(uint startSlot, uint count, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            HSSetConstantBuffers1(startSlot, count, ppConstantBuffers, firstConstantPtr, numConstantsPtr);
        }
    }
    #endregion

    #region GeometryShader
    public void GSSetConstantBuffer1(uint slot, ID3D11Buffer? constantBuffer, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            GSSetConstantBuffers1(slot, 1, &nativePtr, firstConstantPtr, numConstantsPtr);
        }
    }

    public void GSSetConstantBuffers1(uint startSlot, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        GSSetConstantBuffers1(startSlot, (uint)constantBuffers.Length, constantBuffers, firstConstant, numConstants);
    }

    public void GSSetConstantBuffers1(uint startSlot, uint count, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            GSSetConstantBuffers1(startSlot, count, ppConstantBuffers, firstConstantPtr, numConstantsPtr);
        }
    }
    #endregion

    #region ComputeShader
    public void CSSetConstantBuffer1(uint slot, ID3D11Buffer? constantBuffer, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr nativePtr = constantBuffer == null ? IntPtr.Zero : constantBuffer.NativePointer;
        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            CSSetConstantBuffers1(slot, 1, &nativePtr, firstConstantPtr, numConstantsPtr);
        }
    }

    public void CSSetConstantBuffers1(uint startSlot, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        CSSetConstantBuffers1(startSlot, (uint)constantBuffers.Length, constantBuffers, firstConstant, numConstants);
    }

    public void CSSetConstantBuffers1(uint startSlot, uint count, ID3D11Buffer[] constantBuffers, ReadOnlySpan<uint>firstConstant, ReadOnlySpan<uint>numConstants)
    {
        IntPtr* ppConstantBuffers = stackalloc IntPtr[(int)count];

        for (int i = 0; i < count; i++)
        {
            ppConstantBuffers[i] = (constantBuffers[i] == null) ? IntPtr.Zero : constantBuffers[i].NativePointer;
        }

        fixed (uint* firstConstantPtr = firstConstant)
        fixed (uint* numConstantsPtr = numConstants)
        {
            CSSetConstantBuffers1(startSlot, count, ppConstantBuffers, firstConstantPtr, numConstantsPtr);
        }
    }
    #endregion
}
