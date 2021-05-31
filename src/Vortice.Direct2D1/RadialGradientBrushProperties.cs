// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Drawing;

namespace Vortice.Direct2D1
{
    /// <summary>
    /// Contains the gradient origin offset and the size and position of the gradient ellipse for an <see cref="ID2D1RadialGradientBrush"/>.
    /// </summary>
    public partial struct RadialGradientBrushProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadialGradientBrushProperties"/> struct.
        /// </summary>
        /// <param name="center">In the brush's coordinate space, the center of the gradient ellipse.</param>
        /// <param name="gradientOriginOffset">In the brush's coordinate space, the offset of the gradient origin relative to the gradient ellipse's center.</param>
        /// <param name="radiusX">In the brush's coordinate space, the x-radius of the gradient ellipse.</param>
        /// <param name="radiusY">In the brush's coordinate space, the y-radius of the gradient ellipse.</param>
        public RadialGradientBrushProperties(in PointF center, in PointF gradientOriginOffset, float radiusX, float radiusY)
        {
            Center = center;
            GradientOriginOffset = gradientOriginOffset;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }
    }
}
