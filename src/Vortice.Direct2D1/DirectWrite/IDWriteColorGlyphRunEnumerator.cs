// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using Vortice.DCommon;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    public partial class IDWriteColorGlyphRunEnumerator
    {
        public ColorGlyphRun CurrentRun => GetCurrentRun();

        internal unsafe ColorGlyphRun GetCurrentRun()
        {
            ColorGlyphRun colorGlyphRun = default;
            ColorGlyphRun.__Native* colorGlyphRun_ = (ColorGlyphRun.__Native*)GetCurrentRun_();
            colorGlyphRun.__MarshalFrom(ref *colorGlyphRun_);
            return colorGlyphRun;
        }
    }
}
