// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

partial class IWICPalette
{
    public int[] GetColors()
    {
        var colors = new int[ColorCount];
        GetColors(colors.Length, colors, out _);
        return colors;
    }
}
