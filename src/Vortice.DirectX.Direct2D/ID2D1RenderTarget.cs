// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;
using Vortice.DirectX.DirectWrite;
using Vortice.DirectX.WIC;
using SharpGen.Runtime;
using Vortice.DirectX.DXGI;
using System.Drawing;
using Vortice.Interop;

namespace Vortice.DirectX.Direct2D
{
    public partial class ID2D1RenderTarget
    {
        public Vector2 Dpi
        {
            get
            {
                GetDpi(out var dpiX, out var dpiY);
                return new Vector2(dpiX, dpiY);
            }
            set
            {
                SetDpi(value.X, value.Y);
            }
        }

        public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops)
        {
            Guard.NotNullOrEmpty(gradientStops, nameof(gradientStops));

            return CreateGradientStopCollection(gradientStops, gradientStops.Length, Gamma.StandardRgb, ExtendMode.Clamp);
        }

        public ID2D1GradientStopCollection CreateGradientStopCollection(GradientStop[] gradientStops, Gamma colorInterpolationGamma, ExtendMode extendMode)
        {
            Guard.NotNullOrEmpty(gradientStops, nameof(gradientStops));

            return CreateGradientStopCollection(gradientStops, gradientStops.Length, colorInterpolationGamma, extendMode);
        }

        public ID2D1LinearGradientBrush CreateLinearGradientBrush(LinearGradientBrushProperties linearGradientBrushProperties, ID2D1GradientStopCollection gradientStopCollection)
        {
            return CreateLinearGradientBrush(linearGradientBrushProperties, null, gradientStopCollection);
        }

        public ID2D1LinearGradientBrush CreateLinearGradientBrush(LinearGradientBrushProperties linearGradientBrushProperties, BrushProperties brushProperties, ID2D1GradientStopCollection gradientStopCollection)
        {
            return CreateLinearGradientBrush(linearGradientBrushProperties, brushProperties, gradientStopCollection);
        }

        public ID2D1RadialGradientBrush CreateRadialGradientBrush(RadialGradientBrushProperties radialGradientBrushProperties, ID2D1GradientStopCollection gradientStopCollection)
        {
            return CreateRadialGradientBrush(ref radialGradientBrushProperties, null, gradientStopCollection);
        }

        public ID2D1RadialGradientBrush CreateRadialGradientBrush(RadialGradientBrushProperties radialGradientBrushProperties, BrushProperties brushProperties, ID2D1GradientStopCollection gradientStopCollection)
        {
            return CreateRadialGradientBrush(ref radialGradientBrushProperties, brushProperties, gradientStopCollection);
        }

        public ID2D1Bitmap CreateSharedBitmap(ID2D1Bitmap bitmap, BitmapProperties? bitmapProperties)
        {
            Guard.NotNull(bitmap, nameof(bitmap));

            return CreateSharedBitmap(typeof(ID2D1Bitmap).GUID, bitmap.NativePointer, bitmapProperties);
        }

        public ID2D1Bitmap CreateSharedBitmap(IDXGISurface surface, BitmapProperties? bitmapProperties)
        {
            Guard.NotNull(surface, nameof(surface));

            return CreateSharedBitmap(typeof(IDXGISurface).GUID, surface.NativePointer, bitmapProperties);
        }

        public ID2D1Bitmap CreateSharedBitmap(IWICBitmapLock bitmapLock, BitmapProperties? bitmapProperties)
        {
            Guard.NotNull(bitmapLock, nameof(bitmapLock));

            return CreateSharedBitmap(typeof(IWICBitmapLock).GUID, bitmapLock.NativePointer, bitmapProperties);
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
        public void DrawBitmap(ID2D1Bitmap bitmap, float opacity, BitmapInterpolationMode interpolationMode, RawRectangleF sourceRectangle)
        {
            DrawBitmap(bitmap, null, opacity, interpolationMode, sourceRectangle);
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
        public void DrawText(string text, IDWriteTextFormat textFormat, RawRectangleF layoutRect, ID2D1Brush defaultForegroundBrush)
        {
            Guard.NotNullOrEmpty(text, nameof(text));

            DrawText(text, text.Length, textFormat, layoutRect, defaultForegroundBrush, DrawTextOptions.None, MeasuringMode.Natural);
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
        public void DrawText(string text, IDWriteTextFormat textFormat, RawRectangleF layoutRect, ID2D1Brush defaultForegroundBrush, DrawTextOptions options)
        {
            Guard.NotNullOrEmpty(text, nameof(text));

            DrawText(text, text.Length, textFormat, layoutRect, defaultForegroundBrush, options, MeasuringMode.Natural);
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
        public void DrawText(string text, IDWriteTextFormat textFormat, RawRectangleF layoutRect, ID2D1Brush defaultForegroundBrush, DrawTextOptions options, MeasuringMode measuringMode)
        {
            Guard.NotNullOrEmpty(text, nameof(text));

            DrawText(text, text.Length, textFormat, layoutRect, defaultForegroundBrush, options, measuringMode);
        }

        public Result EndDraw()
        {
            return EndDraw(out var tag1, out var tag2);
        }
    }
}
