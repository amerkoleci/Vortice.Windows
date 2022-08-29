// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// Methods used from https://github.com/microsoft/DirectXTex

namespace Vortice.DXGI;

/// <summary>
/// Helper to use with <see cref="Format"/>.
/// </summary>
public static class FormatHelper
{
    public const Format Xbox_R10G10B10_7E3_A2_Float = (Format)116;
    public const Format Xbox_R10G10B10_6E4_A2_Float = (Format)117;
    public const Format Xbox_D16_UNorm_S8_UInt = (Format)118;
    public const Format Xbox_R16_UNorm_X8_Typeless = (Format)119;
    public const Format Xbox_X16_Typeless_G8_UInt = (Format)120;
    public const Format Xbox_R10G10B10_SNorm_A2_UNorm = (Format)189;
    public const Format Xbox_R4G4_UNorm = (Format)190;

    /// <summary>
    /// Return the BPP for a given <see cref="Format"/>.
    /// </summary>
    /// <param name="format">The DXGI format.</param>
    /// <returns>BPP of </returns>
    public static int GetBitsPerPixel(this Format format)
    {
        switch (format)
        {
            case Format.R32G32B32A32_Typeless:
            case Format.R32G32B32A32_Float:
            case Format.R32G32B32A32_UInt:
            case Format.R32G32B32A32_SInt:
                return 128;

            case Format.R32G32B32_Typeless:
            case Format.R32G32B32_Float:
            case Format.R32G32B32_UInt:
            case Format.R32G32B32_SInt:
                return 96;

            case Format.R16G16B16A16_Typeless:
            case Format.R16G16B16A16_Float:
            case Format.R16G16B16A16_UNorm:
            case Format.R16G16B16A16_UInt:
            case Format.R16G16B16A16_SNorm:
            case Format.R16G16B16A16_SInt:
            case Format.R32G32_Typeless:
            case Format.R32G32_Float:
            case Format.R32G32_UInt:
            case Format.R32G32_SInt:
            case Format.R32G8X24_Typeless:
            case Format.D32_Float_S8X24_UInt:
            case Format.R32_Float_X8X24_Typeless:
            case Format.X32_Typeless_G8X24_UInt:
            case Format.Y416:
            case Format.Y210:
            case Format.Y216:
                return 64;

            case Format.R10G10B10A2_Typeless:
            case Format.R10G10B10A2_UNorm:
            case Format.R10G10B10A2_UInt:
            case Format.R11G11B10_Float:
            case Format.R8G8B8A8_Typeless:
            case Format.R8G8B8A8_UNorm:
            case Format.R8G8B8A8_UNorm_SRgb:
            case Format.R8G8B8A8_UInt:
            case Format.R8G8B8A8_SNorm:
            case Format.R8G8B8A8_SInt:
            case Format.R16G16_Typeless:
            case Format.R16G16_Float:
            case Format.R16G16_UNorm:
            case Format.R16G16_UInt:
            case Format.R16G16_SNorm:
            case Format.R16G16_SInt:
            case Format.R32_Typeless:
            case Format.D32_Float:
            case Format.R32_Float:
            case Format.R32_UInt:
            case Format.R32_SInt:
            case Format.R24G8_Typeless:
            case Format.D24_UNorm_S8_UInt:
            case Format.R24_UNorm_X8_Typeless:
            case Format.X24_Typeless_G8_UInt:
            case Format.R9G9B9E5_SharedExp:
            case Format.R8G8_B8G8_UNorm:
            case Format.G8R8_G8B8_UNorm:
            case Format.B8G8R8A8_UNorm:
            case Format.B8G8R8X8_UNorm:
            case Format.R10G10B10_Xr_Bias_A2_UNorm:
            case Format.B8G8R8A8_Typeless:
            case Format.B8G8R8A8_UNorm_SRgb:
            case Format.B8G8R8X8_Typeless:
            case Format.B8G8R8X8_UNorm_SRgb:
            case Format.AYUV:
            case Format.Y410:
            case Format.YUY2:
            case Xbox_R10G10B10_7E3_A2_Float:
            case Xbox_R10G10B10_6E4_A2_Float:
            case Xbox_R10G10B10_SNorm_A2_UNorm:
                return 32;

            case Format.P010:
            case Format.P016:
            case Xbox_D16_UNorm_S8_UInt:
            case Xbox_R16_UNorm_X8_Typeless:
            case Xbox_X16_Typeless_G8_UInt:
            case Format.V408:
                return 24;

            case Format.R8G8_Typeless:
            case Format.R8G8_UNorm:
            case Format.R8G8_UInt:
            case Format.R8G8_SNorm:
            case Format.R8G8_SInt:
            case Format.R16_Typeless:
            case Format.R16_Float:
            case Format.D16_UNorm:
            case Format.R16_UNorm:
            case Format.R16_UInt:
            case Format.R16_SNorm:
            case Format.R16_SInt:
            case Format.B5G6R5_UNorm:
            case Format.B5G5R5A1_UNorm:
            case Format.A8P8:
            case Format.B4G4R4A4_UNorm:
            case Format.P208:
            case Format.V208:
                return 16;

            case Format.NV12:
            case Format.Opaque420:
            case Format.NV11:
                return 12;

            case Format.R8_Typeless:
            case Format.R8_UNorm:
            case Format.R8_UInt:
            case Format.R8_SNorm:
            case Format.R8_SInt:
            case Format.A8_UNorm:
            case Format.BC2_Typeless:
            case Format.BC2_UNorm:
            case Format.BC2_UNorm_SRgb:
            case Format.BC3_Typeless:
            case Format.BC3_UNorm:
            case Format.BC3_UNorm_SRgb:
            case Format.BC5_Typeless:
            case Format.BC5_UNorm:
            case Format.BC5_SNorm:
            case Format.BC6H_Typeless:
            case Format.BC6H_Uf16:
            case Format.BC6H_Sf16:
            case Format.BC7_Typeless:
            case Format.BC7_UNorm:
            case Format.BC7_UNorm_SRgb:
            case Format.AI44:
            case Format.IA44:
            case Format.P8:
            case Xbox_R4G4_UNorm:
                return 8;

            case Format.R1_UNorm:
                return 1;

            case Format.BC1_Typeless:
            case Format.BC1_UNorm:
            case Format.BC1_UNorm_SRgb:
            case Format.BC4_Typeless:
            case Format.BC4_UNorm:
            case Format.BC4_SNorm:
                return 4;

            default:
                return 0;
        }
    }

    /// <summary>
    /// Returns true if the <see cref="Format"/> is valid.
    /// </summary>
    /// <param name="format">A format to validate</param>
    /// <returns>True if the <see cref="Format"/> is valid.</returns>
    public static bool IsValid(this Format format)
    {
        return ((int)(format) >= 1 && (int)(format) <= 115);
    }

    /// <summary>
    /// Returns true if the <see cref="Format"/> is a compressed format.
    /// </summary>
    /// <param name="format">The format to check for compressed.</param>
    /// <returns>True if the <see cref="Format"/> is a compressed format</returns>
    public static bool IsCompressed(this Format format)
    {
        switch (format)
        {
            case Format.BC1_Typeless:
            case Format.BC1_UNorm:
            case Format.BC1_UNorm_SRgb:
            case Format.BC2_Typeless:
            case Format.BC2_UNorm:
            case Format.BC2_UNorm_SRgb:
            case Format.BC3_Typeless:
            case Format.BC3_UNorm:
            case Format.BC3_UNorm_SRgb:
            case Format.BC4_Typeless:
            case Format.BC4_UNorm:
            case Format.BC4_SNorm:
            case Format.BC5_Typeless:
            case Format.BC5_UNorm:
            case Format.BC5_SNorm:
            case Format.BC6H_Typeless:
            case Format.BC6H_Uf16:
            case Format.BC6H_Sf16:
            case Format.BC7_Typeless:
            case Format.BC7_UNorm:
            case Format.BC7_UNorm_SRgb:
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="Format"/> is packed.
    /// </summary>
    /// <param name="format">The DXGI Format.</param>
    /// <returns><c>true</c> if the specified <see cref="Format"/> is packed; otherwise, <c>false</c>.</returns>
    public static bool IsPacked(this Format format)
    {
        switch (format)
        {
            case Format.R8G8_B8G8_UNorm:
            case Format.G8R8_G8B8_UNorm:
            case Format.YUY2: // 4:2:2 8-bit
            case Format.Y210: // 4:2:2 10-bit
            case Format.Y216: // 4:2:2 16-bit
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="Format"/> is video.
    /// </summary>
    /// <param name="format">The <see cref="Format"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Format"/> is video; otherwise, <c>false</c>.</returns>
    public static bool IsVideo(this Format format)
    {
        switch (format)
        {
            case Format.AYUV:
            case Format.Y410:
            case Format.Y416:
            case Format.NV12:
            case Format.P010:
            case Format.P016:
            case Format.YUY2:
            case Format.Y210:
            case Format.Y216:
            case Format.NV11:
            // These video formats can be used with the 3D pipeline through special view mappings

            case Format.Opaque420:
            case Format.AI44:
            case Format.IA44:
            case Format.P8:
            case Format.A8P8:
            // These are limited use video formats not usable in any way by the 3D pipeline

            case Format.P208:
            case Format.V208:
            case Format.V408:
                // These video formats are for JPEG Hardware decode (DXGI 1.4)
                return true;

            default:
                return false;
        }
    }

    public static bool IsPlanar(this Format format)
    {
        switch (format)
        {
            case Format.NV12:      // 4:2:0 8-bit
            case Format.P010:      // 4:2:0 10-bit
            case Format.P016:      // 4:2:0 16-bit
            case Format.Opaque420:// 4:2:0 8-bit
            case Format.NV11:      // 4:1:1 8-bit

            case Format.P208: // 4:2:2 8-bit
            case Format.V208: // 4:4:0 8-bit
            case Format.V408: // 4:4:4 8-bit
                              // These are JPEG Hardware decode formats (DXGI 1.4)
            case Xbox_D16_UNorm_S8_UInt:
            case Xbox_R16_UNorm_X8_Typeless:
            case Xbox_X16_Typeless_G8_UInt:
                // These are Xbox One platform specific types
                return true;

            default:
                return false;
        }
    }

    public static bool IsPalettized(this Format format)
    {
        switch (format)
        {
            case Format.AI44:
            case Format.IA44:
            case Format.P8:
            case Format.A8P8:
                return true;

            default:
                return false;
        }
    }

    public static bool IsDepthStencil(this Format format)
    {
        switch (format)
        {
            case Format.R32G8X24_Typeless:
            case Format.D32_Float_S8X24_UInt:
            case Format.R32_Float_X8X24_Typeless:
            case Format.X32_Typeless_G8X24_UInt:
            case Format.D32_Float:
            case Format.R24G8_Typeless:
            case Format.D24_UNorm_S8_UInt:
            case Format.R24_UNorm_X8_Typeless:
            case Format.X24_Typeless_G8_UInt:
            case Format.D16_UNorm:
            case Xbox_D16_UNorm_S8_UInt:
            case Xbox_R16_UNorm_X8_Typeless:
            case Xbox_X16_Typeless_G8_UInt:
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="Format"/> is a SRGB format.
    /// </summary>
    /// <param name="format">The <see cref="Format"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Format"/> is a SRGB format; otherwise, <c>false</c>.</returns>
    public static bool IsSRGB(this Format format)
    {
        switch (format)
        {
            case Format.R8G8B8A8_UNorm_SRgb:
            case Format.BC1_UNorm_SRgb:
            case Format.BC2_UNorm_SRgb:
            case Format.BC3_UNorm_SRgb:
            case Format.B8G8R8A8_UNorm_SRgb:
            case Format.B8G8R8X8_UNorm_SRgb:
            case Format.BC7_UNorm_SRgb:
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="Format"/> is typeless.
    /// </summary>
    /// <param name="format">The <see cref="Format"/>.</param>
    /// <param name="partialTypeless"></param>
    /// <returns><c>true</c> if the specified <see cref="Format"/> is typeless; otherwise, <c>false</c>.</returns>
    public static bool IsTypeless(this Format format, bool partialTypeless = true)
    {
        switch (format)
        {
            case Format.R32G32B32A32_Typeless:
            case Format.R32G32B32_Typeless:
            case Format.R16G16B16A16_Typeless:
            case Format.R32G32_Typeless:
            case Format.R32G8X24_Typeless:
            case Format.R10G10B10A2_Typeless:
            case Format.R8G8B8A8_Typeless:
            case Format.R16G16_Typeless:
            case Format.R32_Typeless:
            case Format.R24G8_Typeless:
            case Format.R8G8_Typeless:
            case Format.R16_Typeless:
            case Format.R8_Typeless:
            case Format.BC1_Typeless:
            case Format.BC2_Typeless:
            case Format.BC3_Typeless:
            case Format.BC4_Typeless:
            case Format.BC5_Typeless:
            case Format.B8G8R8A8_Typeless:
            case Format.B8G8R8X8_Typeless:
            case Format.BC6H_Typeless:
            case Format.BC7_Typeless:
                return true;

            case Format.R32_Float_X8X24_Typeless:
            case Format.X32_Typeless_G8X24_UInt:
            case Format.R24_UNorm_X8_Typeless:
            case Format.X24_Typeless_G8_UInt:
            case Xbox_R16_UNorm_X8_Typeless:
            case Xbox_X16_Typeless_G8_UInt:
                return partialTypeless;

            default:
                return false;
        }
    }

    public static bool IsBGR(this Format format)
    {
        switch (format)
        {
            case Format.B5G6R5_UNorm:
            case Format.B5G5R5A1_UNorm:
            case Format.B8G8R8A8_UNorm:
            case Format.B8G8R8X8_UNorm:
            case Format.B8G8R8A8_Typeless:
            case Format.B8G8R8A8_UNorm_SRgb:
            case Format.B8G8R8X8_Typeless:
            case Format.B8G8R8X8_UNorm_SRgb:
            case Format.B4G4R4A4_UNorm:
                return true;

            default:
                return false;
        }
    }

    public static void GetSurfaceInfo(
        Format format,
        int width,
        int height,
        out int rowPitch,
        out int slicePitch,
        out int rowCount)
    {
        bool bc = false;
        bool packed = false;
        bool planar = false;
        int bpe = 0;

        switch (format)
        {
            case Format.BC1_Typeless:
            case Format.BC1_UNorm:
            case Format.BC1_UNorm_SRgb:
            case Format.BC4_Typeless:
            case Format.BC4_UNorm:
            case Format.BC4_SNorm:
                bc = true;
                bpe = 8;
                break;

            case Format.BC2_Typeless:
            case Format.BC2_UNorm:
            case Format.BC2_UNorm_SRgb:
            case Format.BC3_Typeless:
            case Format.BC3_UNorm:
            case Format.BC3_UNorm_SRgb:
            case Format.BC5_Typeless:
            case Format.BC5_UNorm:
            case Format.BC5_SNorm:
            case Format.BC6H_Typeless:
            case Format.BC6H_Uf16:
            case Format.BC6H_Sf16:
            case Format.BC7_Typeless:
            case Format.BC7_UNorm:
            case Format.BC7_UNorm_SRgb:
                bc = true;
                bpe = 16;
                break;

            case Format.R8G8_B8G8_UNorm:
            case Format.G8R8_G8B8_UNorm:
            case Format.YUY2:
                packed = true;
                bpe = 4;
                break;

            case Format.Y210:
            case Format.Y216:
                packed = true;
                bpe = 8;
                break;

            case Format.NV12:
            case Format.Opaque420:
            case Format.P208:
                planar = true;
                bpe = 2;
                break;

            case Format.P010:
            case Format.P016:
                planar = true;
                bpe = 4;
                break;

            default:
                break;
        }

        if (bc)
        {
            int numBlocksWide = 0;
            if (width > 0)
            {
                numBlocksWide = Math.Max(1, (width + 3) / 4);
            }
            int numBlocksHigh = 0;
            if (height > 0)
            {
                numBlocksHigh = Math.Max(1, (height + 3) / 4);
            }
            rowPitch = numBlocksWide * bpe;
            rowCount = numBlocksHigh;
            slicePitch = rowPitch * numBlocksHigh;
        }
        else if (packed)
        {
            rowPitch = ((width + 1) >> 1) * bpe;
            rowCount = height;
            slicePitch = rowPitch * height;
        }
        else if (format == Format.NV11)
        {
            rowPitch = ((width + 3) >> 2) * 4;
            rowCount = height * 2; // Direct3D makes this simplifying assumption, although it is larger than the 4:1:1 data
            slicePitch = rowPitch * rowCount;
        }
        else if (planar)
        {
            rowPitch = ((width + 1) >> 1) * bpe;
            slicePitch = (rowPitch * height) + ((rowPitch * height + 1) >> 1);
            rowCount = (int)(height + ((height + 1u) >> 1));
        }
        else
        {
            int bpp = GetBitsPerPixel(format);
            rowPitch = (width * bpp + 7) / 8; // round up to nearest byte
            rowCount = height;
            slicePitch = rowPitch * height;
        }
    }

    public static void GetSurfaceInfo(Format format, int width, int height, out int rowPitch,out int slicePitch)
    {
        GetSurfaceInfo(format, width, height, out rowPitch, out slicePitch, out _);
    }
}
