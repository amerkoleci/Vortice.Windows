// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the blend state for a render target.
/// </summary>
public partial struct RenderTargetBlendDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RenderTargetBlendDescription"/> struct.
    /// </summary>
    /// <param name="blendEnable">Specifies whether to enable (or disable) blending.</param>
    /// <param name="logicOpEnable">Specifies whether to enable (or disable) a logical operation.</param>
    /// <param name="srcBlend">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the RGB value that the pixel shader outputs.
    /// The <see cref="BlendOp"/> member defines how to combine the <see cref="SrcBlend"/> and <see cref="DestBlend"/> operations.
    /// </param>
    /// <param name="destBlend">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the current RGB value in the render target.
    /// The <see cref="BlendOp"/> member defines how to combine the <see cref="SrcBlend"/> and <see cref="DestBlend"/> operations.
    /// </param>
    /// <param name="blendOp">
    /// A <see cref="BlendOperation"/> value that defines how to combine the <see cref="SrcBlend"/> and <see cref="DestBlend"/> operations.
    /// </param>
    /// <param name="srcBlendAlpha">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the alpha value that the pixel shader outputs.
    /// Blend options that end in _COLOR are not allowed.
    /// The <see cref="BlendOpAlpha"/> member defines how to combine the <see cref="SrcBlendAlpha"/> and <see cref="DestBlendAlpha"/> operations.
    /// </param>
    /// <param name="destBlendAlpha">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the current alpha value in the render target.
    /// Blend options that end in _COLOR are not allowed.
    /// The <see cref="BlendOpAlpha"/> member defines how to combine the <see cref="SrcBlendAlpha"/> and <see cref="DestBlendAlpha"/> operations.
    /// </param>
    /// <param name="blendOpAlpha">
    /// A <see cref="BlendOperation"/> value that defines how to combine the <see cref="SrcBlendAlpha"/> and <see cref="DestBlendAlpha"/> operations.
    /// </param>
    /// <param name="logicOp">A <see cref="LogicOp"/> value that specifies the logical operation to configure for the render target.</param>
    /// <param name="renderTargetWriteMask">
    /// A combination of <see cref="ColorWriteEnable"/> values that are combined by using a bitwise OR operation.
    /// The resulting value specifies a write mask.
    /// </param>
    public RenderTargetBlendDescription(
        bool blendEnable,
        bool logicOpEnable,
        Blend srcBlend, Blend destBlend, BlendOperation blendOp,
        Blend srcBlendAlpha, Blend destBlendAlpha, BlendOperation blendOpAlpha,
        LogicOp logicOp = LogicOp.Noop, ColorWriteEnable renderTargetWriteMask = ColorWriteEnable.All
    )
    {
        BlendEnable = blendEnable;
        LogicOpEnable = logicOpEnable;
        SrcBlend = srcBlend;
        DestBlend = destBlend;
        BlendOp = blendOp;
        SrcBlendAlpha = srcBlendAlpha;
        DestBlendAlpha = destBlendAlpha;
        BlendOpAlpha = blendOpAlpha;
        LogicOp = logicOp;
        RenderTargetWriteMask = renderTargetWriteMask;
    }
}
