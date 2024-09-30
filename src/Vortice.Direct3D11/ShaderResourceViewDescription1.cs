// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;
using Vortice.DXGI;

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a shader-resource view.
/// </summary>
public partial struct ShaderResourceViewDescription1
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderResourceViewDescription1"/> struct.
    /// </summary>
    /// <param name="viewDimension">The <see cref="ShaderResourceViewDimension"/></param>
    /// <param name="format">The <see cref="DXGI.Format"/> to use or <see cref="Format.Unknown"/>.</param>
    /// <param name="mostDetailedMip">Index of the most detailed mipmap level to use or first element for <see cref="ShaderResourceViewDimension.Buffer"/> or <see cref="ShaderResourceViewDimension.BufferExtended"/>.</param>
    /// <param name="mipLevels">The maximum number of mipmap levels for the view of the texture or num elements for <see cref="ShaderResourceViewDimension.Buffer"/> or <see cref="ShaderResourceViewDimension.BufferExtended"/>.</param>
    /// <param name="firstArraySlice">The index of the first texture to use in an array of textures or First2DArrayFace for <see cref="ShaderResourceViewDimension.TextureCubeArray"/>. </param>
    /// <param name="arraySize">Number of textures in the array or num cubes for <see cref="ShaderResourceViewDimension.TextureCubeArray"/>. </param>
    /// <param name="flags"><see cref="BufferExtendedShaderResourceViewFlags"/> for <see cref="ShaderResourceViewDimension.BufferExtended"/>.</param>
    /// <param name="planeSlice"></param>
    public ShaderResourceViewDescription1(
        ShaderResourceViewDimension viewDimension,
        Format format = Format.Unknown,
        uint mostDetailedMip = 0,
        uint mipLevels = unchecked((uint)-1),
        uint firstArraySlice = 0,
        uint arraySize = unchecked((uint)-1),
        BufferExtendedShaderResourceViewFlags flags = BufferExtendedShaderResourceViewFlags.None,
        uint planeSlice = 0)
    {
        Format = format;
        ViewDimension = viewDimension;
        switch (viewDimension)
        {
            case ShaderResourceViewDimension.Buffer:
                Buffer.FirstElement = mostDetailedMip;
                Buffer.NumElements = mipLevels;
                break;
            case ShaderResourceViewDimension.Texture1D:
                Texture1D.MostDetailedMip = mostDetailedMip;
                Texture1D.MipLevels = mipLevels;
                break;
            case ShaderResourceViewDimension.Texture1DArray:
                Texture1DArray.MostDetailedMip = mostDetailedMip;
                Texture1DArray.MipLevels = mipLevels;
                Texture1DArray.FirstArraySlice = firstArraySlice;
                Texture1DArray.ArraySize = arraySize;
                break;
            case ShaderResourceViewDimension.Texture2D:
                Texture2D.MostDetailedMip = mostDetailedMip;
                Texture2D.MipLevels = mipLevels;
                Texture2D.PlaneSlice = planeSlice;
                break;
            case ShaderResourceViewDimension.Texture2DArray:
                Texture2DArray.MostDetailedMip = mostDetailedMip;
                Texture2DArray.MipLevels = mipLevels;
                Texture2DArray.FirstArraySlice = firstArraySlice;
                Texture2DArray.ArraySize = arraySize;
                Texture2DArray.PlaneSlice = planeSlice;
                break;
            case ShaderResourceViewDimension.Texture2DMultisampled:
                break;
            case ShaderResourceViewDimension.Texture2DMultisampledArray:
                Texture2DMSArray.FirstArraySlice = firstArraySlice;
                Texture2DMSArray.ArraySize = arraySize;
                break;
            case ShaderResourceViewDimension.Texture3D:
                Texture3D.MostDetailedMip = mostDetailedMip;
                Texture3D.MipLevels = mipLevels;
                break;
            case ShaderResourceViewDimension.TextureCube:
                TextureCube.MostDetailedMip = mostDetailedMip;
                TextureCube.MipLevels = mipLevels;
                break;
            case ShaderResourceViewDimension.TextureCubeArray:
                TextureCubeArray.MostDetailedMip = mostDetailedMip;
                TextureCubeArray.MipLevels = mipLevels;
                TextureCubeArray.First2DArrayFace = firstArraySlice;
                TextureCubeArray.NumCubes = arraySize;
                break;
            case ShaderResourceViewDimension.BufferExtended:
                BufferEx.FirstElement = mostDetailedMip;
                BufferEx.NumElements = mipLevels;
                BufferEx.Flags = flags;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderResourceViewDescription1"/> struct.
    /// </summary>
    /// <param name="buffer">Unused <see cref="ID3D11Buffer"/> </param>
    /// <param name="format"></param>
    /// <param name="firstElement"></param>
    /// <param name="numElements"></param>
    /// <param name="flags"></param>
    public ShaderResourceViewDescription1(ID3D11Buffer buffer, Format format, uint firstElement, uint numElements, BufferExtendedShaderResourceViewFlags flags = BufferExtendedShaderResourceViewFlags.None)
    {
        Format = format;
        ViewDimension = ShaderResourceViewDimension.BufferExtended;
        BufferEx.FirstElement = firstElement;
        BufferEx.NumElements = numElements;
        BufferEx.Flags = flags;
    }

    public ShaderResourceViewDescription1(
        ID3D11Texture1D texture,
        bool isArray,
        Format format = Format.Unknown,
        uint mostDetailedMip = 0,
        uint mipLevels = unchecked((uint)-1),
        uint firstArraySlice = 0,
        uint arraySize = unchecked((uint)-1))
    {
        ViewDimension = isArray ? ShaderResourceViewDimension.Texture1DArray : ShaderResourceViewDimension.Texture1D;
        if (format == Format.Unknown
            || mipLevels == unchecked((uint)-1)
            || (arraySize == unchecked((uint)-1) && ShaderResourceViewDimension.Texture1DArray == ViewDimension))
        {
            Texture1DDescription textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (mipLevels == unchecked((uint)-1))
                mipLevels = textureDesc.MipLevels - mostDetailedMip;
            if (arraySize == unchecked((uint)-1))
                arraySize = textureDesc.ArraySize - firstArraySlice;
        }

        Format = format;
        switch (ViewDimension)
        {
            case ShaderResourceViewDimension.Texture1D:
                Texture1D.MostDetailedMip = mostDetailedMip;
                Texture1D.MipLevels = mipLevels;
                break;
            case ShaderResourceViewDimension.Texture1DArray:
                Texture1DArray.MostDetailedMip = mostDetailedMip;
                Texture1DArray.MipLevels = mipLevels;
                Texture1DArray.FirstArraySlice = firstArraySlice;
                Texture1DArray.ArraySize = arraySize;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderResourceViewDescription1"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="viewDimension"></param>
    /// <param name="format"></param>
    /// <param name="mostDetailedMip"></param>
    /// <param name="mipLevels"></param>
    /// <param name="firstArraySlice"></param>
    /// <param name="arraySize"></param>
    /// <param name="planeSlice"></param>
    public ShaderResourceViewDescription1(
        ID3D11Texture2D texture,
        ShaderResourceViewDimension viewDimension,
        Format format = Format.Unknown,
        uint mostDetailedMip = 0,
        uint mipLevels = unchecked((uint)-1),
        uint firstArraySlice = 0,
        uint arraySize = unchecked((uint)-1),
        uint planeSlice = 0)
    {
        ViewDimension = viewDimension;
        if (format == Format.Unknown
            || (mipLevels == unchecked((uint)-1) && viewDimension != ShaderResourceViewDimension.Texture2DMultisampled && viewDimension != ShaderResourceViewDimension.Texture2DMultisampledArray)
            || (arraySize == unchecked((uint)-1) && (ShaderResourceViewDimension.Texture2DArray == viewDimension || ShaderResourceViewDimension.Texture2DMultisampledArray == viewDimension || ShaderResourceViewDimension.TextureCubeArray == viewDimension)))
        {
            Texture2DDescription textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (unchecked((uint)-1) == mipLevels)
                mipLevels = textureDesc.MipLevels - mostDetailedMip;
            if (unchecked((uint)-1) == arraySize)
            {
                arraySize = textureDesc.ArraySize - firstArraySlice;
                if (viewDimension == ShaderResourceViewDimension.TextureCubeArray)
                    arraySize /= 6;
            }
        }
        Format = format;
        switch (viewDimension)
        {
            case ShaderResourceViewDimension.Texture2D:
                Texture2D.MostDetailedMip = mostDetailedMip;
                Texture2D.MipLevels = mipLevels;
                Texture2D.PlaneSlice = planeSlice;
                break;
            case ShaderResourceViewDimension.Texture2DArray:
                Texture2DArray.MostDetailedMip = mostDetailedMip;
                Texture2DArray.MipLevels = mipLevels;
                Texture2DArray.FirstArraySlice = firstArraySlice;
                Texture2DArray.ArraySize = arraySize;
                Texture2DArray.PlaneSlice = planeSlice;
                break;
            case ShaderResourceViewDimension.Texture2DMultisampled:
                break;
            case ShaderResourceViewDimension.Texture2DMultisampledArray:
                Texture2DMSArray.FirstArraySlice = firstArraySlice;
                Texture2DMSArray.ArraySize = arraySize;
                break;
            case ShaderResourceViewDimension.TextureCube:
                TextureCube.MostDetailedMip = mostDetailedMip;
                TextureCube.MipLevels = mipLevels;
                break;
            case ShaderResourceViewDimension.TextureCubeArray:
                TextureCubeArray.MostDetailedMip = mostDetailedMip;
                TextureCubeArray.MipLevels = mipLevels;
                TextureCubeArray.First2DArrayFace = firstArraySlice;
                TextureCubeArray.NumCubes = arraySize;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShaderResourceViewDescription1"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="format"></param>
    /// <param name="mostDetailedMip"></param>
    /// <param name="mipLevels"></param>
    public ShaderResourceViewDescription1(
        ID3D11Texture3D texture,
        Format format = Format.Unknown,
        uint mostDetailedMip = 0,
        uint mipLevels = unchecked((uint)-1))
    {
        ViewDimension = ShaderResourceViewDimension.Texture3D;
        if (format == Format.Unknown || mipLevels == unchecked((uint)-1))
        {
            Texture3DDescription textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (mipLevels == unchecked((uint)-1))
                mipLevels = textureDesc.MipLevels - mostDetailedMip;
        }

        Format = format;
        Texture3D.MostDetailedMip = mostDetailedMip;
        Texture3D.MipLevels = mipLevels;
    }
}
