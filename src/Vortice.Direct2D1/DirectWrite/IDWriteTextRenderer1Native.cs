// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    internal partial class IDWriteTextRenderer1Native
    {
        public void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, MeasuringMode measuringMode, GlyphRun glyphRun,ref GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect) => DrawGlyphRun_(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, measuringMode, glyphRun,ref glyphRunDescription, clientDrawingEffect);
        public void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, GlyphOrientationAngle orientationAngle, IDWriteInlineObject inlineObject, RawBool isSideways,RawBool isRightToLeft, IUnknown clientDrawingEffect) => DrawInlineObject_(clientDrawingContext, originX, originY, orientationAngle, inlineObject, isSideways, isRightToLeft, clientDrawingEffect);
        public void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, ref Strikethrough strikethrough, IUnknown clientDrawingEffect) => DrawStrikethrough_(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, ref strikethrough, clientDrawingEffect);
        public void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, ref Underline underline, IUnknown clientDrawingEffect) => DrawUnderline_(clientDrawingContext, baselineOriginX, baselineOriginY, orientationAngle, ref underline, clientDrawingEffect);
    }
}
