// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Numerics;
using Vortice.DirectX.DirectWrite;

namespace Vortice.DirectX.Direct2D
{
    public partial class ID2D1Factory
    {
        public ID2D1DrawingStateBlock CreateDrawingStateBlock()
        {
            return CreateDrawingStateBlock(null, null);
        }

        public ID2D1DrawingStateBlock CreateDrawingStateBlock(DrawingStateDescription drawingStateDescription)
        {
            return CreateDrawingStateBlock(drawingStateDescription, null);
        }

        public ID2D1DrawingStateBlock CreateDrawingStateBlock(DrawingStateDescription drawingStateDescription, IDWriteRenderingParams textRenderingParams)
        {
            Guard.NotNull(textRenderingParams, nameof(textRenderingParams));
            return CreateDrawingStateBlock(drawingStateDescription, textRenderingParams);
        }

        public ID2D1GeometryGroup CreateGeometryGroup(FillMode fillMode, params ID2D1Geometry[] geometries)
        {
            CreateGeometryGroup(fillMode, geometries, geometries.Length, out ID2D1GeometryGroup geometryGroup);
            return geometryGroup;
        }

        public ID2D1RectangleGeometry CreateRectangleGeometry(Rectangle rectangle)
        {
            unsafe
            {
                return CreateRectangleGeometry(new IntPtr(&rectangle));
            }
        }

        public ID2D1StrokeStyle CreateStrokeStyle(StrokeStyleProperties properties)
        {
            return CreateStrokeStyle(ref properties, null, 0);
        }

        public ID2D1StrokeStyle CreateStrokeStyle(StrokeStyleProperties properties, float[] dashes)
        {
            Guard.NotNullOrEmpty(dashes, nameof(dashes));

            return CreateStrokeStyle(ref properties, dashes, dashes.Length);
        }

        public ID2D1TransformedGeometry CreateTransformedGeometry(ID2D1Geometry sourceGeometry, Matrix3x2 transform)
        {
            return CreateTransformedGeometry(sourceGeometry, ref transform);
        }

        public Vector2 DesktopDpi
        {
            get
            {
                GetDesktopDpi(out var dpiX, out var dpiY);
                return new Vector2(dpiX, dpiY);
            }
        }
    }
}
