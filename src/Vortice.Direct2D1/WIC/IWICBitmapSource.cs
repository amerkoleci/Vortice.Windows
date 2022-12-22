// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.WIC;

/// <include file="Documentation.xml" path="/comments/comment[@id='IWICBitmapSource']/*" />
public unsafe partial class IWICBitmapSource
{
    public Size Size
    {
        get
        {
            GetSize(out int width, out int height);
            return new(width, height);
        }
    }

    public SizeF Resolution
    {
        get
        {
            GetResolution(out double width, out double height);
            return new((float)width, (float)height);
        }
    }

    public void CopyPixels(int stride, int size, IntPtr data)
    {
        CopyPixels(null, stride, size, data.ToPointer());
    }

    public void CopyPixels(Rectangle rectangle, int stride, int size, IntPtr data)
    {
        CopyPixels(&rectangle, stride, size, data.ToPointer());
    }

    public void CopyPixels(int stride, byte[] data)
    {
        fixed (byte* dataPtr = data)
        {
            CopyPixels(null, stride, data.Length, dataPtr);
        }
    }

    public void CopyPixels(Rectangle rectangle, int stride, byte[] data)
    {
        fixed (byte* dataPtr = &data[0])
        {
            CopyPixels(&rectangle, stride, data.Length, dataPtr);
        }
    }

    public void CopyPixels<T>(int stride, T[] data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(null, stride, data.Length * sizeof(T), dataPtr);
        }
    }

    public void CopyPixels<T>(Rectangle rectangle, int stride, T[] data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(&rectangle, stride, data.Length * sizeof(T), dataPtr);
        }
    }

    public void CopyPixels<T>(int stride, Span<T> data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(null, stride, data.Length * sizeof(T), dataPtr);
        }
    }

    public void CopyPixels<T>(Rectangle rectangle, int stride, Span<T> data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(&rectangle, stride, data.Length * sizeof(T), dataPtr);
        }
    }
}
