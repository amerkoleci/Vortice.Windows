// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

public partial struct BezierSegment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BezierSegment"/> struct.
    /// </summary>
    /// <param name="point1">The first control point for the Bezier segment.</param>
    /// <param name="point2">The second control point for the Bezier segment.</param>
    /// <param name="point3">The end point for the Bezier segment.</param>
    public BezierSegment(in Vector2 point1, in Vector2 point2, in Vector2 point3)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
}
