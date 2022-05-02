// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1Bitmap
{
    public Size Dpi
    {
        get
        {
            GetDpi(out float dpiX, out float dpiY);
            return new(dpiX, dpiY);
        }
    }

    #region CopyFromBitmap
    /// <summary>
    /// Copies the specified bitmap into the current bitmap.
    /// </summary>
    /// <param name="sourceBitmap">The bitmap to copy from.</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromBitmap(ID2D1Bitmap sourceBitmap) => CopyFromBitmap(null, sourceBitmap, null);

    /// <summary>
    /// Copies the specified bitmap into the current bitmap.
    /// </summary>
    /// <param name="destinationPoint">In the current bitmap, the upper-left corner of the area to which the data is copied</param>
    /// <param name="sourceBitmap">The bitmap to copy from.</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromBitmap(Int2 destinationPoint, ID2D1Bitmap sourceBitmap) => CopyFromBitmap(&destinationPoint, sourceBitmap, null);

    /// <summary>
    /// Copies the specified region from the specified bitmap into the current bitmap.
    /// </summary>
    /// <param name="destinationPoint">
    /// In the current bitmap, the upper-left corner of the area to which the region specified by sourceRectangle is copied.</param>
    /// <param name="sourceBitmap">The bitmap to copy from.</param>
    /// <param name="sourceRectangle">The area of bitmap to copy.</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromBitmap(Int2 destinationPoint, ID2D1Bitmap sourceBitmap, RectI sourceRectangle)
    {
        RawRect sourceRect = sourceRectangle;
        return CopyFromBitmap(&destinationPoint, sourceBitmap, &sourceRect);
    }
    #endregion CopyFromBitmap

    #region CopyFromMemory
    /// <summary>
    /// Copies from memory into the current bitmap.
    /// </summary>
    /// <param name="pointer">The data to copy.</param>
    /// <param name="pitch">The stride, or pitch, of the source bitmap stored in srcData. The stride is the byte count of a scanline (one row of pixels in memory).</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromMemory(IntPtr pointer, int pitch)
    {
        return CopyFromMemory(null, pointer.ToPointer(), pitch);
    }

    public Result CopyFromMemory(byte[] source, int pitch)
    {
        fixed (byte* sourcePointer = source)
        {
            return CopyFromMemory(null, sourcePointer, pitch);
        }
    }

    public Result CopyFromMemory<T>(T[] source, int pitch = 0) where T : unmanaged
    {
        ReadOnlySpan<T> span = source.AsSpan();

        return CopyFromMemory(span, pitch);
    }

    public Result CopyFromMemory<T>(ReadOnlySpan<T> source, int pitch = 0) where T : unmanaged
    {
        return CopyFromMemory(ref MemoryMarshal.GetReference(source), pitch);
    }

    public Result CopyFromMemory<T>(ref T source, int pitch = 0) where T : unmanaged
    {
        if (pitch == 0)
            pitch = PixelSize.Width * sizeof(T);

        fixed (void* sourcePointer = &source)
        {
            return CopyFromMemory(null, sourcePointer, pitch);
        }
    }

    /// <summary>
    /// Copies the specified region from memory into the current bitmap.
    /// </summary>
    /// <param name="destinationRect">In the current bitmap, the rectangle to which the region specified by srcRect is copied.</param>
    /// <param name="data">The data to copy.</param>
    /// <param name="pitch">The stride, or pitch, of the source bitmap stored in srcData. The stride is the byte count of a scanline (one row of pixels in memory).</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromMemory(RectI destinationRect, byte[] data, int pitch)
    {
        RawRect dstRect = destinationRect;

        fixed (byte* dataPtr = data)
        {
            return CopyFromMemory(&dstRect, dataPtr, pitch);
        }
    }

    public Result CopyFromMemory<T>(RectI destinationRect, T[] source, int pitch = 0) where T : unmanaged
    {
        ReadOnlySpan<T> span = source.AsSpan();
        return CopyFromMemory(destinationRect, span, pitch);
    }

    public Result CopyFromMemory<T>(RectI destinationRect, ReadOnlySpan<T> source, int pitch = 0) where T : unmanaged
    {
        return CopyFromMemory(destinationRect, ref MemoryMarshal.GetReference(source), pitch);
    }

    public Result CopyFromMemory<T>(RectI destinationRect, ref T source, int pitch = 0) where T : unmanaged
    {
        RawRect dstRect = destinationRect;

        if (pitch == 0)
        {
            pitch = PixelSize.Width * sizeof(T);
        }

        fixed (void* sourcePointer = &source)
        {
            return CopyFromMemory(&dstRect, sourcePointer, pitch);
        }
    }
    #endregion CopyFromMemory

    #region CopyFromRenderTarget
    /// <summary>
    /// Copies the specified render target into the current bitmap.
    /// </summary>
    /// <param name="renderTarget">The render target to copy.</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromRenderTarget(ID2D1RenderTarget renderTarget)
    {
        return CopyFromRenderTarget(null, renderTarget, null);
    }

    /// <summary>
    /// Copies the specified render target into the current bitmap.
    /// </summary>
    /// <param name="destinationPoint">In the current bitmap, the upper-left corner of the area to which the data is copied.</param>
    /// <param name="renderTarget">The render target to copy.</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromRenderTarget(Int2 destinationPoint, ID2D1RenderTarget renderTarget)
    {
        return CopyFromRenderTarget(&destinationPoint, renderTarget, null);
    }

    /// <summary>
    /// Copies the specified region from the specified render target into the current bitmap.
    /// </summary>
    /// <param name="destinationPoint">In the current bitmap, the upper-left corner of the area to which the region specified by sourceRectangle is copied.</param>
    /// <param name="renderTarget">The render target that contains the region to copy.</param>
    /// <param name="sourceRectangle">The area of renderTarget to copy.</param>
    /// <returns>The result of the operation.</returns>
    public Result CopyFromRenderTarget(Int2 destinationPoint, ID2D1RenderTarget renderTarget, RectI sourceRectangle)
    {
        RawRect sourceRect = sourceRectangle;
        return CopyFromRenderTarget(&destinationPoint, renderTarget, &sourceRect);
    }
    #endregion CopyFromRenderTarget
}
