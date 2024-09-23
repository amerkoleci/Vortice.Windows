// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes rasterizer state.
/// </summary>
public partial struct RasterizerDescription1
{
    /// <summary>
    /// A built-in description with settings with settings for not culling any primitives.
    /// </summary>
    public static RasterizerDescription1 CullNone => new(CullMode.None, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with clockwise winding order.
    /// </summary>
    public static RasterizerDescription1 CullClockwise => new(CullMode.Front, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with counter-clockwise winding order.
    /// </summary>
    public static RasterizerDescription1 CullCounterClockwise => new(CullMode.Back, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for not culling any primitives and wireframe fill mode.
    /// </summary>
    public static RasterizerDescription1 Wireframe => new(CullMode.Back, FillMode.Wireframe);

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription1"/> struct.
    /// </summary>
    /// <param name="cullMode">A <see cref="CullMode"/> value that specifies that triangles facing the specified direction are not drawn.</param>
    /// <param name="fillMode">A <see cref="FillMode"/> value that specifies the fill mode to use when rendering.</param>
    /// <param name="frontCounterClockwise"></param>
    /// <param name="depthBias"></param>
    /// <param name="depthBiasClamp"></param>
    /// <param name="slopeScaledDepthBias"></param>
    /// <param name="depthClipEnable"></param>
    /// <param name="multisampleEnable"></param>
    /// <param name="antialiasedLineEnable"></param>
    /// <param name="forcedSampleCount"></param>
    /// <param name="conservativeRaster"></param>
    public RasterizerDescription1(
        CullMode cullMode,
        FillMode fillMode,
        bool frontCounterClockwise = false,
        float depthBias = D3D12.DefaultDepthBias,
        float depthBiasClamp = D3D12.DefaultDepthBiasClamp,
        float slopeScaledDepthBias = D3D12.DefaultSlopeScaledDepthBias,
        bool depthClipEnable = true,
        bool multisampleEnable = false,
        bool antialiasedLineEnable = false,
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
        MultisampleEnable = multisampleEnable;
        AntialiasedLineEnable = antialiasedLineEnable;
        ForcedSampleCount = forcedSampleCount;
        ConservativeRaster = conservativeRaster;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription1"/> struct.
    /// </summary>
    public RasterizerDescription1(in RasterizerDescription other)
    {
        CullMode = other.CullMode;
        FillMode = other.FillMode;
        FrontCounterClockwise = other.FrontCounterClockwise;
        DepthBias = (float)other.DepthBias;
        DepthBiasClamp = other.DepthBiasClamp;
        SlopeScaledDepthBias = other.SlopeScaledDepthBias;
        DepthClipEnable = other.DepthClipEnable;
        MultisampleEnable = other.MultisampleEnable;
        AntialiasedLineEnable = other.AntialiasedLineEnable;
        ForcedSampleCount = other.ForcedSampleCount;
        ConservativeRaster = other.ConservativeRaster;
    }
}
