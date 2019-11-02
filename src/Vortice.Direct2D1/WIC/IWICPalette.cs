using System;
using System.Collections.Generic;
using System.Text;

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
