// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DCommon;
using Vortice.DirectWrite;
using Vortice.DXGI;
using Vortice.WIC;

namespace Vortice.Direct2D1;

public partial class ID2D1RenderTarget
{
    public Size Dpi
    {
        get
        {
            GetDpi(out float dpiX, out float dpiY);
            return new(dpiX, dpiY);
        }
        set
        {
            SetDpi(value.Width, value.Height);
        }
    }

    public ID2D1Bitmap CreateBitmap(SizeI size)
    {
        return CreateBitmap(size, IntPtr.Zero, 0, new BitmapProperties(DCommon.PixelFormat.Unknown));
    }

    public ID2D1Bitmap CreateBitmap(SizeI size, BitmapProperties bitmapProperties)
    {
        return CreateBitmap(size, IntPtr.Zero, 0, bitmapProperties);
    }

    public ID2D1Bitmap CreateBitmap(SizeI size, IntPtr sourceData, uint pitch)
    {
        return CreateBitmap(size, sourceData, pitch, new BitmapProperties(DCommon.PixelFormat.Unknown));
    }

    public void Clear(in Vortice.Mathematics.Color clearColor)
    {
        Clear(new Color4(clearColor));
    }

    public ID2D1SolidColorBrush CreateSolidColorBrush(in Vortice.Mathematics.Color color, BrushProperties? brushProperties = null)
    {
        Color4 color4 = new(color);
        return CreateSolidColorBrush(color4, brushProperties);
    }

    public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops)
    {
        return CreateGradientStopCollection_(gradientStops, (uint)gradientStops.Length, Gamma.StandardRgb, ExtendMode.Clamp);
    }

    public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops, ExtendMode extendMode)
    {
        return CreateGradientStopCollection_(gradientStops, (uint)gradientStops.Length, Gamma.StandardRgb, extendMode);
    }

    public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops, Gamma colorInterpolationGamma, ExtendMode extendMode)
    {
        return CreateGradientStopCollection_(gradientStops, (uint)gradientStops.Length, colorInterpolationGamma, extendMode);
    }

    public ID2D1LinearGradientBrush CreateLinearGradientBrush(LinearGradientBrushProperties linearGradientBrushProperties, ID2D1GradientStopCollection gradientStopCollection)
    {
        return CreateLinearGradientBrush_(linearGradientBrushProperties, null, gradientStopCollection);
    }

    public ID2D1LinearGradientBrush CreateLinearGradientBrush(LinearGradientBrushProperties linearGradientBrushProperties, BrushProperties brushProperties, ID2D1GradientStopCollection gradientStopCollection)
    {
        return CreateLinearGradientBrush_(linearGradientBrushProperties, brushProperties, gradientStopCollection);
    }

    public ID2D1RadialGradientBrush CreateRadialGradientBrush(RadialGradientBrushProperties radialGradientBrushProperties, ID2D1GradientStopCollection gradientStopCollection)
    {
        return CreateRadialGradientBrush_(ref radialGradientBrushProperties, null, gradientStopCollection);
    }

    public ID2D1RadialGradientBrush CreateRadialGradientBrush(RadialGradientBrushProperties radialGradientBrushProperties, BrushProperties brushProperties, ID2D1GradientStopCollection gradientStopCollection)
    {
        return CreateRadialGradientBrush_(ref radialGradientBrushProperties, brushProperties, gradientStopCollection);
    }

    public ID2D1Bitmap CreateSharedBitmap(ID2D1Bitmap bitmap, BitmapProperties? bitmapProperties = default)
    {
        return CreateSharedBitmap(typeof(ID2D1Bitmap).GUID, bitmap.NativePointer, bitmapProperties);
    }

    public ID2D1Bitmap CreateSharedBitmap(IDXGISurface surface, BitmapProperties? bitmapProperties = default)
    {
        return CreateSharedBitmap(typeof(IDXGISurface).GUID, surface.NativePointer, bitmapProperties);
    }

    public ID2D1Bitmap CreateSharedBitmap(IWICBitmapLock bitmapLock, BitmapProperties? bitmapProperties = default)
    {
        return CreateSharedBitmap(typeof(IWICBitmapLock).GUID, bitmapLock.NativePointer, bitmapProperties);
    }

    /// <summary>
    /// Creates a new bitmap render target for use during intermediate offscreen drawing that is compatible with the current render target and has the same size, DPI, and pixel format (but not alpha mode) as the current render target.
    /// </summary>
    /// <returns></returns>
    public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget()
    {
        return CreateCompatibleRenderTarget(null, null, null, CompatibleRenderTargetOptions.None);
    }

    /// <summary>
    /// Creates a bitmap render target for use during intermediate offscreen drawing that is compatible with the current render target.
    /// </summary>
    /// <param name="desiredSize">The desired size of the new render target in device-independent pixels. The pixel size is computed from the desired size using the parent target DPI. If the desiredSize maps to a integer-pixel size, the DPI of the compatible render target is the same as the DPI of the parent target. If desiredSize maps to a fractional-pixel size, the pixel size is rounded up to the nearest integer and the DPI for the compatible render target is slightly higher than the DPI of the parent render target. In all cases, the coordinate (desiredSize.width, desiredSize.height) maps to the lower-right corner of the compatible render target.</param>
    /// <returns></returns>
    public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget(SizeI desiredSize)
    {
        return CreateCompatibleRenderTarget(desiredSize, null, null, CompatibleRenderTargetOptions.None);
    }

    /// <summary>
    /// Creates a bitmap render target for use during intermediate offscreen drawing that is compatible with the current render target.
    /// </summary>
    /// <param name="desiredSize">The desired size of the new render target (in device-independent pixels), if it should be different from the original render target. For more info, see the Remarks section.</param>
    /// <param name="desiredPixelSize">The desired size of the new render target in pixels if it should be different from the original render target. For more information, see the Remarks section.</param>
    /// <returns></returns>
    public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget(SizeI desiredSize, SizeI desiredPixelSize)
    {
        return CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, null, CompatibleRenderTargetOptions.None);
    }

    /// <summary>
    /// Creates a bitmap render target for use during intermediate offscreen drawing that is compatible with the current render target.
    /// </summary>
    /// <param name="desiredSize">The desired size of the new render target (in device-independent pixels), if it should be different from the original render target. For more info, see the Remarks section.</param>
    /// <param name="desiredPixelSize">The desired size of the new render target in pixels if it should be different from the original render target. For more information, see the Remarks section.</param>
    /// <param name="desiredFormat">
    /// The desired pixel format and alpha mode of the new render target.
    /// If the pixel format is set to <see cref="Format.Unknown"/>, the new render target uses the same pixel format as the original render target.
    /// If the alpha mode is <see cref="DCommon.AlphaMode.Unknown"/>, the alpha mode of the new render target defaults to <see cref="DCommon.AlphaMode.Premultiplied"/>.
    /// </param>
    /// <returns></returns>
    public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget(SizeI desiredSize, SizeI desiredPixelSize, DCommon.PixelFormat desiredFormat)
    {
        return CreateCompatibleRenderTarget(desiredSize, desiredPixelSize, desiredFormat, CompatibleRenderTargetOptions.None);
    }

    /// <summary>
    /// Draws the specified bitmap.
    /// </summary>
    /// <param name="bitmap">The <see cref="ID2D1Bitmap"/> to render.</param>
    public void DrawBitmap(ID2D1Bitmap bitmap)
    {
        DrawBitmap(bitmap, null, 1.0f, BitmapInterpolationMode.Linear, null);
    }

    /// <summary>
    /// Draws the specified bitmap with given opacity and interpolation mode. 
    /// </summary>
    /// <param name="bitmap">The <see cref="ID2D1Bitmap"/> to render.</param>
    /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies an opacity value to apply to the bitmap; this value is multiplied against the alpha values of the bitmap's contents. The default value is 1.0f.</param>
    /// <param name="interpolationMode">The interpolation mode to use if the bitmap is scaled or rotated by the drawing operation. The default value is <see cref="BitmapInterpolationMode.Linear" />.</param>
    public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, BitmapInterpolationMode interpolationMode)
    {
        DrawBitmap(bitmap, null, opacity, interpolationMode, null);
    }

    /// <summary>	
    /// Draws the specified bitmap after scaling it to the size of the specified rectangle. 	
    /// </summary>	
    /// <param name="bitmap">The <see cref="ID2D1Bitmap"/> to render.</param>
    /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies an opacity value to apply to the bitmap; this value is multiplied against the alpha values of the bitmap's contents.  The default value is 1.0f. </param>
    /// <param name="interpolationMode">The interpolation mode to use if the bitmap is scaled or rotated by the drawing operation. The default value is <see cref="F:Vortice.Direct2D1.BitmapInterpolationMode.Linear" />.  </param>
    /// <param name="sourceRectangle">The size and position, in device-independent pixels in the bitmap's coordinate space, of the area within the bitmap to be drawn; NULL to draw the entire bitmap.  </param>
    public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, BitmapInterpolationMode interpolationMode, Rect sourceRectangle)
    {
        RawRectF rawRect = sourceRectangle;
        DrawBitmap(bitmap, null, opacity, interpolationMode, rawRect);
    }

    /// <summary>
    /// Draws the specified bitmap after scaling it to the size of the specified rectangle. 	
    /// </summary>
    /// <param name="bitmap">The <see cref="ID2D1Bitmap"/> to render.</param>
    /// <param name="destinationRectangle">The size and position, in device-independent pixels in the render target's coordinate space, of the area to which the bitmap is drawn; If the rectangle is specified but not well-ordered, nothing is drawn, but the render target does not enter an error state.</param>
    /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies an opacity value to apply to the bitmap; this value is multiplied against the alpha values of the bitmap's contents.  The default value is 1.0f. </param>
    /// <param name="interpolationMode">The interpolation mode to use if the bitmap is scaled or rotated by the drawing operation. The default value is <see cref="F:Vortice.Direct2D1.BitmapInterpolationMode.Linear" />.  </param>
    /// <param name="sourceRectangle">The size and position, in device-independent pixels in the bitmap's coordinate space, of the area within the bitmap to be drawn; NULL to draw the entire bitmap.  </param>
    public void DrawBitmap(ID2D1Bitmap bitmap, Rect destinationRectangle, float opacity, BitmapInterpolationMode interpolationMode, Rect sourceRectangle)
    {
        RawRectF rawDestinationRectangle = destinationRectangle;
        RawRectF rawSourceRectangle = sourceRectangle;
        DrawBitmap(bitmap, (RawRectF?)rawDestinationRectangle, opacity, interpolationMode, rawSourceRectangle);
    }

    /// <summary>	
    /// Draws the outline of the specified ellipse using the specified brush. 	
    /// </summary>	
    /// <param name="ellipse">The position and radius of the ellipse to draw, in device-independent pixels.</param>
    /// <param name="brush">The brush used to paint the ellipse's outline.</param>
    public void DrawEllipse(Ellipse ellipse, ID2D1Brush brush)
    {
        DrawEllipse(ellipse, brush, 1.0f, null);
    }

    /// <summary>	
    /// Draws the outline of the specified ellipse using the brush and stroke width. 	
    /// </summary>	
    /// <param name="ellipse">The position and radius of the ellipse to draw, in device-independent pixels. </param>
    /// <param name="brush">The brush used to paint the ellipse's outline. </param>
    /// <param name="strokeWidth">The thickness of the ellipse's stroke. The stroke is centered on the ellipse's outline. </param>
    public void DrawEllipse(Ellipse ellipse, ID2D1Brush brush, float strokeWidth)
    {
        DrawEllipse(ellipse, brush, strokeWidth, null);
    }

    /// <summary>	
    /// Draws the outline of the specified geometry.
    /// </summary>	
    /// <param name="geometry">The <see cref="ID2D1Geometry"/> to draw.</param>
    /// <param name="brush">The brush used to paint the geometry's stroke.</param>
    public void DrawGeometry(ID2D1Geometry geometry, ID2D1Brush brush)
    {
        DrawGeometry(geometry, brush, 1.0f, null);
    }

    /// <summary>	
    /// Draws the outline of the specified geometry with given brush.
    /// </summary>	
    /// <param name="geometry">The <see cref="ID2D1Geometry"/> to draw. </param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the geometry's stroke. </param>
    /// <param name="strokeWidth">The thickness of the geometry's stroke. The stroke is centered on the geometry's outline.</param>
    public void DrawGeometry(ID2D1Geometry geometry, ID2D1Brush brush, float strokeWidth)
    {
        DrawGeometry(geometry, brush, strokeWidth, null);
    }

    /// <summary>	
    /// Draws a line between the specified points using given brush. 	
    /// </summary>	
    /// <param name="point0">The start point of the line, in device-independent pixels. </param>
    /// <param name="point1">The end point of the line, in device-independent pixels. </param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the line's stroke. </param>
    public void DrawLine(Vector2 point0, Vector2 point1, ID2D1Brush brush)
    {
        DrawLine(point0, point1, brush, 1.0f, null);
    }

    /// <summary>	
    /// Draws a line between the specified points using given brush and stroke width. 	
    /// </summary>	
    /// <param name="point0">The start point of the line, in device-independent pixels. </param>
    /// <param name="point1">The end point of the line, in device-independent pixels. </param>
    /// <param name="brush">The brush used to paint the line's stroke. </param>
    /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the stroke. If this parameter isn't specified, it defaults to 1.0f.  The stroke is centered on the line. </param>
    public void DrawLine(in Vector2 point0, in Vector2 point1, ID2D1Brush brush, float strokeWidth)
    {
        DrawLine(point0, point1, brush, strokeWidth, null);
    }

    /// <summary>	
    /// Draws the outline of a rectangle that has the specified dimensions. 
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle to draw, in device-independent pixels. </param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rectangle's stroke. </param>
    public void DrawRectangle(in RawRectF rect, ID2D1Brush brush)
    {
        DrawRectangle(rect, brush, 1.0f, null);
    }

    /// <summary>	
    /// Draws the outline of a rectangle that has the specified dimensions and stroke style.
    /// </summary>
    /// <param name="rectangle">The dimensions of the rectangle to draw, in device-independent pixels.</param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rectangle's stroke.</param>
    /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the rectangle's stroke. The stroke is centered on the rectangle's outline.</param>
    public void DrawRectangle(in Rect rectangle, ID2D1Brush brush, float strokeWidth)
    {
        RawRectF rect = rectangle;
        DrawRectangle(rect, brush, strokeWidth, null);
    }

    /// <summary>	
    /// Draws the outline of a rectangle that has the specified dimensions. 
    /// </summary>
    /// <param name="rectangle">The dimensions of the rectangle to draw, in device-independent pixels. </param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rectangle's stroke. </param>
    public void DrawRectangle(in Rect rectangle, ID2D1Brush brush)
    {
        RawRectF rect = rectangle;
        DrawRectangle(rect, brush, 1.0f, null);
    }

    /// <summary>	
    /// Draws the outline of a rectangle that has the specified dimensions and stroke style.
    /// </summary>
    /// <param name="rect">The dimensions of the rectangle to draw, in device-independent pixels.</param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rectangle's stroke.</param>
    /// <param name="strokeWidth">A value greater than or equal to 0.0f that specifies the width of the rectangle's stroke. The stroke is centered on the rectangle's outline.</param>
    public void DrawRectangle(in RawRectF rect, ID2D1Brush brush, float strokeWidth)
    {
        DrawRectangle(rect, brush, strokeWidth, null);
    }

    public void FillRectangle(in Rect rectangle, ID2D1Brush brush)
    {
        RawRectF rect = rectangle;
        FillRectangle(rect, brush);
    }

    public void FillGeometry(ID2D1Geometry geometry, ID2D1Brush brush)
    {
        FillGeometry(geometry, brush, null);
    }

    /// <summary>	
    /// Draws the outline of the specified rounded rectangle.
    /// </summary>
    /// <param name="roundedRect">The dimensions of the rounded rectangle to draw, in device-independent pixels. </param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rounded rectangle's outline.  </param>
    public void DrawRoundedRectangle(RoundedRectangle roundedRect, ID2D1Brush brush)
    {
        DrawRoundedRectangle(ref roundedRect, brush, 1.0f, null);
    }

    /// <summary>	
    /// Draws the outline of the specified rounded rectangle.
    /// </summary>
    /// <param name="roundedRect">The dimensions of the rounded rectangle to draw, in device-independent pixels. </param>
    /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rounded rectangle's outline.  </param>
    /// <param name="strokeWidth">The width of the rounded rectangle's stroke. The stroke is centered on the rounded rectangle's outline. The default value is 1.0f.  </param>
    public void DrawRoundedRectangle(RoundedRectangle roundedRect, ID2D1Brush brush, float strokeWidth)
    {
        DrawRoundedRectangle(ref roundedRect, brush, strokeWidth, null);
    }

    /// <summary>	
    /// Draws the outline of the specified rounded rectangle using the specified stroke style.
    /// </summary>	
    /// <param name="roundedRect">The dimensions of the rounded rectangle to draw, in device-independent pixels.</param>
    /// <param name="brush">The brush used to paint the rounded rectangle's outline.</param>
    /// <param name="strokeWidth">The width of the rounded rectangle's stroke. The stroke is centered on the rounded rectangle's outline. The default value is 1.0f.</param>
    /// <param name="strokeStyle">The style of the rounded rectangle's stroke, or NULL to paint a solid stroke. The default value is NULL. </param>
    public void DrawRoundedRectangle(RoundedRectangle roundedRect, ID2D1Brush brush, float strokeWidth, ID2D1StrokeStyle strokeStyle)
    {
        DrawRoundedRectangle(ref roundedRect, brush, strokeWidth, strokeStyle);
    }

    public void FillRoundedRectangle(RoundedRectangle roundedRect, ID2D1Brush brush)
    {
        FillRoundedRectangle(ref roundedRect, brush);
    }

    /// <summary>	
    /// Draws the specified text using the format information provided by an <see cref="IDWriteTextFormat" /> object. 	
    /// </summary>	
    /// <remarks>
    /// To create an <see cref="IDWriteTextFormat"/> object, create an <see cref="IDWriteFactory"/> and call its CreateTextFormat method.
    /// </remarks>
    /// <param name="text">A reference to an array of Unicode characters to draw. </param>
    /// <param name="textFormat">An object that describes formatting details of the text to draw, such as the font, the font size, and flow direction.</param>
    /// <param name="layoutRect">The size and position of the area in which the text is drawn.  </param>
    /// <param name="defaultForegroundBrush">The brush used to paint the text. </param>
    public void DrawText(string text, IDWriteTextFormat textFormat, Rect layoutRect, ID2D1Brush defaultForegroundBrush)
    {
        RawRectF rawLayoutRect = layoutRect;
        DrawText(text, (uint)text.Length, textFormat, rawLayoutRect, defaultForegroundBrush, DrawTextOptions.None, MeasuringMode.Natural);
    }

    /// <summary>	
    /// Draws the specified text using the format information provided by an <see cref="T:Vortice.DirectWrite.TextFormat" /> object. 	
    /// </summary>
    /// <remarks>
    /// To create an <see cref="IDWriteTextFormat"/> object, create an <see cref="IDWriteFactory"/> and call its CreateTextFormat method.
    /// </remarks>
    /// <param name="text">A reference to an array of Unicode characters to draw.</param>
    /// <param name="textFormat">An object that describes formatting details of the text to draw, such as the font, the font size, and flow direction.   </param>
    /// <param name="layoutRect">The size and position of the area in which the text is drawn.  </param>
    /// <param name="defaultForegroundBrush">The brush used to paint the text. </param>
    /// <param name="options">A value that indicates whether the text should be snapped to pixel boundaries and whether the text should be clipped to the layout rectangle. The default value is <see cref="F:Vortice.Direct2D1.DrawTextOptions.None" />, which indicates that text should be snapped to pixel boundaries and it should not be clipped to the layout rectangle. </param>
    public void DrawText(string text, IDWriteTextFormat textFormat, Rect layoutRect, ID2D1Brush defaultForegroundBrush, DrawTextOptions options)
    {
        RawRectF rawLayoutRect = layoutRect;
        DrawText(text, (uint)text.Length, textFormat, rawLayoutRect, defaultForegroundBrush, options, MeasuringMode.Natural);
    }

    /// <summary>	
    /// Draws the specified text using the format information provided by an <see cref="T:Vortice.DirectWrite.TextFormat" /> object. 	
    /// </summary>
    /// <remarks>
    /// To create an <see cref="IDWriteTextFormat"/> object, create an <see cref="IDWriteFactory"/> and call its CreateTextFormat method.
    /// </remarks>
    /// <param name="text">A reference to an array of Unicode characters to draw.  </param>
    /// <param name="textFormat">An object that describes formatting details of the text to draw, such as the font, the font size, and flow direction.   </param>
    /// <param name="layoutRect">The size and position of the area in which the text is drawn.  </param>
    /// <param name="defaultForegroundBrush">The brush used to paint the text. </param>
    /// <param name="options">A value that indicates whether the text should be snapped to pixel boundaries and whether the text should be clipped to the layout rectangle. The default value is <see cref="F:Vortice.Direct2D1.DrawTextOptions.None" />, which indicates that text should be snapped to pixel boundaries and it should not be clipped to the layout rectangle. </param>
    /// <param name="measuringMode">A value that indicates how glyph metrics are used to measure text when it is formatted.  The default value is DWRITE_MEASURING_MODE_NATURAL.  </param>
    public void DrawText(string text, IDWriteTextFormat textFormat, Rect layoutRect, ID2D1Brush defaultForegroundBrush, DrawTextOptions options, MeasuringMode measuringMode)
    {
        RawRectF rawLayoutRect = layoutRect;
        DrawText(text, (uint)text.Length, textFormat, rawLayoutRect, defaultForegroundBrush, options, measuringMode);
    }

    /// <summary>	
    /// Draws the formatted text described by the specified <see cref="IDWriteTextLayout"/> object.
    /// </summary>
    /// <param name="origin">The point, described in device-independent pixels, at which the upper-left corner of the text described by textLayout is drawn. </param>
    /// <param name="textLayout">The formatted text to draw. Any drawing effects that do not inherit from <see cref="ID2D1Resource" /> are ignored. If there are drawing effects that inherit from <see cref="ID2D1Resource"/> that are not brushes, this method fails and the render target is put in an error state.  </param>
    /// <param name="defaultForegroundBrush">The <see cref="ID2D1Brush"/> used to paint any text in textLayout that does not already have a brush associated with it as a drawing effect (specified by the <see cref="IDWriteTextLayout.SetDrawingEffect(IUnknown, TextRange)"/> method).</param>
    public void DrawTextLayout(Vector2 origin, IDWriteTextLayout textLayout, ID2D1Brush defaultForegroundBrush)
    {
        DrawTextLayout(origin, textLayout, defaultForegroundBrush, DrawTextOptions.None);
    }

    public void DrawGlyphRun(float baselineOriginX, float baselineOriginY, GlyphRun glyphRun, ID2D1Brush foregroundBrush)
    {
        DrawGlyphRun(new Vector2(baselineOriginX, baselineOriginY), glyphRun, foregroundBrush, MeasuringMode.Natural);
    }

    public void DrawGlyphRun(Vector2 baselineOrigin, GlyphRun glyphRun, ID2D1Brush foregroundBrush)
    {
        DrawGlyphRun(baselineOrigin, glyphRun, foregroundBrush, MeasuringMode.Natural);
    }

    public Result EndDraw()
    {
        return EndDraw(out _, out _);
    }
}
