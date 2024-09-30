// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;
using Vortice.DCommon;
using System.Numerics;

namespace Vortice.DirectWrite;

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

    public IDWriteColorGlyphRunEnumerator TranslateColorGlyphRun(in Vector2 baselineOrigin, GlyphRun glyphRun)
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

    public Result TranslateColorGlyphRun(in Vector2 baselineOrigin, GlyphRun glyphRun,
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
