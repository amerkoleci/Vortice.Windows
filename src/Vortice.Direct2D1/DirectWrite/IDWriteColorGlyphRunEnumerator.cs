// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public partial class IDWriteColorGlyphRunEnumerator
{
    public ColorGlyphRun CurrentRun => GetCurrentRun();

    internal unsafe ColorGlyphRun GetCurrentRun()
    {
        ColorGlyphRun colorGlyphRun = new();
        ColorGlyphRun.__Native* colorGlyphRun_ = (ColorGlyphRun.__Native*)GetCurrentRun_();
        colorGlyphRun.__MarshalFrom(ref *colorGlyphRun_);
        return colorGlyphRun;
    }
}
