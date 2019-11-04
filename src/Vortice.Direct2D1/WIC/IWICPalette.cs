// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.WIC
{
    partial class IWICPalette
    {
        public int[] GetColors()
        {
            var colors = new int[ColorCount];
            GetColors(colors.Length, colors, out _);
            return colors;
        }
    }
}
