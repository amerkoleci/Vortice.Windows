// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial struct RenderTargetBlendDescription : IEquatable<RenderTargetBlendDescription>
{
    public static bool operator ==(in RenderTargetBlendDescription left, in RenderTargetBlendDescription right)
    {
        return (left.BlendEnable == right.BlendEnable)
            && (left.SourceBlend == right.SourceBlend)
            && (left.DestinationBlend == right.DestinationBlend)
            && (left.BlendOperation == right.BlendOperation)
            && (left.SourceBlendAlpha == right.SourceBlendAlpha)
            && (left.DestinationBlendAlpha == right.DestinationBlendAlpha)
            && (left.BlendOperationAlpha == right.BlendOperationAlpha)
            && (left.RenderTargetWriteMask == right.RenderTargetWriteMask);
    }

    public static bool operator !=(in RenderTargetBlendDescription left, in RenderTargetBlendDescription right)
        => !(left == right);

    public override bool Equals(object? obj) => (obj is RenderTargetBlendDescription other) && Equals(other);

    public bool Equals(RenderTargetBlendDescription other) => this == other;

    public override int GetHashCode()
    {
        return HashCode.Combine(BlendEnable, SourceBlend, DestinationBlend, BlendOperation, SourceBlendAlpha, DestinationBlendAlpha, BlendOperationAlpha, RenderTargetWriteMask);
    }
}
