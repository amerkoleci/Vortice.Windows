// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a static sampler state.
/// </summary>
public partial struct StaticSamplerDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StaticSamplerDescription"/> struct.
    /// </summary>
    /// <param name="shaderVisibility">The shader visibility.</param>
    /// <param name="shaderRegister">The shader register.</param>
    /// <param name="registerSpace">The register space.</param>
    public StaticSamplerDescription(ShaderVisibility shaderVisibility, int shaderRegister, int registerSpace)
    {
        Filter = Filter.MinMagMipLinear;
        AddressU = TextureAddressMode.Clamp;
        AddressV = TextureAddressMode.Clamp;
        AddressW = TextureAddressMode.Clamp;
        MipLODBias = 0.0f;
        MaxAnisotropy = 1;
        ComparisonFunction = ComparisonFunction.Never;
        BorderColor = StaticBorderColor.TransparentBlack;
        MinLOD = float.MinValue;
        MaxLOD = float.MaxValue;

        ShaderRegister = shaderRegister;
        RegisterSpace = registerSpace;
        ShaderVisibility = shaderVisibility;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaticSamplerDescription"/> struct.
    /// </summary>
    public StaticSamplerDescription(
        int shaderRegister,
        Filter filter = Filter.Anisotropic,
        TextureAddressMode addressU = TextureAddressMode.Wrap,
        TextureAddressMode addressV = TextureAddressMode.Wrap,
        TextureAddressMode addressW = TextureAddressMode.Wrap,
        float mipLODBias = 0.0f,
        int maxAnisotropy = 16,
        ComparisonFunction comparisonFunction = ComparisonFunction.LessEqual,
        StaticBorderColor borderColor = StaticBorderColor.OpaqueWhite,
        float minLOD = 0.0f,
        float maxLOD = float.MaxValue,
        ShaderVisibility shaderVisibility = ShaderVisibility.All,
        int registerSpace = 0
        )
    {
        Filter = filter;
        AddressU = addressU;
        AddressV = addressV;
        AddressW = addressW;
        MipLODBias = mipLODBias;
        MaxAnisotropy = maxAnisotropy;
        ComparisonFunction = comparisonFunction;
        BorderColor = borderColor;
        MinLOD = minLOD;
        MaxLOD = maxLOD;
        ShaderRegister = shaderRegister;
        RegisterSpace = registerSpace;
        ShaderVisibility = shaderVisibility;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaticSamplerDescription"/> struct.
    /// </summary>
    /// <param name="samplerDescription">Sampler description</param>
    /// <param name="shaderVisibility">The shader visibility.</param>
    /// <param name="shaderRegister">The shader register.</param>
    /// <param name="registerSpace">The register space.</param>
    public StaticSamplerDescription(SamplerDescription samplerDescription, ShaderVisibility shaderVisibility, int shaderRegister, int registerSpace) : this()
    {
        ShaderVisibility = shaderVisibility;
        ShaderRegister = shaderRegister;
        RegisterSpace = registerSpace;

        // Unlike regular samplers, static samplers only support three possible border colors: opaque white, opaque black, and transparent.
        // So if the sampler has a border color that matches one of those exactly, we can convert it; otherwise it's left as the default (transparent).
        if (samplerDescription.BorderColor == Colors.White)
            BorderColor = StaticBorderColor.OpaqueWhite;
        else if (samplerDescription.BorderColor == Colors.Black)
            BorderColor = StaticBorderColor.OpaqueBlack;
        else
            BorderColor = StaticBorderColor.TransparentBlack;

        Filter = samplerDescription.Filter;
        AddressU = samplerDescription.AddressU;
        AddressV = samplerDescription.AddressV;
        AddressW = samplerDescription.AddressW;
        MinLOD = samplerDescription.MinLOD;
        MaxLOD = samplerDescription.MaxLOD;
        MipLODBias = samplerDescription.MipLODBias;
        MaxAnisotropy = samplerDescription.MaxAnisotropy;
        ComparisonFunction = samplerDescription.ComparisonFunction;
    }
}   
