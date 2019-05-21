// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;
using Vortice.DirectX.DirectWrite;
using Vortice.DirectX.WIC;
using SharpGen.Runtime;
using Vortice.Mathematics;
using Vortice.DirectX.DXGI;
using System.Drawing;

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

        public void DrawText(string text, IDWriteTextFormat textFormat, Rectangle layoutRect, ID2D1Brush defaultFillBrush, DrawTextOptions options = DrawTextOptions.None, MeasuringMode measuringMode = MeasuringMode.Natural)
        {
            Guard.NotNullOrEmpty(text, nameof(text));

            DrawText(text, text.Length, textFormat, layoutRect, defaultFillBrush, options, measuringMode);
        }

        public Result EndDraw()
        {
            return EndDraw( out var tag1, out var tag2);
        }
    }
}
