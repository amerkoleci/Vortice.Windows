// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DSwapChain9
{
    /// <summary>
    /// Retrieves a back buffer from the swap chain of the device.
    /// </summary>
    /// <param name="backBuffer">
    /// Index of the back buffer object to return. Back buffers are numbered from 0 to the total number of back buffers - 1. A value of 0 returns the first back buffer, not the front buffer.
    /// </param>
    /// <returns>The back buffer from the swap chain of the device.</returns>
    public IDirect3DSurface9 GetBackBuffer(int backBuffer)
    {
        return GetBackBuffer(backBuffer, BackBufferType.Mono);
    }

    /// <summary>
    /// Presents the contents of the next buffer in the sequence of back buffers to the screen.
    /// </summary>
    /// <param name="presentFlags">The present flags.</param>
    public void Present(Present presentFlags)
    {
        Present(null, null, IntPtr.Zero, null, (int)presentFlags);
    }

    /// <summary>
    /// Presents the contents of the next buffer in the sequence of back buffers to the screen.
    /// </summary>
    /// <param name="presentFlags">The present flags.</param>
    /// <param name="sourceRectangle">The area of the back buffer that should be presented.</param>
    /// <param name="destinationRectangle">The area of the front buffer that should receive the result of the presentation.</param>
    /// <unmanaged>HRESULT IDirect3DSwapChain9::Present([In, Optional] const void* pSourceRect,[InOut, Optional] const void* pDestRect,[In] HWND hDestWindowOverride,[In] const RGNDATA* pDirtyRegion,[In] unsigned int dwFlags)</unmanaged>
    public void Present(Rect sourceRectangle, Rect destinationRectangle, Present presentFlags)
    {
        Present(&sourceRectangle, &destinationRectangle, IntPtr.Zero, null, (int)presentFlags);
    }

    /// <summary>
    /// Presents the contents of the next buffer in the sequence of back buffers to the screen.
    /// </summary>
    /// <param name="presentFlags">The present flags.</param>
    /// <param name="sourceRectangle">The area of the back buffer that should be presented.</param>
    /// <param name="destinationRectangle">The area of the front buffer that should receive the result of the presentation.</param>
    /// <param name="windowOverride">The destination window whose client area is taken as the target for this presentation.</param>
    /// <unmanaged>HRESULT IDirect3DSwapChain9::Present([In, Optional] const void* pSourceRect,[InOut, Optional] const void* pDestRect,[In] HWND hDestWindowOverride,[In] const RGNDATA* pDirtyRegion,[In] unsigned int dwFlags)</unmanaged>
    public void Present(Rect sourceRectangle, Rect destinationRectangle, IntPtr windowOverride, Present presentFlags)
    {
        Present(&sourceRectangle, &destinationRectangle, windowOverride, null, (int)presentFlags);
    }
}
