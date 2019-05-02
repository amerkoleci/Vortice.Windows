// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace SharpDirect2D
{
    /// <summary>
    /// Contains the position and color of a gradient stop.
    /// </summary>
    public partial struct GradientStop
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradientStop"/> struct.
        /// </summary>
        /// <param name="position">A value that indicates the relative position of the gradient stop in the brush. This value must be in the [0.0f, 1.0f] range if the gradient stop is to be seen explicitly.</param>
        /// <param name="color">The color of the gradient stop.</param>
        public GradientStop(float position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
