// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class IDWriteColorGlyphRunEnumerator1
{
    public new ColorGlyphRun1 CurrentRun => GetCurrentRun();

    internal new unsafe ColorGlyphRun1 GetCurrentRun()
    {
        ColorGlyphRun1 colorGlyphRun = new();
        ColorGlyphRun1.__Native* colorGlyphRun_ = (ColorGlyphRun1.__Native*)GetCurrentRun_();
        colorGlyphRun.__MarshalFrom(ref *colorGlyphRun_);
        return colorGlyphRun;
    }
}
