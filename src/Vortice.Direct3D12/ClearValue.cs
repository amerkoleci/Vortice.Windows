// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;
using Vortice.Mathematics;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a value used to optimize clear operations for a particular resource.
/// </summary>
public partial struct ClearValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilValue"/> struct.
    /// </summary>
    /// <param name="format">
    /// The format of the commonly cleared color follows the same validation rules as a view/ descriptor creation. 
    /// In general, the format of the clear color can be any format in the same typeless group that the resource format belongs to.
    /// </param>
    /// <param name="color">Specifies a RGBA clear color.</param>
    public ClearValue(Format format, in Color4 color) : this()
    {
        Format = format;
        Color = color;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilValue"/> struct.
    /// </summary>
    /// <param name="format">
    /// The format of the commonly cleared color follows the same validation rules as a view/ descriptor creation. 
    /// In general, the format of the clear color can be any format in the same typeless group that the resource format belongs to.
    /// </param>
    /// <param name="color">Specifies a RGBA clear color.</param>
    public ClearValue(Format format, System.Drawing.Color color) : this()
    {
        Format = format;
        Color = new Color4(color);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilValue"/> struct.
    /// </summary>
    /// <param name="format">
    /// The format of the commonly cleared color follows the same validation rules as a view/ descriptor creation. 
    /// In general, the format of the clear color can be any format in the same typeless group that the resource format belongs to.
    /// </param>
    /// <param name="depthStencil">Specifies depth and stencil clear value.</param>
    public ClearValue(Format format, in DepthStencilValue depthStencil) : this()
    {
        Format = format;
        DepthStencil = depthStencil;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilValue"/> struct.
    /// </summary>
    /// <param name="format">
    /// The format of the commonly cleared color follows the same validation rules as a view/ descriptor creation. 
    /// In general, the format of the clear color can be any format in the same typeless group that the resource format belongs to.
    /// </param>
    /// <param name="depth">Specifies the depth value.</param>
    /// <param name="stencil">Specifies the stencil value.</param>
    public ClearValue(Format format, float depth, byte stencil = 0) : this()
    {
        Format = format;
        DepthStencil = new DepthStencilValue(depth, stencil);
    }
}
