// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

/// <summary>
/// Specifies the subresources from a resource that are accessible using an unordered-access view.
/// </summary>
public partial struct UnorderedAccessViewDescription1
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnorderedAccessViewDescription1"/> struct.
    /// </summary>
    /// <param name="viewDimension">The <see cref="UnorderedAccessViewDimension"/></param>
    /// <param name="format">The <see cref="DXGI.Format"/> to use or <see cref="Format.Unknown"/>.</param>
    /// <param name="mipSlice">The index of the mipmap level to use mip slice or FirstElement for BUFFER.</param>
    /// <param name="firstArraySlice">The index of the first texture to use in an array of textures or NumElements for BUFFER or FirstWSlice for TEXTURE3D.</param>
    /// <param name="arraySize">Number of textures in the array or WSize for TEXTURE3D.</param>
    /// <param name="flags"><see cref="BufferUnorderedAccessViewFlags"/> options flags for the resource.</param>
    /// <param name="planeSlice"></param>
    public UnorderedAccessViewDescription1(
        UnorderedAccessViewDimension viewDimension,
        Format format = Format.Unknown,
        uint mipSlice = 0,
        uint firstArraySlice = 0,
        uint arraySize = unchecked((uint)-1),
        BufferUnorderedAccessViewFlags flags = BufferUnorderedAccessViewFlags.None,
        uint planeSlice = 0)
    {
        Format = format;
        ViewDimension = viewDimension;
        switch (viewDimension)
        {
            case UnorderedAccessViewDimension.Buffer:
                Buffer.FirstElement = mipSlice;
                Buffer.NumElements = firstArraySlice;
                Buffer.Flags = flags;
                break;
            case UnorderedAccessViewDimension.Texture1D:
                Texture1D.MipSlice = mipSlice;
                break;
            case UnorderedAccessViewDimension.Texture1DArray:
                Texture1DArray.MipSlice = mipSlice;
                Texture1DArray.FirstArraySlice = firstArraySlice;
                Texture1DArray.ArraySize = arraySize;
                break;
            case UnorderedAccessViewDimension.Texture2D:
                Texture2D.MipSlice = mipSlice;
                Texture2D.PlaneSlice = planeSlice;
                break;
            case UnorderedAccessViewDimension.Texture2DArray:
                Texture2DArray.MipSlice = mipSlice;
                Texture2DArray.FirstArraySlice = firstArraySlice;
                Texture2DArray.ArraySize = arraySize;
                Texture2DArray.PlaneSlice = planeSlice;
                break;
            case UnorderedAccessViewDimension.Texture3D:
                Texture3D.MipSlice = mipSlice;
                Texture3D.FirstWSlice = firstArraySlice;
                Texture3D.WSize = arraySize;
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnorderedAccessViewDescription1"/> struct.
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="format"></param>
    /// <param name="firstElement"></param>
    /// <param name="numElements"></param>
    /// <param name="flags"><see cref="BufferUnorderedAccessViewFlags"/> options flags for the resource.</param>
    public UnorderedAccessViewDescription1(
        ID3D11Buffer buffer,
        Format format,
        uint firstElement = 0,
        uint numElements = 0,
        BufferUnorderedAccessViewFlags flags = BufferUnorderedAccessViewFlags.None)
    {
        Format = format;
        ViewDimension = UnorderedAccessViewDimension.Buffer;
        Buffer.FirstElement = firstElement;
        Buffer.NumElements = numElements;
        Buffer.Flags = flags;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnorderedAccessViewDescription1"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="isArray"></param>
    /// <param name="format"></param>
    /// <param name="mipSlice"></param>
    /// <param name="firstArraySlice"></param>
    /// <param name="arraySize"></param>
    public UnorderedAccessViewDescription1(
        ID3D11Texture1D texture,
        bool isArray,
        Format format = Format.Unknown,
        uint mipSlice = 0,
        uint firstArraySlice = 0,
        uint arraySize = unchecked((uint)-1) - 1)
    {
        ViewDimension = isArray ? UnorderedAccessViewDimension.Texture1DArray : UnorderedAccessViewDimension.Texture1D;

        if (format == Format.Unknown
            || (unchecked((uint)-1) == arraySize && (UnorderedAccessViewDimension.Texture1DArray == ViewDimension)))
        {
            var textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (arraySize == unchecked((uint)-1))
                arraySize = textureDesc.ArraySize - firstArraySlice;
        }
        Format = format;
        switch (ViewDimension)
        {
            case UnorderedAccessViewDimension.Texture1D:
                Texture1D.MipSlice = mipSlice;
                break;
            case UnorderedAccessViewDimension.Texture1DArray:
                Texture1DArray.MipSlice = mipSlice;
                Texture1DArray.FirstArraySlice = firstArraySlice;
                Texture1DArray.ArraySize = arraySize;
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnorderedAccessViewDescription1"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="viewDimension"></param>
    /// <param name="format"></param>
    /// <param name="mipSlice"></param>
    /// <param name="firstArraySlice"></param>
    /// <param name="arraySize"></param>
    /// <param name="planeSlice"></param>
    public UnorderedAccessViewDescription1(
        ID3D11Texture2D texture,
        UnorderedAccessViewDimension viewDimension,
        Format format = Format.Unknown,
        uint mipSlice = 0,
        uint firstArraySlice = 0,
        uint arraySize = unchecked((uint)-1) - 1,
        uint planeSlice = 0) 
    {
        ViewDimension = viewDimension;

        if (format == Format.Unknown
            || (unchecked((uint)-1) == arraySize && (viewDimension == UnorderedAccessViewDimension.Texture2DArray)))
        {
            var textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (arraySize == unchecked((uint)-1))
                arraySize = textureDesc.ArraySize - firstArraySlice;
        }
        Format = format;
        switch (viewDimension)
        {
            case UnorderedAccessViewDimension.Texture2D:
                Texture2D.MipSlice = mipSlice;
                Texture2D.PlaneSlice = planeSlice;
                break;
            case UnorderedAccessViewDimension.Texture2DArray:
                Texture2DArray.MipSlice = mipSlice;
                Texture2DArray.FirstArraySlice = firstArraySlice;
                Texture2DArray.ArraySize = arraySize;
                Texture2DArray.PlaneSlice = planeSlice;
                break;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnorderedAccessViewDescription1"/> struct.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="viewDimension"></param>
    /// <param name="format"></param>
    /// <param name="mipSlice"></param>
    /// <param name="firstWSlice"></param>
    /// <param name="wSize"></param>
    public UnorderedAccessViewDescription1(
        ID3D11Texture3D texture,
        UnorderedAccessViewDimension viewDimension,
        Format format = Format.Unknown,
        uint mipSlice = 0,
        uint firstWSlice = 0,
        uint wSize = unchecked((uint)-1))
    {
        ViewDimension = viewDimension;

        if (format == Format.Unknown || wSize == unchecked((uint)-1))
        {
            Texture3DDescription textureDesc = texture.Description;
            if (format == Format.Unknown)
                format = textureDesc.Format;
            if (wSize == unchecked((uint)-1))
                wSize = textureDesc.Depth - firstWSlice;
        }
        Format = format;
        Texture3D.MipSlice = mipSlice;
        Texture3D.FirstWSlice = firstWSlice;
        Texture3D.WSize = wSize;
    }
}
