// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1
{
    /// <summary>
    /// Describes the opacity and transformation of a brush.
    /// </summary>
    public partial struct BrushProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrushProperties"/> struct.
        /// </summary>
        /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies the degree of opacity of the brush.</param>
        public BrushProperties(float opacity)
            : this(opacity, Matrix3x2.Identity)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BrushProperties"/> struct.
        /// </summary>
        /// <param name="opacity">A value between 0.0f and 1.0f, inclusive, that specifies the degree of opacity of the brush.</param>
        /// <param name="transform">The transformation that is applied to the brush.</param>
        public BrushProperties(float opacity, Matrix3x2 transform)
        {
            Opacity = opacity;
            Transform = transform;
        }
    }
}
