// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.WIC;

public partial class IWICBitmapLock
{
    public SizeI Size
    {
        get
        {
            GetSize(out int width, out int height);
            return new (width, height);
        }
    }

    /// <summary>
    /// Gets a <see cref="DataRectangle"/> to the data.
    /// </summary>
    public DataRectangle Data
    {
        get
        {
            var pointer = GetDataPointer(out int size);
            return new DataRectangle(pointer, Stride);
        }
    }

    public unsafe Span<byte> GetDataPointer()
    {
        IntPtr pointer = GetDataPointer(out int size);
        return new Span<byte>(pointer.ToPointer(), size);
    }

    public unsafe Span<T> GetDataPointer<T>() where T : unmanaged
    {
        IntPtr pointer = GetDataPointer(out int size);
        return new Span<T>(pointer.ToPointer(), size / sizeof(T));
    }
}
