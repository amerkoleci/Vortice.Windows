// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1;

/// <summary>
/// Contains the three vertices that describe a triangle.
/// </summary>
public partial struct Triangle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Triangle"/> struct.
    /// </summary>
    /// <param name="point1">The first vertex of a triangle.</param>
    /// <param name="point2">The second vertex of a triangle.</param>
    /// <param name="point3">The third vertex of a triangle.</param>
    public Triangle(in Point point1, in Point point2, in Point point3)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
}
