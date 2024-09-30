// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12;

public partial struct ResourceDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceDescription"/> struct.
    /// </summary>
    /// <param name="dimension"></param>
    /// <param name="alignment"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="depthOrArraySize"></param>
    /// <param name="mipLevels"></param>
    /// <param name="format"></param>
    /// <param name="sampleCount"></param>
    /// <param name="sampleQuality"></param>
    /// <param name="layout"></param>
    /// <param name="flags"></param>
    public ResourceDescription(
        ResourceDimension dimension,
        ulong alignment,
        ulong width,
        uint height,
        ushort depthOrArraySize,
        ushort mipLevels,
        Format format,
        uint sampleCount,
        uint sampleQuality,
        TextureLayout layout,
        ResourceFlags flags)
    {
        Dimension = dimension;
        Alignment = alignment;
        Width = width;
        Height = height;
        DepthOrArraySize = depthOrArraySize;
        MipLevels = mipLevels;
        Format = format;
        SampleDescription = new SampleDescription(sampleCount, sampleQuality);
        Layout = layout;
        Flags = flags;
    }

    public static ResourceDescription Buffer(ResourceAllocationInfo resourceAllocInfo, ResourceFlags flags = ResourceFlags.None)
    {
        return new ResourceDescription(
            ResourceDimension.Buffer,
            resourceAllocInfo.Alignment,
            resourceAllocInfo.SizeInBytes,
            1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags);
    }

    public static ResourceDescription Buffer(
        ulong sizeInBytes,
        ResourceFlags flags = ResourceFlags.None,
        ulong alignment = 0)
    {
        return new ResourceDescription(ResourceDimension.Buffer, alignment, sizeInBytes, 1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags);
    }

    public static ResourceDescription Texture1D(Format format,
        uint width,
        ushort arraySize = 1,
        ushort mipLevels = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        ulong alignment = 0)
    {
        return new ResourceDescription(ResourceDimension.Texture1D, alignment, width, 1, arraySize, mipLevels, format, 1, 0, layout, flags);
    }

    public static ResourceDescription Texture2D(Format format,
        uint width,
        uint height,
        ushort arraySize = 1,
        ushort mipLevels = 0,
        uint sampleCount = 1,
        uint sampleQuality = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        ulong alignment = 0)
    {
        return new ResourceDescription(ResourceDimension.Texture2D,
            alignment,
            width,
            height,
            arraySize,
            mipLevels,
            format,
            sampleCount,
            sampleQuality,
            layout,
            flags);
    }

    public static ResourceDescription Texture3D(Format format,
        uint width,
        uint height,
        ushort depth,
        ushort mipLevels = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        ulong alignment = 0)
    {
        return new ResourceDescription(
            ResourceDimension.Texture3D,
            alignment,
            width,
            height,
            depth,
            mipLevels,
            format,
            1,
            0,
            layout,
            flags);
    }

    public readonly ushort Depth => Dimension == ResourceDimension.Texture3D ? DepthOrArraySize : (ushort)1;
    public readonly ushort ArraySize => Dimension != ResourceDimension.Texture3D ? DepthOrArraySize : (ushort)1;

    public byte GetPlaneCount(ID3D12Device device)
    {
        return device.GetFormatPlaneCount(Format);
    }

    public uint Subresources(ID3D12Device pDevice)
    {
        return (uint)MipLevels * ArraySize * GetPlaneCount(pDevice);
    }

    public uint CalculateSubResourceIndex(uint mipSlice, uint arraySlice, uint planeSlice)
    {
        return ID3D12Resource.CalculateSubResourceIndex(mipSlice, arraySlice, planeSlice, MipLevels, ArraySize);
    }
}
