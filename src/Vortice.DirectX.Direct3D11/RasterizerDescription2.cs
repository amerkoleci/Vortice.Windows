// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D11
{
    /// <summary>
    /// Describes rasterizer state.
    /// </summary>
    public partial struct RasterizerDescription2
    {
        /// <summary>
        /// A built-in description with settings with settings for not culling any primitives.
        /// </summary>
        public static readonly RasterizerDescription2 CullNone = new RasterizerDescription2(CullMode.None, FillMode.Solid);

        /// <summary>
        /// A built-in description with settings for culling primitives with clockwise winding order.
        /// </summary>
        public static readonly RasterizerDescription2 CullClockwise = new RasterizerDescription2(CullMode.Front, FillMode.Solid);

        /// <summary>
        /// A built-in description with settings for culling primitives with counter-clockwise winding order.
        /// </summary>
        public static readonly RasterizerDescription2 CullCounterClockwise = new RasterizerDescription2(CullMode.Back, FillMode.Solid);

        /// <summary>
        /// A built-in description with settings for not culling any primitives and wireframe fill mode.
        /// </summary>
        public static readonly RasterizerDescription2 Wireframe = new RasterizerDescription2(CullMode.Back, FillMode.Wireframe);

        /// <summary>
        /// Initializes a new instance of the <see cref="RasterizerDescription"/> class.
        /// </summary>
        /// <param name="cullMode">A <see cref="CullMode"/> value that specifies that triangles facing the specified direction are not drawn..</param>
        /// <param name="fillMode">A <see cref="FillMode"/> value that specifies the fill mode to use when rendering.</param>
        public RasterizerDescription2(CullMode cullMode, FillMode fillMode)
        {
            CullMode = cullMode;
            FillMode = fillMode;
            FrontCounterClockwise = false;
            DepthBias = RasterizerDescription.DefaultDepthBias;
            DepthBiasClamp = RasterizerDescription.DefaultDepthBiasClamp;
            SlopeScaledDepthBias = RasterizerDescription.DefaultSlopeScaledDepthBias;
            DepthClipEnable = true;
            ScissorEnable = false;
            MultisampleEnable = false;
            AntialiasedLineEnable = false;
            ForcedSampleCount = 0;
            ConservativeRaster = ConservativeRasterizationMode.Off;
        }
    }
}
