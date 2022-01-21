// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using Vortice.Mathematics;
using Vortice.DirectWrite;

namespace Vortice.Direct2D1;

public unsafe partial class ID2D1Factory
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
        return CreateGeometryGroup(fillMode, geometries, geometries.Length);
    }

    public ID2D1RectangleGeometry CreateRectangleGeometry(in Rectangle rectangle)
    {
        RawRectF rawRect = rectangle;
        return CreateRectangleGeometry(new IntPtr(&rawRect));
    }

    public unsafe ID2D1RectangleGeometry CreateRectangleGeometry(RawRectF rectangle)
    {
        return CreateRectangleGeometry(new IntPtr(&rectangle));
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
            GetDesktopDpi(out float dpiX, out float dpiY);
            return new Vector2(dpiX, dpiY);
        }
    }
}
