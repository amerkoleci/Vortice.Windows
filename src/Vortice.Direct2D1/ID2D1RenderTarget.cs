// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;
using SharpGen.Runtime;
using Vortice.DCommon;
using Vortice.DirectWrite;
using Vortice.DXGI;
using Vortice.WIC;

namespace Vortice.Direct2D1
{
    public partial class ID2D1RenderTarget
    {
        public SizeF Dpi
        {
            get
            {
                GetDpi(out var dpiX, out var dpiY);
                return new SizeF(dpiX, dpiY);
            }
            set
            {
                SetDpi(value.Width, value.Height);
            }
        }

        public void Clear(Color clearColor)
        {
            Clear(new Mathematics.Color4(clearColor));
        }

        public ID2D1SolidColorBrush CreateSolidColorBrush(Color color, BrushProperties? brushProperties = null)
        {
            return CreateSolidColorBrush(new Mathematics.Color4(color), brushProperties);
        }

        public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops)
        {
            return CreateGradientStopCollection_(gradientStops, gradientStops.Length, Gamma.StandardRgb, ExtendMode.Clamp);
        }

        public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops, Gamma colorInterpolationGamma, ExtendMode extendMode)
        {
            return CreateGradientStopCollection_(gradientStops, gradientStops.Length, colorInterpolationGamma, extendMode);
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

        public ID2D1Bitmap CreateSharedBitmap(ID2D1Bitmap bitmap, BitmapProperties? bitmapProperties)
        {
            return CreateSharedBitmap(typeof(ID2D1Bitmap).GUID, bitmap.NativePointer, bitmapProperties);
        }

        public ID2D1Bitmap CreateSharedBitmap(IDXGISurface surface, BitmapProperties? bitmapProperties)
        {
            return CreateSharedBitmap(typeof(IDXGISurface).GUID, surface.NativePointer, bitmapProperties);
        }

        public ID2D1Bitmap CreateSharedBitmap(IWICBitmapLock bitmapLock, BitmapProperties? bitmapProperties)
        {
            return CreateSharedBitmap(typeof(IWICBitmapLock).GUID, bitmapLock.NativePointer, bitmapProperties);
        }

        public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget(CompatibleRenderTargetOptions options)
        {
            return CreateCompatibleRenderTarget(null, null, null, options);
        }

        public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget(SizeF desiredSize, CompatibleRenderTargetOptions options)
        {
            return CreateCompatibleRenderTarget(desiredSize, null, null, options);
        }

        public ID2D1BitmapRenderTarget CreateCompatibleRenderTarget(DCommon.PixelFormat desiredFormat, CompatibleRenderTargetOptions options)
        {
            return CreateCompatibleRenderTarget(null, null, desiredFormat, options);
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
        /// <param name="interpolationMode">The interpolation mode to use if the bitmap is scaled or rotated by the drawing operation. The default value is <see cref="F:SharpDX.Direct2D1.BitmapInterpolationMode.Linear" />.  </param>
        /// <param name="sourceRectangle">The size and position, in device-independent pixels in the bitmap's coordinate space, of the area within the bitmap to be drawn; NULL to draw the entire bitmap.  </param>
        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, BitmapInterpolationMode interpolationMode, RectangleF sourceRectangle)
        {
            RawRectF rawRect = sourceRectangle;
            DrawBitmap(bitmap, null, opacity, interpolationMode, rawRect);
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
        public void DrawLine(PointF point0, PointF point1, ID2D1Brush brush)
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
        public void DrawLine(in PointF point0, in PointF point1, ID2D1Brush brush, float strokeWidth)
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
        public void DrawRectangle(in RectangleF rectangle, ID2D1Brush brush, float strokeWidth)
        {
            RawRectF rect = rectangle;
            DrawRectangle(rect, brush, strokeWidth, null);
        }

        /// <summary>	
        /// Draws the outline of a rectangle that has the specified dimensions. 
        /// </summary>
        /// <param name="rectangle">The dimensions of the rectangle to draw, in device-independent pixels. </param>
        /// <param name="brush">The <see cref="ID2D1Brush"/> used to paint the rectangle's stroke. </param>
        public void DrawRectangle(in RectangleF rectangle, ID2D1Brush brush)
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

        public void FillRectangle(in RectangleF rectangle, ID2D1Brush brush)
        {
            RawRectF rect = rectangle;
            FillRectangle(rect, brush);
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
        public void DrawText(string text, IDWriteTextFormat textFormat, RectangleF layoutRect, ID2D1Brush defaultForegroundBrush)
        {
            RawRectF rawLayoutRect = layoutRect;
            DrawText(text, text.Length, textFormat, rawLayoutRect, defaultForegroundBrush, DrawTextOptions.None, MeasuringMode.Natural);
        }

        /// <summary>	
        /// Draws the specified text using the format information provided by an <see cref="T:SharpDX.DirectWrite.TextFormat" /> object. 	
        /// </summary>
        /// <remarks>
        /// To create an <see cref="IDWriteTextFormat"/> object, create an <see cref="IDWriteFactory"/> and call its CreateTextFormat method.
        /// </remarks>
        /// <param name="text">A reference to an array of Unicode characters to draw.</param>
        /// <param name="textFormat">An object that describes formatting details of the text to draw, such as the font, the font size, and flow direction.   </param>
        /// <param name="layoutRect">The size and position of the area in which the text is drawn.  </param>
        /// <param name="defaultForegroundBrush">The brush used to paint the text. </param>
        /// <param name="options">A value that indicates whether the text should be snapped to pixel boundaries and whether the text should be clipped to the layout rectangle. The default value is <see cref="F:SharpDX.Direct2D1.DrawTextOptions.None" />, which indicates that text should be snapped to pixel boundaries and it should not be clipped to the layout rectangle. </param>
        public void DrawText(string text, IDWriteTextFormat textFormat, RectangleF layoutRect, ID2D1Brush defaultForegroundBrush, DrawTextOptions options)
        {
            RawRectF rawLayoutRect = layoutRect;
            DrawText(text, text.Length, textFormat, rawLayoutRect, defaultForegroundBrush, options, MeasuringMode.Natural);
        }

        /// <summary>	
        /// Draws the specified text using the format information provided by an <see cref="T:SharpDX.DirectWrite.TextFormat" /> object. 	
        /// </summary>
        /// <remarks>
        /// To create an <see cref="IDWriteTextFormat"/> object, create an <see cref="IDWriteFactory"/> and call its CreateTextFormat method.
        /// </remarks>
        /// <param name="text">A reference to an array of Unicode characters to draw.  </param>
        /// <param name="textFormat">An object that describes formatting details of the text to draw, such as the font, the font size, and flow direction.   </param>
        /// <param name="layoutRect">The size and position of the area in which the text is drawn.  </param>
        /// <param name="defaultForegroundBrush">The brush used to paint the text. </param>
        /// <param name="options">A value that indicates whether the text should be snapped to pixel boundaries and whether the text should be clipped to the layout rectangle. The default value is <see cref="F:SharpDX.Direct2D1.DrawTextOptions.None" />, which indicates that text should be snapped to pixel boundaries and it should not be clipped to the layout rectangle. </param>
        /// <param name="measuringMode">A value that indicates how glyph metrics are used to measure text when it is formatted.  The default value is DWRITE_MEASURING_MODE_NATURAL.  </param>
        public void DrawText(string text, IDWriteTextFormat textFormat, RectangleF layoutRect, ID2D1Brush defaultForegroundBrush, DrawTextOptions options, MeasuringMode measuringMode)
        {
            RawRectF rawLayoutRect = layoutRect;
            DrawText(text, text.Length, textFormat, rawLayoutRect, defaultForegroundBrush, options, measuringMode);
        }

        /// <summary>	
        /// Draws the formatted text described by the specified <see cref="IDWriteTextLayout"/> object.
        /// </summary>
        /// <param name="origin">The point, described in device-independent pixels, at which the upper-left corner of the text described by textLayout is drawn. </param>
        /// <param name="textLayout">The formatted text to draw. Any drawing effects that do not inherit from <see cref="ID2D1Resource" /> are ignored. If there are drawing effects that inherit from <see cref="ID2D1Resource"/> that are not brushes, this method fails and the render target is put in an error state.  </param>
        /// <param name="defaultForegroundBrush">The <see cref="ID2D1Brush"/> used to paint any text in textLayout that does not already have a brush associated with it as a drawing effect (specified by the <see cref="IDWriteTextLayout.SetDrawingEffect(IUnknown, TextRange)"/> method).</param>
        public void DrawTextLayout(PointF origin, IDWriteTextLayout textLayout, ID2D1Brush defaultForegroundBrush)
        {
            DrawTextLayout(origin, textLayout, defaultForegroundBrush, DrawTextOptions.None);
        }

        public Result EndDraw()
        {
            return EndDraw(out _, out _);
        }
    }
}
