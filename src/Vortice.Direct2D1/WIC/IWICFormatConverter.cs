// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public partial class IWICFormatConverter
{
    /// <summary>
    /// Initializes the format converter.
    /// </summary>
    /// <param name="source">The input bitmap to convert</param>
    /// <param name="dstFormat">The destination format.</param>
    /// <returns></returns>
    public Result Initialize(IWICBitmapSource source, Guid dstFormat)
    {
        return Initialize(source, dstFormat, BitmapDitherType.None, null, 0.0, BitmapPaletteType.Custom);
    }
}
