// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using Vortice.DCommon;

namespace Vortice.DirectWrite;

public abstract class TextRendererBase : CallbackBase, IDWriteTextRenderer
{
    /// <summary>
    /// Gets a transform that maps abstract coordinates to DIPs.
    /// </summary>
    /// <param name="clientDrawingContext"></param>
    /// <returns>Contains a structure which has transform information for pixel snapping.</returns>
    public virtual Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext)
    {
        return Matrix3x2.Identity;
    }

    /// <summary>
    /// Gets the number of physical pixels per DIP.
    /// </summary>
    /// <param name="clientDrawingContext">The drawing context passed to <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/>.</param>
    /// <returns>Ccontains the number of physical pixels per DIP.</returns>
    public virtual float GetPixelsPerDip(IntPtr clientDrawingContext)
    {
        return 1.0f;
    }

    /// <summary>
    /// Determines whether pixel snapping is disabled. The recommended default is <b>false</b>, unless doing animation that requires subpixel vertical placement.
    /// </summary>
    /// <param name="clientDrawingContext">The drawing context passed to <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/>.</param>
    /// <returns>Receives <b>true</b> if pixel snapping is disabled; otherwise, <b>false</b>.</returns>
    public virtual RawBool IsPixelSnappingDisabled(IntPtr clientDrawingContext)
    {
        return false;
    }

    /// <summary>
    /// <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/> calls this function to instruct the client to render a run of glyphs.
    /// </summary>
    /// <param name="clientDrawingContext">The drawing context passed to <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/>.</param>
    /// <param name="baselineOriginX">The pixel location (X-coordinate) at the baseline origin of the glyph run.</param>
    /// <param name="baselineOriginY">The pixel location (Y-coordinate) at the baseline origin of the glyph run.</param>
    /// <param name="measuringMode">The measuring method for glyphs in the run, used with the other properties to determine the rendering mode.</param>
    /// <param name="glyphRun">The <see cref="GlyphRun"/> instance to render.</param>
    /// <param name="glyphRunDescription">The <see cref="GlyphRunDescription"/> instance which contains properties of the characters associated with this run.</param>
    /// <param name="clientDrawingEffect">Application-defined drawing effects for the glyphs to render. Usually this argument represents effects such as the foreground brush filling the interior of text.</param>
    public virtual void DrawGlyphRun(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, MeasuringMode measuringMode, GlyphRun glyphRun, GlyphRunDescription glyphRunDescription, IUnknown clientDrawingEffect)
    {
    }

    /// <summary>
    /// <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/> calls this application callback when it needs to draw an inline object.
    /// </summary>
    /// <param name="clientDrawingContext">The drawing context passed to <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/>.</param>
    /// <param name="originX">X-coordinate at the top-left corner of the inline object.</param>
    /// <param name="originY">Y-coordinate at the top-left corner of the inline object.</param>
    /// <param name="inlineObject">The application-defined inline object set using <see cref="IDWriteTextLayout.SetInlineObject(IDWriteInlineObject, TextRange)"/> IDWriteTextFormat::SetInlineObject.</param>
    /// <param name="isSideways">A Boolean flag that indicates whether the object's baseline runs alongside the baseline axis of the line.</param>
    /// <param name="isRightToLeft">A Boolean flag that indicates whether the object is in a right-to-left context, hinting that the drawing may want to mirror the normal image.</param>
    /// <param name="clientDrawingEffect">Application-defined drawing effects for the glyphs to render. Usually this argument represents effects such as the foreground brush filling the interior of a line.</param>
    public virtual void DrawInlineObject(IntPtr clientDrawingContext, float originX, float originY, IDWriteInlineObject inlineObject, RawBool isSideways, RawBool isRightToLeft, IUnknown clientDrawingEffect)
    {

    }

    /// <summary>
    /// <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/> calls this function to instruct the client to draw a strikethrough.
    /// </summary>
    /// <param name="clientDrawingContext">The drawing context passed to <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/>.</param>
    /// <param name="baselineOriginX">The pixel location (X-coordinate) at the baseline origin of the run where strikethrough applies.</param>
    /// <param name="baselineOriginY">The pixel location (Y-coordinate) at the baseline origin of the run where strikethrough applies.</param>
    /// <param name="strikethrough"><see cref="Strikethrough"/> structure containing strikethrough logical information.</param>
    /// <param name="clientDrawingEffect">Application-defined effect to apply to the strikethrough. Usually this argument represents effects such as the foreground brush filling the interior of a line.</param>
    public virtual void DrawStrikethrough(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Strikethrough strikethrough, IUnknown clientDrawingEffect)
    {

    }

    /// <summary>
    /// <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/> calls this function to instruct the client to draw an underline.
    /// </summary>
    /// <param name="clientDrawingContext">The drawing context passed to <see cref="IDWriteTextLayout.Draw(IntPtr, IDWriteTextRenderer, float, float)"/>.</param>
    /// <param name="baselineOriginX">The pixel location (X-coordinate) at the baseline origin of the run where underline applies.</param>
    /// <param name="baselineOriginY">The pixel location (Y-coordinate) at the baseline origin of the run where underline applies.</param>
    /// <param name="underline"><see cref="Underline"/> structure containing underline logical information.</param>
    /// <param name="clientDrawingEffect">Application-defined effect to apply to the underline. Usually this argument represents effects such as the foreground brush filling the interior of a line.</param>
    public virtual void DrawUnderline(IntPtr clientDrawingContext, float baselineOriginX, float baselineOriginY, ref Underline underline, IUnknown clientDrawingEffect)
    {

    }
}
