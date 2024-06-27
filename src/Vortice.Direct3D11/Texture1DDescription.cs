// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a 1D texture.
/// </summary>
public partial struct Texture1DDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Texture1DDescription"/> struct.
    /// </summary>
    /// <param name="format">Texture format.</param>
    /// <param name="width">Texture width (in texels).</param>
    /// <param name="arraySize">Number of textures in the array.</param>
    /// <param name="mipLevels">The maximum number of mipmap levels in the texture.</param>
    /// <param name="bindFlags">The <see cref="Direct3D11.BindFlags"/> for binding to pipeline stages.</param>
    /// <param name="usage">Value that identifies how the texture is to be read from and written to.</param>
    /// <param name="cpuAccessFlags">The <see cref="Direct3D11.CpuAccessFlags"/> to specify the types of CPU access allowed.</param>
    /// <param name="miscFlags">The <see cref="ResourceOptionFlags"/> that identify other, less common resource options. </param>
    public Texture1DDescription(
        Format format,
        int width,
        int arraySize = 1,
        int mipLevels = 0,
        BindFlags bindFlags = BindFlags.ShaderResource,
        ResourceUsage usage = ResourceUsage.Default,
        CpuAccessFlags cpuAccessFlags = CpuAccessFlags.None,
        ResourceOptionFlags miscFlags = ResourceOptionFlags.None)
    {
        if (format == Format.Unknown)
            throw new ArgumentException($"format need to be valid", nameof(format));

        if (width < 1 || width > ID3D11Resource.MaximumTexture1DSize)
            throw new ArgumentException($"Width need to be in range 1-{ID3D11Resource.MaximumTexture1DSize}", nameof(width));

        if (arraySize < 1 || arraySize > ID3D11Resource.MaximumTexture1DArraySize)
            throw new ArgumentException($"Array size need to be in range 1-{ID3D11Resource.MaximumTexture1DArraySize}", nameof(arraySize));

        Width = width;
        MipLevels = mipLevels;
        ArraySize = arraySize;
        Format = format;
        Usage = usage;
        BindFlags = bindFlags;
        CPUAccessFlags = cpuAccessFlags;
        MiscFlags = miscFlags;
    }

    /// <summary>
    /// Get a mip-level width.
    /// </summary>
    /// <param name="mipLevel"></param>
    /// <returns></returns>
    public int GetWidth(int mipLevel = 0)
    {
        return (mipLevel == 0) || (mipLevel < MipLevels) ? Math.Max(1, Width >> mipLevel) : 0;
    }
}
