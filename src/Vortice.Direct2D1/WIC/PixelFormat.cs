// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Globalization;
using Vortice.DXGI;

namespace Vortice.WIC;

public sealed partial class PixelFormat
{
    private static readonly Dictionary<Guid, int> s_mapGuidToSize;
    private static readonly Dictionary<Guid, Format> s_WICFormats;

    static PixelFormat()
    {
        s_mapGuidToSize = new()
        {
            { Format72bpp8ChannelsAlpha, 72 },
            { Format48bppBGRFixedPoint, 48 },
            { Format64bppCMYK, 64 },
            { Format40bpp5Channels, 40 },
            { Format4bppIndexed, 4 },
            { Format128bppRGBAFixedPoint, 128 },
            { Format40bpp4ChannelsAlpha, 40 },
            { Format32bppGrayFixedPoint, 32 },
            { Format48bpp6Channels, 48 },
            { Format16bppGray, 16 },
            { Format1bppIndexed, 1 },
            { Format32bpp3ChannelsAlpha, 32 },
            { Format64bppRGBAFixedPoint, 64 },
            { Format80bpp4ChannelsAlpha, 80 },
            { Format64bpp8Channels, 64 },
            { Format16bppBGR555, 16 },
            { Format16bppBGR565, 16 },
            { Format32bppRGBA1010102XR, 32 },
            { Format24bppRGB, 24 },
            { Format128bppRGBFloat, 128 },
            { Format32bppBGR, 32 },
            { Format64bppRGBA, 64 },
            { FormatDontCare, 0 },
            { Format40bppCMYKAlpha, 40 },
            { Format32bppPRGBA, 32 },
            { Format24bpp3Channels, 24 },
            { Format32bppRGBE, 32 },
            { Format24bppBGR, 24 },
            { Format64bppRGBFixedPoint, 64 },
            { Format96bppRGBFixedPoint, 96 },
            { Format128bppRGBFixedPoint, 128 },
            { Format144bpp8ChannelsAlpha, 144 },
            { Format64bppBGRAFixedPoint, 64 },
            { Format32bppCMYK, 32 },
            { Format32bppGrayFloat, 32 },
            { Format48bpp3Channels, 48 },
            { Format32bppBGR101010, 32 },
            { Format2bppGray, 2 },
            { Format56bpp7Channels, 56 },
            { Format16bppBGRA5551, 16 },
            { Format48bppRGBFixedPoint, 48 },
            { Format32bppRGBA1010102, 32 },
            { Format64bppPBGRA, 64 },
            { Format96bpp6Channels, 96 },
            { Format32bppPBGRA, 32 },
            { Format64bpp4Channels, 64 },
            { Format96bpp5ChannelsAlpha, 48 },
            { Format48bppRGBHalf, 96 },
            { Format48bpp5ChannelsAlpha, 48 },
            { Format8bppGray, 8 },
            { Format128bpp7ChannelsAlpha, 128 },
            { Format32bppRGBA, 32 },
            { Format80bpp5Channels, 80 },
            { Format64bppPRGBA, 64 },
            { Format16bppGrayFixedPoint, 16 },
            { Format112bpp7Channels, 112 },
            { Format128bpp8Channels, 128 },
            { Format80bppCMYKAlpha, 80 },
            { Format8bppAlpha, 8 },
            { Format4bppGray, 4 },
            { FormatBlackWhite, 0 },
            { Format32bppBGRA, 32 },
            { Format128bppRGBAFloat, 128 },
            { Format112bpp6ChannelsAlpha, 112 },
            { Format64bppRGBAHalf, 64 },
            { Format16bppGrayHalf, 16 },
            { Format2bppIndexed, 2 },
            { Format64bppBGRA, 64 },
            { Format8bppIndexed, 8 },
            { Format56bpp6ChannelsAlpha, 56 },
            { Format48bppBGR, 48 },
            { Format128bppPRGBAFloat, 128 },
            { Format64bpp7ChannelsAlpha, 64 },
            { Format64bpp3ChannelsAlpha, 64 },
            { Format64bppRGBHalf, 64 },
            { Format48bppRGB, 48 },
            { Format32bpp4Channels, 32 },
        };

        s_WICFormats = new()
        {
            { Format128bppRGBAFloat,        Format.R32G32B32A32_Float },

            { Format64bppRGBAHalf,          Format.R16G16B16A16_Float },
            { Format64bppRGBA,              Format.R16G16B16A16_UNorm },

            { Format32bppRGBA,              Format.R8G8B8A8_UNorm },
            { Format32bppBGRA,              Format.B8G8R8A8_UNorm }, // DXGI 1.1
            { Format32bppBGR,               Format.B8G8R8X8_UNorm }, // DXGI 1.1

            { Format32bppRGBA1010102XR,     Format.R10G10B10_Xr_Bias_A2_UNorm }, // DXGI 1.1
            { Format32bppRGBA1010102,       Format.R10G10B10A2_UNorm },

            { Format16bppBGRA5551,          Format.B5G5R5A1_UNorm },
            { Format16bppBGR565,            Format.B5G6R5_UNorm },

            { Format32bppGrayFloat,         Format.R32_Float },
            { Format16bppGrayHalf,          Format.R16_Float },
            { Format16bppGray,              Format.R16_UNorm },
            { Format8bppGray,               Format.R8_UNorm },

            { Format8bppAlpha,              Format.A8_UNorm },
            { Format96bppRGBFloat,          Format.R32G32B32_Float },
        };
    }

    /// <summary>
    /// Gets the number of bits per pixel for a particular pixel format.
    /// </summary>
    /// <param name="guid">The pixel format guid.</param>
    /// <returns>The number of bits per pixel. If the pixel format guid is invalid, returns 0</returns>
    public static int GetBitsPerPixel(Guid guid)
    {
        s_mapGuidToSize.TryGetValue(guid, out int bitsPerPixel);
        return bitsPerPixel;
    }

    /// <summary>
    /// Gets the stride in bytes from a pixel format and a width..
    /// </summary>
    /// <param name="guid">The pixel format guid.</param>
    /// <param name="width">The width.</param>
    /// <returns>The number of bytes per row.</returns>
    public static int GetStride(Guid guid, int width)
    {
        int bitsPerPixel = GetBitsPerPixel(guid);
        if (bitsPerPixel == 0)
        {
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid PixelFormat guid [{0}]. Unable to calculate stride", guid));
        }

        return (bitsPerPixel * width + 7) / 8;
    }

    public static Format ToDXGIFormat(Guid guid)
    {
        if (s_WICFormats.TryGetValue(guid, out Format format))
        {
            return format;
        }

        return Format.Unknown;
    }

    public static int WICBitsPerPixel(IWICImagingFactory factory, Guid pixelFormat)
    {
        using IWICComponentInfo info = factory.CreateComponentInfo(pixelFormat);
        ComponentType type = info.ComponentType;
        if (type != ComponentType.PixelFormat)
            return 0;

        using IWICPixelFormatInfo pixelFormatInfo = info.QueryInterface<IWICPixelFormatInfo>();
        return pixelFormatInfo.BitsPerPixel;
    }
}
