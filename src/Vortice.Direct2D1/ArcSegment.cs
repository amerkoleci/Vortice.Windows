// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1;

/// <summary>
/// Describes an elliptical arc between two points.
/// </summary>
public partial struct ArcSegment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArcSegment"/> struct.
    /// </summary>
    /// <param name="point">The end point of the arc.</param>
    /// <param name="size">The x-radius and y-radius of the arc.</param>
    /// <param name="rotationAngle">A value that specifies how many degrees in the clockwise direction the ellipse is rotated relative to the current coordinate system.</param>
    /// <param name="sweepDirection">A value that specifies whether the arc sweep is clockwise or counterclockwise.</param>
    /// <param name="arcSize">A value that specifies whether the given arc is larger than 180 degrees.</param>
    public ArcSegment(Point point, Size size, float rotationAngle, SweepDirection sweepDirection, ArcSize arcSize)
    {
        Point = point;
        Size = size;
        RotationAngle = rotationAngle;
        SweepDirection = sweepDirection;
        ArcSize = arcSize;
    }
}
