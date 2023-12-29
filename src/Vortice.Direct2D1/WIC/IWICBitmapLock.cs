// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public unsafe partial class IWICBitmapLock
{
    public SizeI Size
    {
        get
        {
            GetSize(out int width, out int height);
            return new(width, height);
        }
    }

    /// <summary>
    /// Gets a <see cref="DataRectangle"/> to the data.
    /// </summary>
    public DataRectangle Data
    {
        get
        {
            IntPtr pointer = GetDataPointer(out _);
            return new DataRectangle(pointer, Stride);
        }
    }

    public Span<byte> GetDataPointer()
    {
        IntPtr pointer = GetDataPointer(out int size);
        return new(pointer.ToPointer(), size);
    }

    public Span<T> GetDataPointer<T>() where T : unmanaged
    {
        IntPtr pointer = GetDataPointer(out int size);
        return new(pointer.ToPointer(), size / sizeof(T));
    }
}
