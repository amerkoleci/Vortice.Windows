// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12.Video;

/// <summary>
/// Specifies alpha blending parameters for video processing
/// </summary>
public partial struct VideoProcessAlphaBlending
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VideoProcessAlphaBlending"/> struct.
    /// </summary>
    /// <param name="enable">A boolean value specifying whether alpha blending is enabled.</param>
    /// <param name="alpha">The planar alpha value. The value can range from 0.0 (transparent) to 1.0 (opaque). If Enable is false, this parameter is ignored.</param>
    /// <remarks>
    /// For each pixel, the destination color value is computed as follows:
    ///     Cd = Cs * (As * Ap * Ae) + Cd * (1.0 - As * Ap * Ae)
    /// where:
    ///     Cd = The color value of the destination pixel
    ///     Cs = The color value of the source pixel
    ///     As = The per-pixel source alpha
    ///     Ap = The planar alpha value
    ///     Ae = The palette-entry alpha value, or 1.0 (palette-entry alpha values apply only to palettized color formats)
    /// </remarks>
    public VideoProcessAlphaBlending(bool enable, float alpha)
    {
        Enable = enable;
        Alpha = alpha;
    }
}
