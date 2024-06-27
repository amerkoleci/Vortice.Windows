// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

/// <summary>
/// Provides access to subresource data.
/// </summary>
public unsafe partial struct MappedSubresource
{
    /// <summary>
    /// Create a new instance of <see cref="MappedSubresource"/> struct.
    /// </summary>
    /// <param name="dataPointer">Pointer to the data</param>
    /// <param name="rowPitch">The row pitch, or width, or physical size (in bytes) of the data.</param>
    /// <param name="depthPitch">The depth pitch, or width, or physical size (in bytes)of the data.</param>
    public MappedSubresource(IntPtr dataPointer, int rowPitch, int depthPitch)
    {
        DataPointer = dataPointer;
        RowPitch = rowPitch;
        DepthPitch = depthPitch;
    }

    /// <summary>
    /// Create a new instance of <see cref="MappedSubresource"/> struct.
    /// </summary>
    /// <param name="dataPointer">Pointer to the data</param>
    public MappedSubresource(IntPtr dataPointer)
    {
        DataPointer = dataPointer;
        RowPitch = DepthPitch = 0;
    }

    public Span<byte> AsSpan(int length) => new(DataPointer.ToPointer(), length);

    public Span<T> AsSpan<T>(int length) where T : unmanaged
    {
        return new Span<T>(DataPointer.ToPointer(), length);
    }

    public Span<T> AsSpan<T>(ID3D11Buffer buffer) where T : unmanaged
    {
        Span<byte> source = new(DataPointer.ToPointer(), buffer.Description.ByteWidth);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> AsSpan<T>(ID3D11Texture1D resource, int mipSlice, int arraySlice) where T : unmanaged
    {
        resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
        Span<byte> source = new(DataPointer.ToPointer(), mipSize * RowPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> AsSpan<T>(ID3D11Texture2D resource, int mipSlice, int arraySlice) where T : unmanaged
    {
        resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
        Span<byte> source = new Span<byte>(DataPointer.ToPointer(), mipSize * RowPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }

    public Span<T> AsSpan<T>(ID3D11Texture3D resource, int mipSlice, int arraySlice) where T : unmanaged
    {
        resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
        Span<byte> source = new(DataPointer.ToPointer(), mipSize * DepthPitch);
        return MemoryMarshal.Cast<byte, T>(source);
    }
}
