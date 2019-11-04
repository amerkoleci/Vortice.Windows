using System;
using System.Collections.Generic;
using System.Text;
using SharpGen.Runtime;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    [Shadow(typeof(IDWriteTextRendererShadow))]
    partial interface IDWriteTextRenderer
    {
        void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun,ref GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect);

        void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Underline underline, IUnknown clientDrawingEffect);
        void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Strikethrough strikethrough, IUnknown clientDrawingEffect);

        void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, IDWriteInlineObject inlineObject, bool isSideways, bool isRightToLeft, IUnknown clientDrawingEffect);
    }
}
