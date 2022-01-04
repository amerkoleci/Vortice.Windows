// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

/// <summary>
/// Describes depth-stencil state.
/// </summary>
public partial struct DepthStencilDescription
{
    /// <summary>
    /// A built-in description with settings for not using a depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription None = new(false, DepthWriteMask.Zero);

    /// <summary>
    /// A built-in description with default settings for using a depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription Default = new(true, DepthWriteMask.All);

    /// <summary>
    /// A built-in description with settings for enabling a read-only depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription DepthRead = new(true, DepthWriteMask.Zero);

    /// <summary>
    /// A built-in description with settings for using a reverse depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription DepthReverseZ = new(true, DepthWriteMask.All, ComparisonFunction.GreaterEqual);

    /// <summary>
    /// A built-in description with settings for enabling a read-only reverse depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription DepthReadReverseZ = new(true, DepthWriteMask.Zero, ComparisonFunction.GreaterEqual);

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription"/> struct.
    /// </summary>
    /// <param name="depthEnable">Enable depth testing.</param>
    /// <param name="depthWriteMask">Identify a portion of the depth-stencil buffer that can be modified by depth data.</param>
    /// <param name="depthFunc">A function that compares depth data against existing depth data. </param>
    public DepthStencilDescription(bool depthEnable, DepthWriteMask depthWriteMask, ComparisonFunction depthFunc = ComparisonFunction.LessEqual)
    {
        DepthEnable = depthEnable;
        DepthWriteMask = depthWriteMask;
        DepthFunc = depthFunc;
        StencilEnable = false;
        StencilReadMask = DefaultStencilReadMask;
        StencilWriteMask = DefaultStencilWriteMask;
        FrontFace = DepthStencilOperationDescription.Default;
        BackFace = DepthStencilOperationDescription.Default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription"/> struct.
    /// </summary>
    /// <param name="depthEnable">Specifies whether to enable depth testing. Set this member to <b>true</b> to enable depth testing.</param>
    /// <param name="depthWriteEnable">Specifies a value that identifies a portion of the depth-stencil buffer that can be modified by depth data.</param>
    /// <param name="depthFunc">A <see cref="ComparisonFunction"/> value that identifies a function that compares depth data against existing depth data.</param>
    /// <param name="stencilEnable">Specifies whether to enable stencil testing. Set this member to <b>true</b> to enable stencil testing.</param>
    /// <param name="stencilReadMask">Identify a portion of the depth-stencil buffer for reading stencil data.</param>
    /// <param name="stencilWriteMask">Identify a portion of the depth-stencil buffer for writing stencil data.</param>
    /// <param name="frontStencilFailOp"></param>
    /// <param name="frontStencilDepthFailOp"></param>
    /// <param name="frontStencilPassOp"></param>
    /// <param name="frontStencilFunc"></param>
    /// <param name="backStencilFailOp"></param>
    /// <param name="backStencilDepthFailOp"></param>
    /// <param name="backStencilPassOp"></param>
    /// <param name="backStencilFunc"></param>
    public DepthStencilDescription(
        bool depthEnable,
        bool depthWriteEnable,
        ComparisonFunction depthFunc,
        bool stencilEnable,
        byte stencilReadMask,
        byte stencilWriteMask,
        StencilOperation frontStencilFailOp,
        StencilOperation frontStencilDepthFailOp,
        StencilOperation frontStencilPassOp,
        ComparisonFunction frontStencilFunc,
        StencilOperation backStencilFailOp,
        StencilOperation backStencilDepthFailOp,
        StencilOperation backStencilPassOp,
        ComparisonFunction backStencilFunc)
    {
        DepthEnable = depthEnable;
        DepthWriteMask = depthWriteEnable ? DepthWriteMask.All : DepthWriteMask.Zero;
        DepthFunc = depthFunc;
        StencilEnable = stencilEnable;
        StencilReadMask = stencilReadMask;
        StencilWriteMask = stencilWriteMask;
        FrontFace.StencilFailOp = frontStencilFailOp;
        FrontFace.StencilDepthFailOp = frontStencilDepthFailOp;
        FrontFace.StencilPassOp = frontStencilPassOp;
        FrontFace.StencilFunc = frontStencilFunc;
        BackFace.StencilFailOp = backStencilFailOp;
        BackFace.StencilDepthFailOp = backStencilDepthFailOp;
        BackFace.StencilPassOp = backStencilPassOp;
        BackFace.StencilFunc = backStencilFunc;
    }
}
