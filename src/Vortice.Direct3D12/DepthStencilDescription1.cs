// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes depth-stencil state.
/// </summary>
public partial struct DepthStencilDescription1
{
    /// <summary>
    /// A built-in description with settings for not using a depth stencil buffer.
    /// </summary>
    public static DepthStencilDescription1 None => new(false, DepthWriteMask.Zero, ComparisonFunction.LessEqual);

    /// <summary>
    /// A built-in description with default settings for using a depth stencil buffer.
    /// </summary>
    public static DepthStencilDescription1 Default => new(true, DepthWriteMask.All, ComparisonFunction.LessEqual);

    /// <summary>
    /// A built-in description with settings for enabling a read-only depth stencil buffer.
    /// </summary>
    public static DepthStencilDescription1 Read => new(true, DepthWriteMask.Zero, ComparisonFunction.LessEqual);

    /// <summary>
    /// A built-in description with default settings for using a reverse depth stencil buffer.
    /// </summary>
    public static DepthStencilDescription1 ReverseZ => new(true, DepthWriteMask.All, ComparisonFunction.GreaterEqual);

    /// <summary>
    /// A built-in description with default settings for using a reverse read-only depth stencil buffer.
    /// </summary>
    public static DepthStencilDescription1 ReadReverseZ => new(true, DepthWriteMask.Zero, ComparisonFunction.GreaterEqual);

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription1"/> struct.
    /// </summary>
    /// <param name="depthEnable"></param>
    /// <param name="depthWriteMask"></param>
    /// <param name="depthFunc"></param>
    public DepthStencilDescription1(bool depthEnable, DepthWriteMask depthWriteMask, ComparisonFunction depthFunc)
    {
        DepthEnable = depthEnable;
        DepthWriteMask = depthWriteMask;
        DepthFunc = depthFunc;
        StencilEnable = false;
        StencilReadMask = D3D12.DefaultStencilReadMask;
        StencilWriteMask = D3D12.DefaultStencilWriteMask;
        FrontFace = DepthStencilOperationDescription.Default;
        BackFace = DepthStencilOperationDescription.Default;
        DepthBoundsTestEnable = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription1"/> struct.
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
    /// <param name="depthBoundsTestEnable"></param>
    public DepthStencilDescription1(
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
        ComparisonFunction backStencilFunc,
        bool depthBoundsTestEnable)
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
        DepthBoundsTestEnable = depthBoundsTestEnable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilDescription1"/> struct.
    /// </summary>
    public DepthStencilDescription1(in DepthStencilDescription other)
    {
        DepthEnable = other.DepthEnable;
        DepthWriteMask = other.DepthWriteMask;
        DepthFunc = other.DepthFunc;
        StencilEnable = other.StencilEnable;
        StencilReadMask = other.StencilReadMask;
        StencilWriteMask = other.StencilWriteMask;
        FrontFace.StencilFailOp = other.FrontFace.StencilFailOp;
        FrontFace.StencilDepthFailOp = other.FrontFace.StencilDepthFailOp;
        FrontFace.StencilPassOp = other.FrontFace.StencilPassOp;
        FrontFace.StencilFunc = other.FrontFace.StencilFunc;
        BackFace.StencilFailOp = other.BackFace.StencilFailOp;
        BackFace.StencilDepthFailOp = other.BackFace.StencilDepthFailOp;
        BackFace.StencilPassOp = other.BackFace.StencilPassOp;
        BackFace.StencilFunc = other.BackFace.StencilFunc;
        DepthBoundsTestEnable = false;
    }
}
