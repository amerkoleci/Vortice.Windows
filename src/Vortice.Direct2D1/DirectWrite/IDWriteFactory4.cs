// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Numerics;
using SharpGen.Runtime;
using Vortice.DCommon;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    public partial class IDWriteFactory4
    {
        public Result TranslateColorGlyphRun(
            in PointF baselineOrigin,
            GlyphRun glyphRun,
            GlyphImageFormats desiredGlyphImageFormats,
            MeasuringMode measuringMode,
            out IDWriteColorGlyphRunEnumerator1 colorLayers)
        {
            return TranslateColorGlyphRun(baselineOrigin, glyphRun, null, desiredGlyphImageFormats, measuringMode, null, 0, out colorLayers);
        }

        public IDWriteColorGlyphRunEnumerator1 TranslateColorGlyphRun(
            in PointF baselineOrigin,
            GlyphRun glyphRun,
            GlyphImageFormats desiredGlyphImageFormats,
            MeasuringMode measuringMode = MeasuringMode.Natural)
        {
            TranslateColorGlyphRun(baselineOrigin, glyphRun, null,
                desiredGlyphImageFormats,
                measuringMode, null, 0, out IDWriteColorGlyphRunEnumerator1 colorLayers).CheckError();
            return colorLayers;
        }
    }
}
