// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes the blend state that you use in a call to <see cref="ID3D11Device1.CreateBlendState1(BlendDescription1)"/> to create a blend-state object.
    /// </summary>
    public partial struct BlendDescription1
    {
        /// <summary>
        /// A built-in description with settings for default blend, that is no blend at all.
        /// </summary>
        public static readonly BlendDescription1 Default = new BlendDescription1(Blend.One, Blend.Zero);

        /// <summary>
        /// A built-in description with settings for additive blend, that is adding the destination data to the source data without using alpha.
        /// </summary>
        public static readonly BlendDescription1 Additive = new BlendDescription1(Blend.SourceAlpha, Blend.One);

        /// <summary>
        /// A built-in description with settings for alpha blend, that is blending the source and destination data using alpha.
        /// </summary>
        public static readonly BlendDescription1 AlphaBlend = new BlendDescription1(Blend.One, Blend.InverseSourceAlpha);

        /// <summary>
        /// A built-in description with settings for blending with non-premultipled alpha, that is blending source and destination data using alpha while assuming the color data contains no alpha information.
        /// </summary>
        public static readonly BlendDescription1 NonPremultiplied = new BlendDescription1(Blend.SourceAlpha, Blend.InverseSourceAlpha);

        /// <summary>
        /// A built-in description with settings for opaque blend, that is overwriting the source with the destination data.
        /// </summary>
        public static readonly BlendDescription1 Opaque = new BlendDescription1(Blend.One, Blend.Zero);

        /// <summary>
        /// Initializes a new instance of the <see cref="BlendDescription1"/> struct.
        /// </summary>
        /// <param name="sourceBlend">The source blend.</param>
        /// <param name="destinationBlend">The destination blend.</param>
        public BlendDescription1(Blend sourceBlend, Blend destinationBlend)
            : this(sourceBlend, destinationBlend, sourceBlend, destinationBlend)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlendDescription1"/> struct.
        /// </summary>
        /// <param name="sourceBlend">The source blend.</param>
        /// <param name="destinationBlend">The destination blend.</param>
        /// <param name="srcBlendAlpha">The source alpha blend.</param>
        /// <param name="destBlendAlpha">The destination alpha blend.</param>
        public BlendDescription1(Blend sourceBlend, Blend destinationBlend, Blend srcBlendAlpha, Blend destBlendAlpha)
            : this()
        {
            AlphaToCoverageEnable = false;
            IndependentBlendEnable = false;

            for (var i = 0; i < BlendDescription.SimultaneousRenderTargetCount; i++)
            {
                RenderTarget[i].IsLogicOperationEnabled = false;
                RenderTarget[i].SourceBlend = sourceBlend;
                RenderTarget[i].DestinationBlend = destinationBlend;
                RenderTarget[i].BlendOperation = BlendOperation.Add;
                RenderTarget[i].SourceBlendAlpha = srcBlendAlpha;
                RenderTarget[i].DestinationBlendAlpha = destBlendAlpha;
                RenderTarget[i].BlendOperationAlpha = BlendOperation.Add;
                RenderTarget[i].LogicOp = LogicOp.Noop;
                RenderTarget[i].RenderTargetWriteMask = ColorWriteEnable.All;
                RenderTarget[i].IsBlendEnabled = IsBlendEnabled(ref RenderTarget[i]);
            }
        }

        private static bool IsBlendEnabled(ref RenderTargetBlendDescription1 renderTarget)
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
