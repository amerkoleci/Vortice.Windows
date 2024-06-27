// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1;

/// <summary>
/// Describes the extend modes and the interpolation mode of an <see cref="ID2D1BitmapBrush"/>.
/// </summary>
public partial struct BitmapBrushProperties1
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BitmapBrushProperties1"/> struct.
    /// </summary>
    /// <param name="extendModeX">A value that describes how the brush horizontally tiles those areas that extend past its bitmap.</param>
    /// <param name="extendModeY">A value that describes how the brush vertically tiles those areas that extend past its bitmap.</param>
    /// <param name="interpolationMode">A value that specifies how the bitmap is interpolated when it is scaled or rotated.</param>
    public BitmapBrushProperties1(ExtendMode extendModeX, ExtendMode extendModeY, InterpolationMode interpolationMode)
    {
        ExtendModeX = extendModeX;
        ExtendModeY = extendModeY;
        InterpolationMode = interpolationMode;
    }
}
