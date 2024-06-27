// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

/// <summary>
/// Describes rasterizer state.
/// </summary>
public partial struct RasterizerDescription1 : IEquatable<RasterizerDescription1>
{
    /// <summary>
    /// A built-in description with settings with settings for not culling any primitives.
    /// </summary>
    public static RasterizerDescription1 CullNone => new(CullMode.None, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with clockwise winding order.
    /// </summary>
    public static RasterizerDescription1 CullFront => new(CullMode.Front, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with counter-clockwise winding order.
    /// </summary>
    public static RasterizerDescription1 CullBack => new(CullMode.Back, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for not culling any primitives and wireframe fill mode.
    /// </summary>
    public static RasterizerDescription1 Wireframe => new(CullMode.None, FillMode.Wireframe);

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription"/> class.
    /// </summary>
    /// <param name="cullMode">A <see cref="CullMode"/> value that specifies that triangles facing the specified direction are not drawn..</param>
    /// <param name="fillMode">A <see cref="FillMode"/> value that specifies the fill mode to use when rendering.</param>
    public RasterizerDescription1(CullMode cullMode, FillMode fillMode)
    {
        CullMode = cullMode;
        FillMode = fillMode;
        FrontCounterClockwise = false;
        DepthBias = ID3D11RasterizerState.DefaultDepthBias;
        DepthBiasClamp = ID3D11RasterizerState.DefaultDepthBiasClamp;
        SlopeScaledDepthBias = ID3D11RasterizerState.DefaultSlopeScaledDepthBias;
        DepthClipEnable = true;
        ScissorEnable = false;
        MultisampleEnable = false;
        AntialiasedLineEnable = false;
        ForcedSampleCount = 0;
    }

    public static bool operator ==(in RasterizerDescription1 left, in RasterizerDescription1 right)
    {
        return (left.FillMode == right.FillMode)
            && (left.CullMode == right.CullMode)
            && (left.FrontCounterClockwise == right.FrontCounterClockwise)
            && (left.DepthBias == right.DepthBias)
            && (left.DepthBiasClamp == right.DepthBiasClamp)
            && (left.SlopeScaledDepthBias == right.SlopeScaledDepthBias)
            && (left.DepthClipEnable == right.DepthClipEnable)
            && (left.ScissorEnable == right.ScissorEnable)
            && (left.MultisampleEnable == right.MultisampleEnable)
            && (left.AntialiasedLineEnable == right.AntialiasedLineEnable)
            && (left.ForcedSampleCount == right.ForcedSampleCount);
    }

    public static bool operator !=(in RasterizerDescription1 left, in RasterizerDescription1 right)
        => !(left == right);

    public override bool Equals(object? obj) => (obj is RasterizerDescription1 other) && Equals(other);

    public bool Equals(RasterizerDescription1 other) => this == other;

    public override int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(FillMode);
        hashCode.Add(CullMode);
        hashCode.Add(FrontCounterClockwise);
        hashCode.Add(DepthBias);
        hashCode.Add(DepthBiasClamp);
        hashCode.Add(SlopeScaledDepthBias);
        hashCode.Add(DepthClipEnable);
        hashCode.Add(ScissorEnable);
        hashCode.Add(MultisampleEnable);
        hashCode.Add(AntialiasedLineEnable);
        hashCode.Add(ForcedSampleCount);
        return hashCode.ToHashCode();
    }
}
