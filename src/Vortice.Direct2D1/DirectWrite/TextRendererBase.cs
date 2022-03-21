// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using Vortice.DCommon;

namespace Vortice.DirectWrite;

public abstract class TextRendererBase : CallbackBase, IDWriteTextRenderer
{
    public virtual Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext) => Matrix3x2.Identity;
    public virtual float GetPixelsPerDip(IntPtr clientDrawingContext) => 1.0f;
    public virtual RawBool IsPixelSnappingDisabled(IntPtr clientDrawingContext) => false;

    public virtual void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun, GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect)
    {
    }

    public virtual void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, IDWriteInlineObject inlineObject, RawBool isSideways, RawBool isRightToLeft, IUnknown clientDrawingEffect)
    {

    }

    public virtual void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Strikethrough strikethrough, IUnknown clientDrawingEffect)
    {

    }

    public virtual void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Underline underline, IUnknown clientDrawingEffect)
    {

    }
}
