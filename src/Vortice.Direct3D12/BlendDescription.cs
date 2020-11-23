// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the blend state.
    /// </summary>
    public partial struct BlendDescription
    {
        /// <summary>
        /// A built-in description with settings for additive blend, that is adding the destination data to the source data without using alpha.
        /// </summary>
        public static readonly BlendDescription Additive = new BlendDescription(Blend.SourceAlpha, Blend.One);

        /// <summary>
        /// A built-in description with settings for alpha blend, that is blending the source and destination data using alpha.
        /// </summary>
        public static readonly BlendDescription AlphaBlend = new BlendDescription(Blend.One, Blend.InverseSourceAlpha);

        /// <summary>
        /// A built-in description with settings for blending with non-premultipled alpha, that is blending source and destination data using alpha while assuming the color data contains no alpha information.
        /// </summary>
        public static readonly BlendDescription NonPremultiplied = new BlendDescription(Blend.SourceAlpha, Blend.InverseSourceAlpha);

        /// <summary>
        /// A built-in description with settings for opaque blend, that is overwriting the source with the destination data.
        /// </summary>
        public static readonly BlendDescription Opaque = new BlendDescription(Blend.One, Blend.Zero);

        /// <summary>
        /// Initializes a new instance of the <see cref="BlendDescription"/> struct.
        /// </summary>
        /// <param name="sourceBlend">The source blend.</param>
        /// <param name="destinationBlend">The destination blend.</param>
        public BlendDescription(Blend sourceBlend, Blend destinationBlend)
            : this(sourceBlend, destinationBlend, sourceBlend, destinationBlend)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlendDescription"/> struct.
        /// </summary>
        /// <param name="sourceBlend">The source blend.</param>
        /// <param name="destinationBlend">The destination blend.</param>
        /// <param name="srcBlendAlpha">The source alpha blend.</param>
        /// <param name="destBlendAlpha">The destination alpha blend.</param>
        public BlendDescription(Blend sourceBlend, Blend destinationBlend, Blend srcBlendAlpha, Blend destBlendAlpha)
            : this()
        {
            AlphaToCoverageEnable = false;
            IndependentBlendEnable = false;

            for (int i = 0; i < D3D12.SimultaneousRenderTargetCount; i++)
            {
                RenderTarget[i].BlendEnable = IsBlendEnabled(ref RenderTarget[i]);
                RenderTarget[i].LogicOpEnable = false;
                RenderTarget[i].SrcBlend = sourceBlend;
                RenderTarget[i].DestBlend = destinationBlend;
                RenderTarget[i].BlendOp = BlendOperation.Add;
                RenderTarget[i].SrcBlendAlpha = srcBlendAlpha;
                RenderTarget[i].DestBlendAlpha = destBlendAlpha;
                RenderTarget[i].BlendOpAlpha = BlendOperation.Add;
                RenderTarget[i].LogicOp = LogicOp.Noop;
                RenderTarget[i].RenderTargetWriteMask = ColorWriteEnable.All;
            }
        }

        private static bool IsBlendEnabled(ref RenderTargetBlendDescription renderTarget)
        {
            return renderTarget.BlendOpAlpha != BlendOperation.Add
                    || renderTarget.SrcBlendAlpha != Blend.One
                    || renderTarget.DestBlendAlpha != Blend.Zero
                    || renderTarget.BlendOp != BlendOperation.Add
                    || renderTarget.SrcBlend != Blend.One
                    || renderTarget.DestBlend != Blend.Zero;
        }
    }
}
