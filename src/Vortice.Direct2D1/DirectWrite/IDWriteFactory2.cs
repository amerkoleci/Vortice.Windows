// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;
using SharpGen.Runtime;
using Vortice.DCommon;

namespace Vortice.DirectWrite
{
    public partial class IDWriteFactory2
    {
        public IDWriteColorGlyphRunEnumerator TranslateColorGlyphRun(
            float baselineOriginX, 
            float baselineOriginY,
            GlyphRun glyphRun)
        {
            TranslateColorGlyphRun(
                baselineOriginX,
                baselineOriginY,
                glyphRun,
                null,
                MeasuringMode.Natural,
                null,
                0,
                out IDWriteColorGlyphRunEnumerator colorLayers).CheckError();
            return colorLayers;
        }

        public IDWriteColorGlyphRunEnumerator TranslateColorGlyphRun(in PointF baselineOrigin, GlyphRun glyphRun)
        {
            TranslateColorGlyphRun(
                baselineOrigin.X,
                baselineOrigin.Y,
                glyphRun,
                null,
                MeasuringMode.Natural,
                null,
                0,
                out IDWriteColorGlyphRunEnumerator colorLayers).CheckError();
            return colorLayers;
        }

        public Result TranslateColorGlyphRun(
            float baselineOriginX,
            float baselineOriginY,
            GlyphRun glyphRun,
            out IDWriteColorGlyphRunEnumerator colorLayers)
        {
            return TranslateColorGlyphRun(
                baselineOriginX,
                baselineOriginY,
                glyphRun,
                null,
                MeasuringMode.Natural,
                null,
                0,
                out colorLayers);
        }

        public Result TranslateColorGlyphRun(in PointF baselineOrigin, GlyphRun glyphRun,
            out IDWriteColorGlyphRunEnumerator colorLayers)
        {
            return TranslateColorGlyphRun(
                baselineOrigin.X,
                baselineOrigin.Y,
                glyphRun,
                null,
                MeasuringMode.Natural,
                null,
                0,
                out colorLayers);
        }
    }
}
