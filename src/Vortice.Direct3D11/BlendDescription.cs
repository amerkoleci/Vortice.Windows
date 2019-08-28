// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes the blend state.
    /// </summary>
    public partial struct BlendDescription
    {
        /// <summary>
        /// A built-in description with settings for default blend, that is no blend at all.
        /// </summary>
        public static readonly BlendDescription Default = new BlendDescription(Blend.One, Blend.Zero);

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

            for (var i = 0; i < SimultaneousRenderTargetCount; i++)
            {
                RenderTarget[i].SourceBlend = sourceBlend;
                RenderTarget[i].DestinationBlend = destinationBlend;
                RenderTarget[i].BlendOperation = BlendOperation.Add;
                RenderTarget[i].SourceBlendAlpha = srcBlendAlpha;
                RenderTarget[i].DestinationBlendAlpha = destBlendAlpha;
                RenderTarget[i].BlendOperationAlpha = BlendOperation.Add;
                RenderTarget[i].RenderTargetWriteMask = ColorWriteEnable.All;
                RenderTarget[i].IsBlendEnabled = IsBlendEnabled(ref RenderTarget[i]);
            }
        }

        private static bool IsBlendEnabled(ref RenderTargetBlendDescription renderTarget)
        {
            return renderTarget.BlendOperationAlpha != BlendOperation.Add
                    || renderTarget.SourceBlendAlpha != Blend.One
                    || renderTarget.DestinationBlendAlpha != Blend.Zero
                    || renderTarget.BlendOperation != BlendOperation.Add
                    || renderTarget.SourceBlend != Blend.One
                    || renderTarget.DestinationBlend != Blend.Zero;
        }
    }
}
