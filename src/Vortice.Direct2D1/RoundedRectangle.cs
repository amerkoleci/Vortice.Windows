// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

/// <summary>
/// Contains the dimensions and corner radii of a rounded rectangle.
/// </summary>
public partial struct RoundedRectangle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoundedRectangle"/> struct.
    /// </summary>
    /// <param name="rect">The coordinates of the rectangle.</param>
    /// <param name="radiusX">The x-radius for the quarter ellipse that is drawn to replace every corner of the rectangle.</param>
    /// <param name="radiusY">The y-radius for the quarter ellipse that is drawn to replace every corner of the rectangle.</param>
    public RoundedRectangle(Rect rect, float radiusX, float radiusY)
    {
        Rect = rect;
        RadiusX = radiusX;
        RadiusY = radiusY;
    }
}
