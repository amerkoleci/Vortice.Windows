// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial struct RenderTargetBlendDescription1 : IEquatable<RenderTargetBlendDescription1>
{
    public static bool operator ==(in RenderTargetBlendDescription1 left, in RenderTargetBlendDescription1 right)
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

    public static bool operator !=(in RenderTargetBlendDescription1 left, in RenderTargetBlendDescription1 right)
        => !(left == right);

    public override bool Equals(object? obj) => (obj is RenderTargetBlendDescription1 other) && Equals(other);

    public bool Equals(RenderTargetBlendDescription1 other) => this == other;

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
