// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectWrite
{
    public partial class IDWriteFontFamily2
    {
        public IDWriteFontList2 GetMatchingFonts(FontAxisValue[] fontAxisValues)
        {
            return GetMatchingFonts(fontAxisValues, fontAxisValues.Length);
        }
    }
}
