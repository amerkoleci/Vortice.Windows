// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public static partial class WIC
{
    public static Guid GetGuid(ContainerFormat format)
    {
        return format switch
        {
            ContainerFormat.Bmp => ContainerFormatGuids.Bmp,
            ContainerFormat.Png => ContainerFormatGuids.Png,
            ContainerFormat.Ico => ContainerFormatGuids.Ico,
            ContainerFormat.Jpeg => ContainerFormatGuids.Jpeg,
            ContainerFormat.Tiff => ContainerFormatGuids.Tiff,
            ContainerFormat.Gif => ContainerFormatGuids.Gif,
            ContainerFormat.Wmp => ContainerFormatGuids.Wmp,
            ContainerFormat.Dds => ContainerFormatGuids.Dds,
            ContainerFormat.Adng => ContainerFormatGuids.Adng,
            ContainerFormat.Heif => ContainerFormatGuids.Heif,
            ContainerFormat.Webp => ContainerFormatGuids.Webp,
            ContainerFormat.Raw => ContainerFormatGuids.Raw,
            _ => throw new ArgumentException(nameof(format)),
        };
    }

    public static IWICBitmapSource WICConvertBitmapSource(Guid dstFormat, IWICBitmapSource source)
    {
        WICConvertBitmapSource_(dstFormat, source, out IWICBitmapSource destination).CheckError();
        return destination;
    }

    public static Result WICConvertBitmapSource(Guid dstFormat, IWICBitmapSource source, out IWICBitmapSource destination)
    {
        return WICConvertBitmapSource_(dstFormat, source, out destination);
    }

    public static IWICBitmap WICCreateBitmapFromSection(int width, int height, Guid pixelFormat, IntPtr section, int stride, int offset)
    {
        WICCreateBitmapFromSection_(width, height, pixelFormat, section, stride, offset, out IWICBitmap result).CheckError();
        return result;
    }

    public static Result WICCreateBitmapFromSection(int width, int height, Guid pixelFormat, IntPtr section, int stride, int offset, out IWICBitmap result)
    {
        return WICCreateBitmapFromSection_(width, height, pixelFormat, section, stride, offset, out result);
    }

    public static IWICBitmap WICCreateBitmapFromSectionEx(int width, int height, Guid pixelFormat, IntPtr section, int stride, int offset, SectionAccessLevel desiredAccessLevel)
    {
        WICCreateBitmapFromSectionEx_(width, height, pixelFormat, section, stride, offset, desiredAccessLevel, out IWICBitmap bitmap).CheckError();
        return bitmap;
    }

    public static Result WICCreateBitmapFromSectionEx(int width, int height, Guid pixelFormat, IntPtr section, int stride, int offset, SectionAccessLevel desiredAccessLevel, out IWICBitmap bitmap)
    {
        return WICCreateBitmapFromSectionEx_(width, height, pixelFormat, section, stride, offset, desiredAccessLevel, out bitmap);
    }
}
