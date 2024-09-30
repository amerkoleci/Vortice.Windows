// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DCommon;

namespace Vortice.Direct2D1;

/// <summary>
/// Describes the pixel format and dpi of a <see cref="ID2D1Bitmap"/>.
/// </summary>
public partial struct BitmapProperties
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BitmapProperties"/> struct.
    /// </summary>
    /// <param name="pixelFormat">The pixel format.</param>
    public BitmapProperties(PixelFormat pixelFormat)
    {
        PixelFormat = pixelFormat;
        DpiX = 96.0f;
        DpiY = 96.0f;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BitmapProperties"/> struct.
    /// </summary>
    /// <param name="pixelFormat">The pixel format.</param>
    /// <param name="dpiX">The bitmap dpi in the x direction.</param>
    /// <param name="dpiY">The bitmap dpi in the y direction.</param>
    public BitmapProperties(PixelFormat pixelFormat, float dpiX, float dpiY)
    {
        PixelFormat = pixelFormat;
        DpiX = dpiX;
        DpiY = dpiY;
    }
}
