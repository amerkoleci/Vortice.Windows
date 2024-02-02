// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial struct DepthStencilOperationDescription : IEquatable<DepthStencilOperationDescription>
{
    /// <summary>
    /// A built-in description with default values.
    /// </summary>
    public static readonly DepthStencilOperationDescription Default = new(StencilOperation.Keep, StencilOperation.Keep, StencilOperation.Keep, ComparisonFunction.Always);

    /// <summary>
    /// Initializes a new instance of the <see cref="DepthStencilOperationDescription"/> struct.
    /// </summary>
    /// <param name="stencilFailOp">A <see cref="StencilOperation"/> value that identifies the stencil operation to perform when stencil testing fails.</param>
    /// <param name="stencilDepthFailOp">A <see cref="StencilOperation"/> value that identifies the stencil operation to perform when stencil testing passes and depth testing fails.</param>
    /// <param name="stencilPassOp">A <see cref="StencilOperation"/> value that identifies the stencil operation to perform when stencil testing and depth testing both pass.</param>
    /// <param name="stencilFunc">A <see cref="ComparisonFunction"/> value that identifies the function that compares stencil data against existing stencil data.</param>
    public DepthStencilOperationDescription(StencilOperation stencilFailOp, StencilOperation stencilDepthFailOp, StencilOperation stencilPassOp, ComparisonFunction stencilFunc)
    {
        StencilFailOp = stencilFailOp;
        StencilDepthFailOp = stencilDepthFailOp;
        StencilPassOp = stencilPassOp;
        StencilFunc = stencilFunc;
    }

    public static bool operator ==(in DepthStencilOperationDescription left, in DepthStencilOperationDescription right)
    {
        return (left.StencilFailOp == right.StencilFailOp)
            && (left.StencilDepthFailOp == right.StencilDepthFailOp)
            && (left.StencilPassOp == right.StencilPassOp)
            && (left.StencilFunc == right.StencilFunc);
    }

    public static bool operator !=(in DepthStencilOperationDescription left, in DepthStencilOperationDescription right)
        => !(left == right);

    public override bool Equals(object? obj) => (obj is DepthStencilOperationDescription other) && Equals(other);

    public bool Equals(DepthStencilOperationDescription other) => this == other;

    public override int GetHashCode() => HashCode.Combine(StencilFailOp, StencilDepthFailOp, StencilPassOp, StencilFunc);
}
