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
    public static readonly RasterizerDescription CullNone = new(CullMode.None, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with clockwise winding order.
    /// </summary>
    public static readonly RasterizerDescription CullClockwise = new(CullMode.Front, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for culling primitives with counter-clockwise winding order.
    /// </summary>
    public static readonly RasterizerDescription CullCounterClockwise = new(CullMode.Back, FillMode.Solid);

    /// <summary>
    /// A built-in description with settings for not culling any primitives and wireframe fill mode.
    /// </summary>
    public static readonly RasterizerDescription Wireframe = new(CullMode.Back, FillMode.Wireframe);

    /// <summary>
    /// Initializes a new instance of the <see cref="RasterizerDescription"/> class.
    /// </summary>
    /// <param name="cullMode">A <see cref="CullMode"/> value that specifies that triangles facing the specified direction are not drawn..</param>
    /// <param name="fillMode">A <see cref="FillMode"/> value that specifies the fill mode to use when rendering.</param>
    public RasterizerDescription(CullMode cullMode, FillMode fillMode)
    {
        CullMode = cullMode;
        FillMode = fillMode;
        FrontCounterClockwise = false;
        DepthBias = D3D12.DefaultDepthBias;
        DepthBiasClamp = D3D12.DefaultDepthBiasClamp;
        SlopeScaledDepthBias = D3D12.DefaultSlopeScaledDepthBias;
        DepthClipEnable = true;
        MultisampleEnable = false;
        AntialiasedLineEnable = false;
        ForcedSampleCount = 0;
        ConservativeRaster = ConservativeRasterizationMode.Off;
    }
}
