// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

/// <include file="Documentation.xml" path="/comments/comment[@id='IWICBitmapSource']/*" />
public unsafe partial class IWICBitmapSource
{
    public SizeI Size
    {
        get
        {
            GetSize(out uint width, out uint height);
            return new((int)width, (int)height);
        }
    }

    public Size Resolution
    {
        get
        {
            GetResolution(out double width, out double height);
            return new((float)width, (float)height);
        }
    }

    public void CopyPixels(uint stride, uint size, IntPtr data)
    {
        CopyPixels(null, stride, size, data.ToPointer());
    }

    public void CopyPixels(RectI rectangle, uint stride, uint size, IntPtr data)
    {
        CopyPixels(&rectangle, stride, size, data.ToPointer());
    }

    public void CopyPixels(uint stride, byte[] data)
    {
        fixed (byte* dataPtr = data)
        {
            CopyPixels(null, stride, (uint)data.Length, dataPtr);
        }
    }

    public void CopyPixels(RectI rectangle, uint stride, byte[] data)
    {
        fixed (byte* dataPtr = &data[0])
        {
            CopyPixels(&rectangle, stride, (uint)data.Length, dataPtr);
        }
    }

    public void CopyPixels<T>(uint stride, T[] data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(null, stride, (uint)(data.Length * sizeof(T)), dataPtr);
        }
    }

    public void CopyPixels<T>(RectI rectangle, uint stride, T[] data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(&rectangle, stride, (uint)(data.Length * sizeof(T)), dataPtr);
        }
    }

    public void CopyPixels<T>(uint stride, Span<T> data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(null, stride, (uint)(data.Length * sizeof(T)), dataPtr);
        }
    }

    public void CopyPixels<T>(RectI rectangle, uint stride, Span<T> data) where T : unmanaged
    {
        fixed (T* dataPtr = data)
        {
            CopyPixels(&rectangle, stride, (uint)(data.Length * sizeof(T)), dataPtr);
        }
    }
}
