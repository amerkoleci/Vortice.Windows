// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public unsafe partial class IWICBitmapLock
{
    public SizeI Size
    {
        get
        {
            GetSize(out uint width, out uint height);
            return new((int)width, (int)height);
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
        IntPtr pointer = GetDataPointer(out uint size);
        return new(pointer.ToPointer(), (int)size);
    }

    public Span<T> GetDataPointer<T>() where T : unmanaged
    {
        IntPtr pointer = GetDataPointer(out uint size);
        return new(pointer.ToPointer(), (int)(size / sizeof(T)));
    }
}
