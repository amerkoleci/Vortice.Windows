// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

/// <summary>
/// Specifies the subresources of a texture that are accessible from a depth-stencil view.
/// </summary>
public partial struct DepthStencilViewDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilViewDescription"/> struct.
    /// </summary>
    /// <param name="viewDimension">The <see cref="DepthStencilViewDimension"/></param>
    /// <param name="format">The <see cref="DXGI.Format"/> to use or <see cref="Format.Unknown"/>.</param>
    /// <param name="mipSlice">The index of the mipmap level to use mip slice.</param>
    /// <param name="firstArraySlice">The index of the first texture to use in an array of textures.</param>
    /// <param name="arraySize">Number of textures in the array.</param>
    /// <param name="flags"></param>
    public DepthStencilViewDescription(
        DepthStencilViewDimension viewDimension,
        Format format = Format.Unknown,
        int mipSlice = 0,
        int firstArraySlice = 0,
        int arraySize = -1,
        DepthStencilViewFlags flags = DepthStencilViewFlags.None) : this()
    {
        Format = format;
        ViewDimension = viewDimension;
        Flags = flags;
        switch (viewDimension)
        {
            case DepthStencilViewDimension.Texture1D:
                Texture1D.MipSlice = mipSlice;
                break;
            case DepthStencilViewDimension.Texture1DArray:
                Texture1DArray.MipSlice = mipSlice;
                Texture1DArray.FirstArraySlice = firstArraySlice;
                Texture1DArray.ArraySize = arraySize;
                break;
            case DepthStencilViewDimension.Texture2D:
                Texture2D.MipSlice = mipSlice;
                break;
            case DepthStencilViewDimension.Texture2DArray:
                Texture2DArray.MipSlice = mipSlice;
                Texture2DArray.FirstArraySlice = firstArraySlice;
                Texture2DArray.ArraySize = arraySize;
                break;
            case DepthStencilViewDimension.Texture2DMultisampled:
                break;
            case DepthStencilViewDimension.Texture2DMultisampledArray:
                Texture2DMSArray.FirstArraySlice = firstArraySlice;
                Texture2DMSArray.ArraySize = arraySize;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilViewDescription"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="isArray"></param>
    /// <param name="format"></param>
    /// <param name="mipSlice"></param>
    /// <param name="firstArraySlice"></param>
    /// <param name="arraySize"></param>
    /// <param name="flags"></param>
    public DepthStencilViewDescription(
        ID3D11Texture1D texture,
        bool isArray,
        Format format = Format.Unknown,
        int mipSlice = 0,
        int firstArraySlice = 0,
        int arraySize = -1,
        DepthStencilViewFlags flags = DepthStencilViewFlags.None) : this()
    {
        ViewDimension = isArray ? DepthStencilViewDimension.Texture1DArray : DepthStencilViewDimension.Texture1D;
        Flags = flags;
        if (format == Format.Unknown
            || (arraySize == -1 && DepthStencilViewDimension.Texture1DArray == ViewDimension))
        {
            var textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (arraySize == -1)
                arraySize = textureDesc.ArraySize - firstArraySlice;
        }

        Format = format;
        switch (ViewDimension)
        {
            case DepthStencilViewDimension.Texture1D:
                Texture1D.MipSlice = mipSlice;
                break;
            case DepthStencilViewDimension.Texture1DArray:
                Texture1DArray.MipSlice = mipSlice;
                Texture1DArray.FirstArraySlice = firstArraySlice;
                Texture1DArray.ArraySize = arraySize;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilViewDescription"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="viewDimension"></param>
    /// <param name="format"></param>
    /// <param name="mipSlice"></param>
    /// <param name="firstArraySlice"></param>
    /// <param name="arraySize"></param>
    /// <param name="flags"></param>
    public DepthStencilViewDescription(
        ID3D11Texture2D texture,
        DepthStencilViewDimension viewDimension,
        Format format = Format.Unknown,
        int mipSlice = 0,
        int firstArraySlice = 0,
        int arraySize = -1,
        DepthStencilViewFlags flags = DepthStencilViewFlags.None) : this()
    {
        ViewDimension = viewDimension;
        Flags = flags;

        if (format == Format.Unknown
            || (-1 == arraySize && (DepthStencilViewDimension.Texture2DArray == viewDimension || DepthStencilViewDimension.Texture2DMultisampledArray == viewDimension)))
        {
            var textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (arraySize == -1)
                arraySize = textureDesc.ArraySize - firstArraySlice;
        }
        Format = format;
        switch (viewDimension)
        {
            case DepthStencilViewDimension.Texture2D:
                Texture2D.MipSlice = mipSlice;
                break;
            case DepthStencilViewDimension.Texture2DArray:
                Texture2DArray.MipSlice = mipSlice;
                Texture2DArray.FirstArraySlice = firstArraySlice;
                Texture2DArray.ArraySize = arraySize;
                break;
            case DepthStencilViewDimension.Texture2DMultisampled:
                break;
            case DepthStencilViewDimension.Texture2DMultisampledArray:
                Texture2DMSArray.FirstArraySlice = firstArraySlice;
                Texture2DMSArray.ArraySize = arraySize;
                break;
            default:
                break;
        }
    }
}
