// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes depth-stencil state.
/// </summary>
public partial struct DepthStencilDescription2
{
    /// <summary>
    /// A built-in description with settings for not using a depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription2 None = new(false, DepthWriteMask.Zero, ComparisonFunction.LessEqual);

    /// <summary>
    /// A built-in description with default settings for using a depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription2 Default = new(true, DepthWriteMask.All, ComparisonFunction.LessEqual);

    /// <summary>
    /// A built-in description with settings for enabling a read-only depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription2 Read = new(true, DepthWriteMask.Zero, ComparisonFunction.LessEqual);

    /// <summary>
    /// A built-in description with default settings for using a reverse depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription2 ReverseZ = new(true, DepthWriteMask.All, ComparisonFunction.GreaterEqual);

    /// <summary>
    /// A built-in description with default settings for using a reverse read-only depth stencil buffer.
    /// </summary>
    public static readonly DepthStencilDescription2 ReadReverseZ = new(true, DepthWriteMask.Zero, ComparisonFunction.GreaterEqual);

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription2"/> struct.
    /// </summary>
    /// <param name="depthEnable"></param>
    /// <param name="depthWriteMask"></param>
    /// <param name="depthFunc"></param>
    public DepthStencilDescription2(bool depthEnable, DepthWriteMask depthWriteMask, ComparisonFunction depthFunc)
    {
        DepthEnable = depthEnable;
        DepthWriteMask = depthWriteMask;
        DepthFunc = depthFunc;
        StencilEnable = false;
        FrontFace = DepthStencilOperationDescription1.Default;
        BackFace = DepthStencilOperationDescription1.Default;
        DepthBoundsTestEnable = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription1"/> struct.
    /// </summary>
    /// <param name="depthEnable">Specifies whether to enable depth testing. Set this member to <b>true</b> to enable depth testing.</param>
    /// <param name="depthWriteEnable">Specifies a value that identifies a portion of the depth-stencil buffer that can be modified by depth data.</param>
    /// <param name="depthFunc">A <see cref="ComparisonFunction"/> value that identifies a function that compares depth data against existing depth data.</param>
    /// <param name="stencilEnable">Specifies whether to enable stencil testing. Set this member to <b>true</b> to enable stencil testing.</param>
    /// <param name="frontStencilFailOp"></param>
    /// <param name="frontStencilDepthFailOp"></param>
    /// <param name="frontStencilPassOp"></param>
    /// <param name="frontStencilFunc"></param>
    /// <param name="frontStencilReadMask"></param>
    /// <param name="frontStencilWriteMask"></param>
    /// <param name="backStencilFailOp"></param>
    /// <param name="backStencilDepthFailOp"></param>
    /// <param name="backStencilPassOp"></param>
    /// <param name="backStencilFunc"></param>
    /// <param name="backStencilReadMask"></param>
    /// <param name="backStencilWriteMask"></param>
    /// <param name="depthBoundsTestEnable"></param>
    public DepthStencilDescription2(
        bool depthEnable,
        bool depthWriteEnable,
        ComparisonFunction depthFunc,
        bool stencilEnable,
        StencilOperation frontStencilFailOp,
        StencilOperation frontStencilDepthFailOp,
        StencilOperation frontStencilPassOp,
        ComparisonFunction frontStencilFunc,
        byte frontStencilReadMask,
        byte frontStencilWriteMask,
        StencilOperation backStencilFailOp,
        StencilOperation backStencilDepthFailOp,
        StencilOperation backStencilPassOp,
        ComparisonFunction backStencilFunc,
        byte backStencilReadMask,
        byte backStencilWriteMask,
        bool depthBoundsTestEnable)
    {
        DepthEnable = depthEnable;
        DepthWriteMask = depthWriteEnable ? DepthWriteMask.All : DepthWriteMask.Zero;
        DepthFunc = depthFunc;
        StencilEnable = stencilEnable;
        FrontFace.StencilFailOp = frontStencilFailOp;
        FrontFace.StencilDepthFailOp = frontStencilDepthFailOp;
        FrontFace.StencilPassOp = frontStencilPassOp;
        FrontFace.StencilFunc = frontStencilFunc;
        FrontFace.StencilReadMask = frontStencilReadMask;
        FrontFace.StencilWriteMask = frontStencilWriteMask;
        BackFace.StencilFailOp = backStencilFailOp;
        BackFace.StencilDepthFailOp = backStencilDepthFailOp;
        BackFace.StencilPassOp = backStencilPassOp;
        BackFace.StencilFunc = backStencilFunc;
        BackFace.StencilReadMask = backStencilReadMask;
        BackFace.StencilWriteMask = backStencilWriteMask;
        DepthBoundsTestEnable = depthBoundsTestEnable;
    }
}
