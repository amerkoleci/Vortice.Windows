// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;

namespace Vortice.Direct2D1
{
    /// <summary>
    /// Contains the center point, x-radius, and y-radius of an ellipse.
    /// </summary>
    public partial struct Ellipse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> struct.
        /// </summary>
        /// <param name="point">The center point of the ellipse.</param>
        /// <param name="radiusX">The X-radius of the ellipse.</param>
        /// <param name="radiusY">The Y-radius of the ellipse.</param>
        public Ellipse(PointF point, float radiusX, float radiusY)
        {
            Point = point;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }
    }
}
