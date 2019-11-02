using System;
using System.Collections.Generic;
using System.Text;
using SharpGen.Runtime;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    partial class IDWriteTextRendererNative: IDWriteTextRenderer
    {
        public void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun,ref GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect) => DrawGlyphRun_(clientDrawingContext, baselineOriginX, baselineOriginY, measuringMode, glyphRun,ref glyphRunDescription, clientDrawingEffect);
        public void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Underline underline, IUnknown clientDrawingEffect) => DrawUnderline_(clientDrawingContext, baselineOriginX, baselineOriginY, ref underline, clientDrawingEffect);
        public void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Strikethrough strikethrough, IUnknown clientDrawingEffect) => DrawStrikethrough_(clientDrawingContext, baselineOriginX, baselineOriginY, ref strikethrough, clientDrawingEffect);
        public void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, IDWriteInlineObject inlineObject, bool isSideways, bool isRightToLeft, IUnknown clientDrawingEffect) => DrawInlineObject_(clientDrawingContext, originX, originY, inlineObject, isSideways, isRightToLeft, clientDrawingEffect);

    }
}
