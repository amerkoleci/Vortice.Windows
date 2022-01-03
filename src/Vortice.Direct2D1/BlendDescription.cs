// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1;

/// <summary>
/// Defines a blend description to be used in a particular blend transform.
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
    /// <param name="sourceBlend">Specifies the first RGB data source and includes an optional preblend operation.</param>
    /// <param name="destinationBlend">Specifies the second RGB data source and includes an optional preblend operation.</param>
    /// <param name="sourceBlendAlpha">The source alpha blend.</param>
    /// <param name="destinationBlendAlpha">The destination alpha blend.</param>
    public BlendDescription(Blend sourceBlend, Blend destinationBlend, Blend sourceBlendAlpha, Blend destinationBlendAlpha)
    {
        SourceBlend = sourceBlend;
        DestinationBlend = destinationBlend;
        BlendOperation = BlendOperation.Add;
        SourceBlendAlpha = sourceBlendAlpha;
        DestinationBlendAlpha = destinationBlendAlpha;
        BlendOperationAlpha = BlendOperation.Add;
        BlendFactor = default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlendDescription"/> struct.
    /// </summary>
    /// <param name="sourceBlend">Specifies the first RGB data source and includes an optional preblend operation.</param>
    /// <param name="destinationBlend">Specifies the second RGB data source and includes an optional preblend operation.</param>
    /// <param name="blendOperation">Specifies how to combine the RGB data sources.</param>
    /// <param name="sourceBlendAlpha">Specifies the first alpha data source and includes an optional preblend operation. Blend options that end in Color are not allowed.</param>
    /// <param name="destinationBlendAlpha">Specifies the second alpha data source and includes an optional preblend operation. Blend options that end in Color are not allowed.</param>
    /// <param name="blendOperationAlpha">Specifies how to combine the alpha data sources.</param>
    public BlendDescription(
        Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation,
        Blend sourceBlendAlpha, Blend destinationBlendAlpha, BlendOperation blendOperationAlpha)
    {
        SourceBlend = sourceBlend;
        DestinationBlend = destinationBlend;
        BlendOperation = blendOperation;
        SourceBlendAlpha = sourceBlendAlpha;
        DestinationBlendAlpha = destinationBlendAlpha;
        BlendOperationAlpha = blendOperationAlpha;
        BlendFactor = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlendDescription"/> struct.
    /// </summary>
    /// <param name="sourceBlend">Specifies the first RGB data source and includes an optional preblend operation.</param>
    /// <param name="destinationBlend">Specifies the second RGB data source and includes an optional preblend operation.</param>
    /// <param name="blendOperation">Specifies how to combine the RGB data sources.</param>
    /// <param name="sourceBlendAlpha">Specifies the first alpha data source and includes an optional preblend operation. Blend options that end in Color are not allowed.</param>
    /// <param name="destinationBlendAlpha">Specifies the second alpha data source and includes an optional preblend operation. Blend options that end in Color are not allowed.</param>
    /// <param name="blendOperationAlpha">Specifies how to combine the alpha data sources.</param>
    /// <param name="blendFactor">Parameters to the blend operations. The blend must use <see cref="Blend.BlendFactor"/> for this to be used.</param>
    public BlendDescription(
        Blend sourceBlend, Blend destinationBlend, BlendOperation blendOperation,
        Blend sourceBlendAlpha, Blend destinationBlendAlpha, BlendOperation blendOperationAlpha,
        in Color4 blendFactor)
    {
        SourceBlend = sourceBlend;
        DestinationBlend = destinationBlend;
        BlendOperation = blendOperation;
        SourceBlendAlpha = sourceBlendAlpha;
        DestinationBlendAlpha = destinationBlendAlpha;
        BlendOperationAlpha = blendOperationAlpha;
        BlendFactor = blendFactor;
    }
}
