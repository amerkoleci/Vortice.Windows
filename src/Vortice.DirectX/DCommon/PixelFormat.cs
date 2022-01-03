// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.DCommon;

public partial struct PixelFormat
{
    /// <summary>
    /// An unkown <see cref="PixelFormat"/> with <see cref="Format"/> to <see cref="Format.Unknown"/> and <see cref="AlphaMode"/> to <see cref="AlphaMode.Unknown"/>.
    /// </summary>
    public static readonly PixelFormat Unknown = new PixelFormat(Format.Unknown, AlphaMode.Unknown);

    /// <summary>
    /// Initializes a new instance of the <see cref="PixelFormat"/> struct.
    /// </summary>
    /// <param name="format">The <see cref="DXGI.Format"/> to use.</param>
    /// <param name="alphaMode">A value that specifies whether the alpha channel is using pre-multiplied alpha, straight alpha, whether it should be ignored and considered opaque, or whether it is unknown.</param>
    public PixelFormat(Format format, AlphaMode alphaMode)
    {
        Format = format;
        AlphaMode = alphaMode;
    }
}
