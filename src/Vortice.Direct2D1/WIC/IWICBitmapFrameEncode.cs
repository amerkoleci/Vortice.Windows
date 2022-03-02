// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public unsafe partial class IWICBitmapFrameEncode
{
    public Result Initialize() => Initialize(null);

    /// <summary>
    /// Sets the <see cref="IWICColorContext"/> objects for this frame encoder.
    /// </summary>
    /// <param name="colorContexts">The color contexts to set for the encoder.</param>
    public void SetColorContexts(IWICColorContext[] colorContexts)
    {
        SetColorContexts(colorContexts != null ? colorContexts.Length : 0, colorContexts);
    }

    public Result SetSize(in SizeI size) => SetSize(size.Width, size.Height);

    /// <summary>
    /// Requests that the encoder use the specified pixel format.
    /// </summary>
    /// <param name="pixelFormat">
    /// On input, the requested pixel format GUID. On output, the closest pixel format GUID supported by the encoder; this may be different than the requested format.
    /// For a list of pixel format GUIDs, see <see cref="PixelFormat"/> class.
    /// </param>
    public void SetPixelFormat(Guid pixelFormat)
    {
        SetPixelFormat(ref pixelFormat);
    }

    /// <summary>
    /// Sets the output image dimensions for the frame.
    /// </summary>
    /// <param name="size">The width and height of the output image.</param>
    public void SetSize(SizeI size)
    {
        SetSize(size.Width, size.Height);
    }

    #region WritePixels
    public Result WritePixels(int lineCount, DataRectangle buffer, int totalSizeInBytes = 0)
    {
        return WritePixels(lineCount, buffer.DataPointer, buffer.Pitch, totalSizeInBytes);
    }

    public Result WritePixels(int lineCount, IntPtr buffer, int stride, int totalSizeInBytes = 0)
    {
        if (totalSizeInBytes == 0)
        {
            totalSizeInBytes = lineCount * stride;
        }

        return WritePixels(lineCount, stride, totalSizeInBytes, buffer.ToPointer());
    }

    public Result WritePixels<T>(int lineCount, int stride, T[] source) where T : unmanaged
    {
        ReadOnlySpan<T> span = source.AsSpan();

        return WritePixels(lineCount, stride, span);
    }

    public Result WritePixels<T>(int lineCount, int stride, ReadOnlySpan<T> source) where T : unmanaged
    {
        return WritePixels(lineCount, ref MemoryMarshal.GetReference(source), stride);
    }

    public Result WritePixels<T>(int lineCount, ref T source, int stride, int totalSizeInBytes = 0) where T : unmanaged
    {
        if (totalSizeInBytes == 0)
        {
            totalSizeInBytes = lineCount * stride;
        }

        fixed (void* sourcePointer = &source)
        {
            return WritePixels(lineCount, stride, totalSizeInBytes, sourcePointer);
        }
    }

    public Result WritePixels(int lineCount, int stride, int bufferSize, IntPtr pixels)
    {
        return WritePixels(lineCount, stride, bufferSize, pixels);
    }
    #endregion WritePixels

    /// <summary>
    /// Encodes a bitmap source.
    /// </summary>
    /// <param name="bitmapSource">The bitmap source to encode.</param>
    public Result WriteSource(IWICBitmapSource bitmapSource) => WriteSource(bitmapSource, null);

    /// <summary>
    /// Encodes a bitmap source.
    /// </summary>
    /// <param name="bitmapSource">The bitmap source to encode.</param>
    /// <param name="rectangle">The size rectangle of the bitmap source.</param>
    public Result WriteSource(IWICBitmapSource bitmapSource, RectI rectangle) => WriteSource(bitmapSource, &rectangle);
}
