// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using Vortice.DirectWrite;
using Vortice.Mathematics;

namespace Vortice.Direct2D1
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

        public ID2D1DrawingStateBlock CreateDrawingStateBlock(DrawingStateDescription drawingStateDescription, IDWriteRenderingParams? textRenderingParams)
        {
            return CreateDrawingStateBlock(drawingStateDescription, textRenderingParams);
        }

        public ID2D1GeometryGroup CreateGeometryGroup(FillMode fillMode, params ID2D1Geometry[] geometries)
        {
            CreateGeometryGroup(fillMode, geometries, geometries.Length, out ID2D1GeometryGroup geometryGroup);
            return geometryGroup;
        }

        public ID2D1RectangleGeometry CreateRectangleGeometry(RectangleF rectangle)
        {
            unsafe
            {
                RawRectF rawRect = rectangle;
                return CreateRectangleGeometry(new IntPtr(&rawRect));
            }
        }

        public ID2D1StrokeStyle CreateStrokeStyle(StrokeStyleProperties properties)
        {
            return CreateStrokeStyle(ref properties, null, 0);
        }

        public ID2D1StrokeStyle CreateStrokeStyle(StrokeStyleProperties properties, float[] dashes)
        {
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
