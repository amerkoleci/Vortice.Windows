// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes rasterizer state.
/// </summary>
public partial struct RasterizerDescription
{
    /// <summary>
    /// A built-in description with settings with settings for not culling any primitives.
    /// </summary>
    public static RasterizerDescription CullNone => new(CullMode.None, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with clockwise winding order.
    /// </summary>
    public static RasterizerDescription CullClockwise => new(CullMode.Front, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with counter-clockwise winding order.
    /// </summary>
    public static RasterizerDescription CullCounterClockwise => new(CullMode.Back, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for not culling any primitives and wireframe fill mode.
    /// </summary>
    public static RasterizerDescription Wireframe => new(CullMode.Back, FillMode.Wireframe);

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription"/> struct.
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
    public RasterizerDescription(
        CullMode cullMode,
        FillMode fillMode,
        bool frontCounterClockwise = false,
        int depthBias = D3D12.DefaultDepthBias,
        float depthBiasClamp = D3D12.DefaultDepthBiasClamp,
        float slopeScaledDepthBias = D3D12.DefaultSlopeScaledDepthBias,
        bool depthClipEnable = true,
        bool multisampleEnable = false,
        bool antialiasedLineEnable = false,
        int forcedSampleCount = 0,
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
}
