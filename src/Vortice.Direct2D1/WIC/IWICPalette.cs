// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

partial class IWICPalette
{
    public uint[] GetColors()
    {
        var colors = new uint[ColorCount];
        GetColors((uint)colors.Length, colors, out _);
        return colors;
    }

    public void GetColors(uint[] colors)
    {
        GetColors((uint)colors.Length, colors, out _);
    }
}
