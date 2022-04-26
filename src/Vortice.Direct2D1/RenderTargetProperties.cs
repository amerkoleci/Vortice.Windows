// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DCommon;

namespace Vortice.Direct2D1;

public partial struct RenderTargetProperties
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RenderTargetProperties"/> struct.
    /// </summary>
    /// <param name="pixelFormat">The pixel format and alpha mode of the render target.</param>
    public RenderTargetProperties(PixelFormat pixelFormat)
        : this(RenderTargetType.Default, pixelFormat, 96.0f, 96.0f, RenderTargetUsage.None, FeatureLevel.Default)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RenderTargetProperties"/> struct.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pixelFormat"></param>
    /// <param name="dpiX"></param>
    /// <param name="dpiY"></param>
    /// <param name="usage"></param>
    /// <param name="minLevel"></param>  
    public RenderTargetProperties(RenderTargetType type, PixelFormat pixelFormat, float dpiX, float dpiY, RenderTargetUsage usage, FeatureLevel minLevel)
    {
        Type = type;
        PixelFormat = pixelFormat;
        DpiX = dpiX;
        DpiY = dpiY;
        Usage = usage;
        MinLevel = minLevel;
    }
}
