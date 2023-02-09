// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the blend state for a render target.
/// </summary>
public partial struct RenderTargetBlendDescription : IEquatable<RenderTargetBlendDescription>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RenderTargetBlendDescription"/> struct.
    /// </summary>
    /// <param name="blendEnable">Specifies whether to enable (or disable) blending.</param>
    /// <param name="logicOpEnable">Specifies whether to enable (or disable) a logical operation.</param>
    /// <param name="srcBlend">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the RGB value that the pixel shader outputs.
    /// The <see cref="BlendOperation"/> member defines how to combine the <see cref="SourceBlend"/> and <see cref="DestinationBlend"/> operations.
    /// </param>
    /// <param name="destBlend">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the current RGB value in the render target.
    /// The <see cref="BlendOperation"/> member defines how to combine the <see cref="SourceBlend"/> and <see cref="DestinationBlend"/> operations.
    /// </param>
    /// <param name="blendOp">
    /// A <see cref="BlendOperation"/> value that defines how to combine the <see cref="SourceBlend"/> and <see cref="DestinationBlend"/> operations.
    /// </param>
    /// <param name="srcBlendAlpha">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the alpha value that the pixel shader outputs.
    /// Blend options that end in _COLOR are not allowed.
    /// The <see cref="BlendOperationAlpha"/> member defines how to combine the <see cref="SourceBlendAlpha"/> and <see cref="DestinationBlendAlpha"/> operations.
    /// </param>
    /// <param name="destBlendAlpha">
    /// A <see cref="Blend"/> value that specifies the operation to perform on the current alpha value in the render target.
    /// Blend options that end in _COLOR are not allowed.
    /// The <see cref="BlendOperationAlpha"/> member defines how to combine the <see cref="SourceBlendAlpha"/> and <see cref="DestinationBlendAlpha"/> operations.
    /// </param>
    /// <param name="blendOpAlpha">
    /// A <see cref="BlendOperation"/> value that defines how to combine the <see cref="SourceBlendAlpha"/> and <see cref="DestinationBlendAlpha"/> operations.
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
        SourceBlend = srcBlend;
        DestinationBlend = destBlend;
        BlendOperation = blendOp;
        SourceBlendAlpha = srcBlendAlpha;
        DestinationBlendAlpha = destBlendAlpha;
        BlendOperationAlpha = blendOpAlpha;
        LogicOp = logicOp;
        RenderTargetWriteMask = renderTargetWriteMask;
    }

    public static bool operator ==(in RenderTargetBlendDescription left, in RenderTargetBlendDescription right)
    {
        return (left.BlendEnable == right.BlendEnable)
            && (left.LogicOpEnable == right.LogicOpEnable)
            && (left.SourceBlend == right.SourceBlend)
            && (left.DestinationBlend == right.DestinationBlend)
            && (left.BlendOperation == right.BlendOperation)
            && (left.SourceBlendAlpha == right.SourceBlendAlpha)
            && (left.DestinationBlendAlpha == right.DestinationBlendAlpha)
            && (left.BlendOperationAlpha == right.BlendOperationAlpha)
            && (left.LogicOp == right.LogicOp)
            && (left.RenderTargetWriteMask == right.RenderTargetWriteMask);
    }

    public static bool operator !=(in RenderTargetBlendDescription left, in RenderTargetBlendDescription right)
        => !(left == right);

    public override bool Equals(object? obj) => (obj is RenderTargetBlendDescription other) && Equals(other);

    public bool Equals(RenderTargetBlendDescription other) => this == other;

    public override int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(BlendEnable);
        hashCode.Add(LogicOpEnable);
        hashCode.Add(SourceBlend);
        hashCode.Add(DestinationBlend);
        hashCode.Add(BlendOperation);
        hashCode.Add(SourceBlendAlpha);
        hashCode.Add(DestinationBlendAlpha);
        hashCode.Add(BlendOperationAlpha);
        hashCode.Add(LogicOp);
        hashCode.Add(RenderTargetWriteMask);
        return hashCode.ToHashCode();
    }
}
