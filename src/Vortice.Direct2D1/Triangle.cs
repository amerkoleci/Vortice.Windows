// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1
{
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
        public Triangle(in Vector2 point1, in Vector2 point2, in Vector2 point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }
    }
}
