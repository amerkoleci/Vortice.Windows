// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the blend state.
/// </summary>
public partial struct BlendDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='D3D12_BLEND_DESC::AlphaToCoverageEnable']/*" />
    public RawBool AlphaToCoverageEnable;

    /// <include file="Documentation.xml" path="/comments/comment[@id='D3D12_BLEND_DESC::IndependentBlendEnable']/*" />
    public RawBool IndependentBlendEnable;

    /// <include file="Documentation.xml" path="/comments/comment[@id='D3D12_BLEND_DESC::RenderTarget']/*" />
	public RenderTarget__FixedBuffer RenderTarget;

    /// <summary>
    /// A built-in description with settings for opaque blend, that is overwriting the source with the destination data.
    /// </summary>
    public static BlendDescription Opaque => new(Blend.One, Blend.Zero);

    /// <summary>
    /// A built-in description with settings for alpha blend, that is blending the source and destination data using alpha.
    /// </summary>
    public static BlendDescription AlphaBlend => new(Blend.One, Blend.InverseSourceAlpha);

    /// <summary>
    /// A built-in description with settings for additive blend, that is adding the destination data to the source data without using alpha.
    /// </summary>
    public static BlendDescription Additive => new(Blend.SourceAlpha, Blend.One);

    /// <summary>
    /// A built-in description with settings for blending with non-premultipled alpha, that is blending source and destination data using alpha while assuming the color data contains no alpha information.
    /// </summary>
    public static BlendDescription NonPremultiplied => new(Blend.SourceAlpha, Blend.InverseSourceAlpha);

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
            RenderTarget[i].BlendEnable = IsBlendEnabled(RenderTarget[i]);
            RenderTarget[i].LogicOpEnable = false;
            RenderTarget[i].SourceBlend = sourceBlend;
            RenderTarget[i].DestinationBlend = destinationBlend;
            RenderTarget[i].BlendOperation = BlendOperation.Add;
            RenderTarget[i].SourceBlendAlpha = srcBlendAlpha;
            RenderTarget[i].DestinationBlendAlpha = destBlendAlpha;
            RenderTarget[i].BlendOperationAlpha = BlendOperation.Add;
            RenderTarget[i].LogicOp = LogicOp.Noop;
            RenderTarget[i].RenderTargetWriteMask = ColorWriteEnable.All;
        }
    }

    private static bool IsBlendEnabled(in RenderTargetBlendDescription renderTarget)
    {
        return renderTarget.BlendOperationAlpha != BlendOperation.Add
                || renderTarget.SourceBlendAlpha != Blend.One
                || renderTarget.DestinationBlendAlpha != Blend.Zero
                || renderTarget.BlendOperation != BlendOperation.Add
                || renderTarget.SourceBlend != Blend.One
                || renderTarget.DestinationBlend != Blend.Zero;
    }


    public partial struct RenderTarget__FixedBuffer
    {
        public RenderTargetBlendDescription e0;
        public RenderTargetBlendDescription e1;
        public RenderTargetBlendDescription e2;
        public RenderTargetBlendDescription e3;
        public RenderTargetBlendDescription e4;
        public RenderTargetBlendDescription e5;
        public RenderTargetBlendDescription e6;
        public RenderTargetBlendDescription e7;

        [UnscopedRef]
        public ref RenderTargetBlendDescription this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref AsSpan()[index];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [UnscopedRef]
        public Span<RenderTargetBlendDescription> AsSpan()
        {
            return MemoryMarshal.CreateSpan(ref e0, 8);
        }
    }
}
