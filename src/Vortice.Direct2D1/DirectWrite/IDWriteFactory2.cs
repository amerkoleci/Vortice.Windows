// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using Vortice.DCommon;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    public partial class IDWriteFactory2
    {
        public IDWriteColorGlyphRunEnumerator TranslateColorGlyphRun(
            float baselineOriginX, 
            float baselineOriginY,
            GlyphRun glyphRun)
        {
            return TranslateColorGlyphRun(
                baselineOriginX,
                baselineOriginY,
                glyphRun,
                null,
                MeasuringMode.Natural,
                null,
                0);
        }
    }
}
