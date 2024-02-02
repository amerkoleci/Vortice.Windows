// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11Buffer
{
    public void SetData<T>(ID3D11DeviceContext context, T[] source, MapMode mode, MapFlags flags = MapFlags.None, int offsetInBytes = 0)
        where T : unmanaged
    {
        ReadOnlySpan<T> span = source.AsSpan();
        SetData(context, span, mode, flags, offsetInBytes);
    }

    public void SetData<T>(ID3D11DeviceContext context, ReadOnlySpan<T> source, MapMode mode, MapFlags flags = MapFlags.None, int offsetInBytes = 0)
        where T : unmanaged
    {
        context.Map(this, 0, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        fixed (T* sourcePtr = source)
        {
            Unsafe.CopyBlockUnaligned((byte*)mappedSubresource.DataPointer.ToPointer() + offsetInBytes, sourcePtr, (uint)(source.Length * sizeof(T)));
        }
        context.Unmap(this, 0);
    }

    public void SetData<T>(ID3D11DeviceContext context, in T source, MapMode mode, MapFlags flags = MapFlags.None, int offsetInBytes = 0)
        where T : unmanaged
    {
        context.Map(this, 0, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        fixed (void* sourcePointer = &source)
        {
            Unsafe.CopyBlockUnaligned((byte*)mappedSubresource.DataPointer.ToPointer() + offsetInBytes, sourcePointer, (uint)sizeof(T));
        }
        context.Unmap(this, 0);
    }

    public T GetData<T>(ID3D11DeviceContext context, int offsetInBytes = 0, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None)
        where T : unmanaged
    {
        T data = new();
        GetData(context, ref data, (uint)sizeof(T), offsetInBytes, mode, flags);
        return data;
    }

    public void GetData<T>(ID3D11DeviceContext context, Span<T> destination, int offsetInBytes = 0, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None)
        where T : unmanaged
    {
        GetData(context, ref MemoryMarshal.GetReference(destination), (uint)(destination.Length * sizeof(T)), offsetInBytes, mode, flags);
    }

    public T[] GetArray<T>(ID3D11DeviceContext context, int offsetInBytes = 0, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None)
        where T : unmanaged
    {
        T[] data = new T[(Description.ByteWidth / sizeof(T)) - offsetInBytes];
        GetData(context, data.AsSpan(), offsetInBytes, mode, flags);

        return data;
    }

    public void GetData<T>(ID3D11DeviceContext context, ref T destination, uint sizeInBytes, int offsetInBytes = 0, MapMode mode = MapMode.Read, MapFlags flags = MapFlags.None)
        where T : unmanaged
    {
        context.Map(this, 0, mode, flags, out MappedSubresource mappedSubresource).CheckError();
        fixed (T* destinationPointer = &destination)
        {
            Unsafe.CopyBlockUnaligned(destinationPointer, (byte*)mappedSubresource.DataPointer.ToPointer() + offsetInBytes, sizeInBytes);
        }
        context.Unmap(this, 0);
    }
}
