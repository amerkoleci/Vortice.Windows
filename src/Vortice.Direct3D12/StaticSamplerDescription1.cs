// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes a static sampler state.
/// </summary>
public partial struct StaticSamplerDescription1
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StaticSamplerDescription1"/> struct.
    /// </summary>
    /// <param name="shaderVisibility">The shader visibility.</param>
    /// <param name="shaderRegister">The shader register.</param>
    /// <param name="registerSpace">The register space.</param>
    /// <param name="flags">The <see cref="SamplerFlags"/>.</param>
    public StaticSamplerDescription1(ShaderVisibility shaderVisibility, int shaderRegister, int registerSpace, SamplerFlags flags)
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
        Flags = flags;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StaticSamplerDescription1"/> struct.
    /// </summary>
    public StaticSamplerDescription1(Filter filter,
        TextureAddressMode addressU,
        TextureAddressMode addressV,
        TextureAddressMode addressW,
        float mipLODBias,
        int maxAnisotropy,
        ComparisonFunction comparisonFunction,
        StaticBorderColor borderColor,
        float minLOD,
        float maxLOD,
        int shaderRegister,
        int registerSpace,
        ShaderVisibility shaderVisibility = ShaderVisibility.All,
        SamplerFlags flags = SamplerFlags.None)
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
        Flags = flags;
    }
}   
