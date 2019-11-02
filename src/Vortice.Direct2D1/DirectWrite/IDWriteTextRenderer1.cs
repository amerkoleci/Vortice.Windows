using System;
using System.Collections.Generic;
using System.Text;
using SharpGen.Runtime;
using Vortice.Direct2D1;

namespace Vortice.DirectWrite
{
    [Shadow(typeof(IDWriteTextRenderer1Shadow))]
    public partial interface IDWriteTextRenderer1
    {
        void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, MeasuringMode measuringMode, GlyphRun glyphRun,ref GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect);
        void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, GlyphOrientationAngle orientationAngle, IDWriteInlineObject inlineObject, SharpGen.Runtime.Win32.RawBool isSideways, SharpGen.Runtime.Win32.RawBool isRightToLeft, IUnknown clientDrawingEffect);
        void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, ref Strikethrough strikethrough, IUnknown clientDrawingEffect);
        void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, GlyphOrientationAngle orientationAngle, ref Underline underline, IUnknown clientDrawingEffect);
    }
}
