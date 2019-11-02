using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;
using Vortice.DirectWrite;

namespace Vortice.Direct2D1.DirectWrite
{
    public abstract class CustomTextRendereBase : CallbackBase, IDWriteTextRenderer
    {
        public abstract void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun, ref GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect);

        public abstract void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, IDWriteInlineObject inlineObject, bool isSideways, bool isRightToLeft, IUnknown clientDrawingEffect);

        public abstract void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Strikethrough strikethrough, IUnknown clientDrawingEffect);

        public abstract void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Underline underline, IUnknown clientDrawingEffect);

        public abstract void GetCurrentTransform(IntPtr clientDrawingContext, out Matrix3x2 transform);

        public abstract void GetPixelsPerDip(IntPtr clientDrawingContext, out float ixelsPerDipRef);

        public abstract void IsPixelSnappingDisabled(IntPtr clientDrawingContext, out RawBool isDisabled);
    }
}
