// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12;

public partial struct ResourceDescription1
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceDescription1"/> struct.
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
    /// <param name="samplerFeedbackMipRegionWidth"></param>
    /// <param name="samplerFeedbackMipRegionHeight"></param>
    /// <param name="samplerFeedbackMipRegionDepth"></param>
    public ResourceDescription1(
        ResourceDimension dimension,
        ulong alignment,
        ulong width,
        int height,
        ushort depthOrArraySize,
        ushort mipLevels,
        Format format,
        int sampleCount,
        int sampleQuality,
        TextureLayout layout,
        ResourceFlags flags,
        int samplerFeedbackMipRegionWidth = 0,
        int samplerFeedbackMipRegionHeight = 0,
        int samplerFeedbackMipRegionDepth = 0)
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
        SamplerFeedbackMipRegion = new MipRegion(samplerFeedbackMipRegionWidth, samplerFeedbackMipRegionHeight, samplerFeedbackMipRegionDepth);
    }

    public static ResourceDescription1 Buffer(ResourceAllocationInfo resourceAllocInfo, ResourceFlags flags = ResourceFlags.None)
    {
        return new ResourceDescription1(
            ResourceDimension.Buffer,
            resourceAllocInfo.Alignment,
            resourceAllocInfo.SizeInBytes,
            1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags, 0, 0, 0);
    }

    public static ResourceDescription1 Buffer(
        ulong width,
        ResourceFlags flags = ResourceFlags.None,
        ulong alignment = 0)
    {
        return new ResourceDescription1(ResourceDimension.Buffer, alignment, width, 1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags, 0, 0, 0);
    }

    public static ResourceDescription1 Buffer(
        long width,
        ResourceFlags flags = ResourceFlags.None,
        long alignment = 0)
    {
        checked
        {
            return new ResourceDescription1(ResourceDimension.Buffer, (ulong)alignment, (ulong)width, 1, 1, 1, Format.Unknown, 1, 0, TextureLayout.RowMajor, flags, 0, 0, 0);
        }
    }

    public static ResourceDescription1 Texture1D(Format format,
        ulong width,
        ushort arraySize = 1,
        ushort mipLevels = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        ulong alignment = 0)
    {
        return new ResourceDescription1(ResourceDimension.Texture1D, alignment, width, 1, arraySize,
            mipLevels, format, 1, 0, layout, flags, 0, 0, 0);
    }

    public static ResourceDescription1 Texture1D(Format format,
        long width,
        int arraySize = 1,
        int mipLevels = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        long alignment = 0)
    {
        checked
        {
            return new ResourceDescription1(ResourceDimension.Texture1D, (ulong)alignment, (ulong)width, 1, (ushort)arraySize,
                (ushort)mipLevels, format, 1, 0, layout, flags, 0, 0, 0);
        }
    }

    public static ResourceDescription1 Texture2D(Format format,
        ulong width,
        int height,
        ushort arraySize = 1,
        ushort mipLevels = 0,
        int sampleCount = 1,
        int sampleQuality = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        ulong alignment = 0,
        int samplerFeedbackMipRegionWidth = 0,
        int samplerFeedbackMipRegionHeight = 0,
        int samplerFeedbackMipRegionDepth = 0)
    {
        return new ResourceDescription1(ResourceDimension.Texture2D, alignment, width, height, arraySize,
            mipLevels, format, sampleCount, sampleQuality, layout, flags,
            samplerFeedbackMipRegionWidth, samplerFeedbackMipRegionHeight, samplerFeedbackMipRegionDepth);
    }

    public static ResourceDescription1 Texture2D(Format format,
        long width,
        int height,
        int arraySize = 1,
        int mipLevels = 0,
        int sampleCount = 1,
        int sampleQuality = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        long alignment = 0,
        int samplerFeedbackMipRegionWidth = 0,
        int samplerFeedbackMipRegionHeight = 0,
        int samplerFeedbackMipRegionDepth = 0)
    {
        checked
        {
            return new ResourceDescription1(ResourceDimension.Texture2D, (ulong)alignment, (ulong)width, height,
                (ushort)arraySize, (ushort)mipLevels, format, sampleCount, sampleQuality, layout, flags,
                samplerFeedbackMipRegionWidth, samplerFeedbackMipRegionHeight, samplerFeedbackMipRegionDepth);
        }
    }

    public static ResourceDescription1 Texture3D(Format format,
        ulong width,
        int height,
        ushort depth,
        ushort mipLevels = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        ulong alignment = 0)
    {
        return new ResourceDescription1(ResourceDimension.Texture3D, alignment, width, height, depth,
            mipLevels, format, 1, 0, layout, flags, 0, 0, 0);
    }

    public static ResourceDescription1 Texture3D(Format format,
        long width,
        int height,
        int depth,
        int mipLevels = 0,
        ResourceFlags flags = ResourceFlags.None,
        TextureLayout layout = TextureLayout.Unknown,
        long alignment = 0)
    {
        checked
        {
            return new ResourceDescription1(ResourceDimension.Texture3D, (ulong)alignment, (ulong)width, height,
                (ushort)depth, (ushort)mipLevels, format, 1, 0, layout, flags, 0, 0, 0);
        }
    }

    public int Depth => Dimension == ResourceDimension.Texture3D ? DepthOrArraySize : 1;
    public int ArraySize => Dimension != ResourceDimension.Texture3D ? DepthOrArraySize : 1;

    public byte GetPlaneCount(ID3D12Device device)
    {
        return device.GetFormatPlaneCount(Format);
    }

    public int Subresources(ID3D12Device pDevice)
    {
        return MipLevels * ArraySize * GetPlaneCount(pDevice);
    }

    public int CalculateSubResourceIndex(int mipSlice, int arraySlice, int planeSlice)
    {
        return ID3D12Resource.CalculateSubResourceIndex(mipSlice, arraySlice, planeSlice, MipLevels, ArraySize);
    }
}
