// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using Vortice.DirectX.Direct2D;

namespace Vortice.DirectX.DirectWrite
{
    public partial class IDWriteFactory2
    {
        public IDWriteColorGlyphRunEnumerator TranslateColorGlyphRun(
            float baselineOriginX, 
            float baselineOriginY,
            ref GlyphRun glyphRun)
        {
            return TranslateColorGlyphRun(
                baselineOriginX,
                baselineOriginY,
                ref glyphRun,
                null,
                MeasuringMode.Natural,
                null,
                0);
        }
    }
}
