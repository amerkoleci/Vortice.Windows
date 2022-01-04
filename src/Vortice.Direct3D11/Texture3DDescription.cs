// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a 3D texture.
/// </summary>
public partial struct Texture3DDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Texture3DDescription"/> struct.
    /// </summary>
    /// <param name="width">Texture width (in texels).</param>
    /// <param name="height">Texture height (in texels).</param>
    /// <param name="depth">Texture depth (in texels).</param>
    /// <param name="format">Texture format.</param>
    /// <param name="mipLevels">The maximum number of mipmap levels in the texture.</param>
    /// <param name="bindFlags">The <see cref="Vortice.Direct3D11.BindFlags"/> for binding to pipeline stages.</param>
    /// <param name="usage">Value that identifies how the texture is to be read from and written to.</param>
    /// <param name="cpuAccessFlags">The <see cref="Direct3D11.CpuAccessFlags"/> to specify the types of CPU access allowed.</param>
    /// <param name="optionFlags">The <see cref="ResourceOptionFlags"/> that identify other, less common resource options. </param>
    public Texture3DDescription(
        int width,
        int height,
        int depth,
        Format format = Format.R8G8B8A8_UNorm,
        int mipLevels = 0,
        BindFlags bindFlags = BindFlags.ShaderResource,
        ResourceUsage usage = ResourceUsage.Default,
        CpuAccessFlags cpuAccessFlags = CpuAccessFlags.None,
        ResourceOptionFlags optionFlags = ResourceOptionFlags.None)
    {
        if (format == Format.Unknown)
            throw new ArgumentException($"format need to be valid", nameof(format));

        if (width < 1 || width > ID3D11Resource.MaximumTexture3DSize)
            throw new ArgumentException($"Width need to be in range 1-{ID3D11Resource.MaximumTexture3DSize}", nameof(width));

        if (height < 1 || height > ID3D11Resource.MaximumTexture3DSize)
            throw new ArgumentException($"Height need to be in range 1-{ID3D11Resource.MaximumTexture3DSize}", nameof(height));

        if (depth < 1 || depth > ID3D11Resource.MaximumTexture3DSize)
            throw new ArgumentException($"Depth need to be in range 1-{ID3D11Resource.MaximumTexture3DSize}", nameof(depth));

        Width = width;
        Height = height;
        Depth = depth;
        MipLevels = mipLevels;
        Format = format;
        Usage = usage;
        BindFlags = bindFlags;
        CpuAccessFlags = cpuAccessFlags;
        OptionFlags = optionFlags;
    }
}