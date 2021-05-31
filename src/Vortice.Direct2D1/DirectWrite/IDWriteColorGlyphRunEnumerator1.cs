// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectWrite
{
    partial class IDWriteColorGlyphRunEnumerator1
    {
        public new ColorGlyphRun1 CurrentRun => GetCurrentRun();

        internal new unsafe ColorGlyphRun1 GetCurrentRun()
        {
            ColorGlyphRun1 colorGlyphRun = default;
            ColorGlyphRun1.__Native* colorGlyphRun_ = (ColorGlyphRun1.__Native*)GetCurrentRun_();
            colorGlyphRun.__MarshalFrom(ref *colorGlyphRun_);
            return colorGlyphRun;
        }
    }
}
