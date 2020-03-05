// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.DXGI;
using Vortice.Mathematics;

namespace Vortice.Direct2D1
{
    /// <summary>
    /// Describes image brush features.
    /// </summary>
    public partial struct ImageBrushProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageBrushProperties"/> struct.
        /// </summary>
        /// <param name="sourceRectangle">The source rectangle in the image space from which the image will be tiled or interpolated.</param>
        /// <param name="extendModeX">The extend mode in the image x-axis.</param>
        /// <param name="extendModeY">The extend mode in the image y-axis.</param>
        /// <param name="interpolationMode">The interpolation mode to use when scaling the image brush.</param>
        public ImageBrushProperties(RectF sourceRectangle, ExtendMode extendModeX, ExtendMode extendModeY, InterpolationMode interpolationMode)
        {
            SourceRectangle = sourceRectangle;
            ExtendModeX = extendModeX;
            ExtendModeY = extendModeY;
            InterpolationMode = interpolationMode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageBrushProperties"/> struct.
        /// </summary>
        /// <param name="sourceRectangle">The source rectangle in the image space from which the image will be tiled or interpolated.</param>
        /// <param name="extendModeX">The extend mode in the image x-axis.</param>
        /// <param name="extendModeY">The extend mode in the image y-axis.</param>
        /// <param name="interpolationMode">The interpolation mode to use when scaling the image brush.</param>
        public ImageBrushProperties(System.Drawing.RectangleF sourceRectangle, ExtendMode extendModeX, ExtendMode extendModeY, InterpolationMode interpolationMode)
        {
            SourceRectangle = sourceRectangle;
            ExtendModeX = extendModeX;
            ExtendModeY = extendModeY;
            InterpolationMode = interpolationMode;
        }
    }
}
