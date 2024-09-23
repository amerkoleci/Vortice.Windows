// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes rasterizer state.
/// </summary>
public partial struct RasterizerDescription2
{
    /// <summary>
    /// A built-in description with settings with settings for not culling any primitives.
    /// </summary>
    public static RasterizerDescription2 CullNone => new(CullMode.None, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with clockwise winding order.
    /// </summary>
    public static RasterizerDescription2 CullClockwise => new(CullMode.Front, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with counter-clockwise winding order.
    /// </summary>
    public static RasterizerDescription2 CullCounterClockwise => new(CullMode.Back, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for not culling any primitives and wireframe fill mode.
    /// </summary>
    public static RasterizerDescription2 Wireframe => new(CullMode.Back, FillMode.Wireframe);

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription2"/> struct.
    /// </summary>
    /// <param name="cullMode">A <see cref="CullMode"/> value that specifies that triangles facing the specified direction are not drawn.</param>
    /// <param name="fillMode">A <see cref="FillMode"/> value that specifies the fill mode to use when rendering.</param>
    /// <param name="frontCounterClockwise"></param>
    /// <param name="depthBias"></param>
    /// <param name="depthBiasClamp"></param>
    /// <param name="slopeScaledDepthBias"></param>
    /// <param name="depthClipEnable"></param>
    /// <param name="lineRasterizationMode"></param>
    /// <param name="forcedSampleCount"></param>
    /// <param name="conservativeRaster"></param>
    public RasterizerDescription2(
        CullMode cullMode,
        FillMode fillMode,
        bool frontCounterClockwise = false,
        float depthBias = D3D12.DefaultDepthBias,
        float depthBiasClamp = D3D12.DefaultDepthBiasClamp,
        float slopeScaledDepthBias = D3D12.DefaultSlopeScaledDepthBias,
        bool depthClipEnable = true,
        LineRasterizationMode lineRasterizationMode = LineRasterizationMode.Aliased,
        uint forcedSampleCount = 0,
        ConservativeRasterizationMode conservativeRaster = ConservativeRasterizationMode.Off)
    {
        CullMode = cullMode;
        FillMode = fillMode;
        FrontCounterClockwise = frontCounterClockwise;
        DepthBias = depthBias;
        DepthBiasClamp = depthBiasClamp;
        SlopeScaledDepthBias = slopeScaledDepthBias;
        DepthClipEnable = depthClipEnable;
        LineRasterizationMode = lineRasterizationMode;
        ForcedSampleCount = forcedSampleCount;
        ConservativeRaster = conservativeRaster;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription2"/> struct.
    /// </summary>
    public RasterizerDescription2(in RasterizerDescription description)
        : this(new RasterizerDescription1(description))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription2"/> struct.
    /// </summary>
    public RasterizerDescription2(in RasterizerDescription1 description)
    {
        CullMode = description.CullMode;
        FillMode = description.FillMode;
        FrontCounterClockwise = description.FrontCounterClockwise;
        DepthBias = (float)description.DepthBias;
        DepthBiasClamp = description.DepthBiasClamp;
        SlopeScaledDepthBias = description.SlopeScaledDepthBias;
        DepthClipEnable = description.DepthClipEnable;
        LineRasterizationMode = LineRasterizationMode.Aliased;
        if (description.MultisampleEnable)
        {
            LineRasterizationMode = LineRasterizationMode.QuadrilateralWide;
        }
        else if (description.AntialiasedLineEnable)
        {
            LineRasterizationMode = LineRasterizationMode.AlphaAntialiased;
        }
        ForcedSampleCount = description.ForcedSampleCount;
        ConservativeRaster = description.ConservativeRaster;
    }

}
