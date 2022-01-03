// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;

namespace Vortice.Direct2D1;

/// <summary>
/// Represents a cubic bezier segment drawn between two points.
/// </summary>
public partial struct BezierSegment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegment"/> struct.
    /// </summary>
    /// <param name="point1">The first control point for the Bezier segment.</param>
    /// <param name="point2">The second control point for the Bezier segment.</param>
    /// <param name="point3">The end point for the Bezier segment.</param>
    public BezierSegment(PointF point1, PointF point2, PointF point3)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
}
