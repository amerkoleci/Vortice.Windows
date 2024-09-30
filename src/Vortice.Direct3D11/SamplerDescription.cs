// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a sampler state.
/// </summary>
public unsafe partial struct SamplerDescription : IEquatable<SamplerDescription>
{
    public static SamplerDescription PointWrap => new(Filter.MinMagMipPoint, TextureAddressMode.Wrap);
    public static SamplerDescription PointClamp => new(Filter.MinMagMipPoint, TextureAddressMode.Clamp);

    public static SamplerDescription LinearWrap => new(Filter.MinMagMipLinear, TextureAddressMode.Wrap);
    public static SamplerDescription LinearClamp => new(Filter.MinMagMipLinear, TextureAddressMode.Clamp);

    public static SamplerDescription AnisotropicWrap => new(Filter.Anisotropic, TextureAddressMode.Wrap, 0.0f, ID3D11SamplerState.MaxMaxAnisotropy);
    public static SamplerDescription AnisotropicClamp => new(Filter.Anisotropic, TextureAddressMode.Clamp, 0.0f, ID3D11SamplerState.MaxMaxAnisotropy);

    /// <summary>
    /// Initializes a new instance of the <see cref="SamplerDescription"/> struct.
    /// </summary>
    /// <param name="filter">Filtering method to use when sampling a texture.</param>
    /// <param name="addressU">Method to use for resolving a u texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="addressV">Method to use for resolving a v texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="addressW">Method to use for resolving a w texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="mipLODBias">Offset from the calculated mipmap level.</param>
    /// <param name="maxAnisotropy">Clamping value used if <see cref="Filter.Anisotropic"/> or <see cref="Filter.ComparisonAnisotropic"/> is specified in Filter. Valid values are between 1 and 16.</param>
    /// <param name="comparisonFunc">A function that compares sampled data against existing sampled data. </param>
    /// <param name="borderColor">Border color to use if <see cref="TextureAddressMode.Border"/> is specified for AddressU, AddressV, or AddressW.</param>
    /// <param name="minLOD">Lower end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed.</param>
    /// <param name="maxLOD">Upper end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed. This value must be greater than or equal to MinLOD. </param>
    public SamplerDescription(
        Filter filter,
        TextureAddressMode addressU,
        TextureAddressMode addressV,
        TextureAddressMode addressW,
        float mipLODBias,
        uint maxAnisotropy,
        ComparisonFunction comparisonFunc,
        in Color4 borderColor,
        float minLOD = float.MinValue,
        float maxLOD = float.MaxValue)
    {
        Filter = filter;
        AddressU = addressU;
        AddressV = addressV;
        AddressW = addressW;
        MipLODBias = mipLODBias;
        MaxAnisotropy = maxAnisotropy;
        ComparisonFunc = comparisonFunc;
        BorderColor = borderColor;
        MinLOD = minLOD;
        MaxLOD = maxLOD;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SamplerDescription"/> struct.
    /// </summary>
    /// <param name="filter">Filtering method to use when sampling a texture.</param>
    /// <param name="addressU">Method to use for resolving a u texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="addressV">Method to use for resolving a v texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="addressW">Method to use for resolving a w texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="mipLODBias">Offset from the calculated mipmap level.</param>
    /// <param name="maxAnisotropy">Clamping value used if <see cref="Filter.Anisotropic"/> or <see cref="Filter.ComparisonAnisotropic"/> is specified in Filter. Valid values are between 1 and 16.</param>
    /// <param name="comparisonFunc">A function that compares sampled data against existing sampled data. </param>
    /// <param name="minLOD">Lower end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed.</param>
    /// <param name="maxLOD">Upper end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed. This value must be greater than or equal to MinLOD. </param>
    public SamplerDescription(
        Filter filter,
        TextureAddressMode addressU,
        TextureAddressMode addressV,
        TextureAddressMode addressW,
        float mipLODBias = 0.0f,
        uint maxAnisotropy = 1,
        ComparisonFunction comparisonFunc = ComparisonFunction.Never,
        float minLOD = float.MinValue,
        float maxLOD = float.MaxValue)
    {
        Filter = filter;
        AddressU = addressU;
        AddressV = addressV;
        AddressW = addressW;
        MipLODBias = mipLODBias;
        MaxAnisotropy = maxAnisotropy;
        ComparisonFunc = comparisonFunc;
        BorderColor = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        MinLOD = minLOD;
        MaxLOD = maxLOD;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SamplerDescription"/> struct.
    /// </summary>
    /// <param name="filter">Filtering method to use when sampling a texture.</param>
    /// <param name="address">Method to use for resolving a u, v e w texture coordinate that is outside the 0 to 1 range.</param>
    /// <param name="mipLODBias">Offset from the calculated mipmap level.</param>
    /// <param name="maxAnisotropy">Clamping value used if <see cref="Filter.Anisotropic"/> or <see cref="Filter.ComparisonAnisotropic"/> is specified in Filter. Valid values are between 1 and 16.</param>
    /// <param name="comparisonFunc">A function that compares sampled data against existing sampled data. </param>
    /// <param name="minLOD">Lower end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed.</param>
    /// <param name="maxLOD">Upper end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed. This value must be greater than or equal to MinLOD. </param>
    public SamplerDescription(
        Filter filter,
        TextureAddressMode address,
        float mipLODBias = 0.0f,
        uint maxAnisotropy = 1,
        ComparisonFunction comparisonFunc = ComparisonFunction.Never,
        float minLOD = float.MinValue,
        float maxLOD = float.MaxValue)
    {
        Filter = filter;
        AddressU = address;
        AddressV = address;
        AddressW = address;
        MipLODBias = mipLODBias;
        MaxAnisotropy = maxAnisotropy;
        ComparisonFunc = comparisonFunc;
        BorderColor = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
        MinLOD = minLOD;
        MaxLOD = maxLOD;
    }

    public static bool operator ==(in SamplerDescription left, in SamplerDescription right)
    {
        return (left.Filter == right.Filter)
            && (left.AddressU == right.AddressU)
            && (left.AddressV == right.AddressV)
            && (left.AddressW == right.AddressW)
            && (left.MipLODBias == right.MipLODBias)
            && (left.MaxAnisotropy == right.MaxAnisotropy)
            && (left.ComparisonFunc == right.ComparisonFunc)
            && (left.BorderColor[0] == right.BorderColor[0])
            && (left.BorderColor[1] == right.BorderColor[1])
            && (left.BorderColor[2] == right.BorderColor[2])
            && (left.BorderColor[3] == right.BorderColor[3])
            && (left.MinLOD == right.MinLOD)
            && (left.MaxLOD == right.MaxLOD);
    }

    public static bool operator !=(in SamplerDescription left, in SamplerDescription right)
        => !(left == right);

    public override bool Equals(object? obj) => (obj is SamplerDescription other) && Equals(other);

    public bool Equals(SamplerDescription other) => this == other;

    public override readonly int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(Filter);
        hashCode.Add(AddressU);
        hashCode.Add(AddressV);
        hashCode.Add(AddressW);
        hashCode.Add(MipLODBias);
        hashCode.Add(MaxAnisotropy);
        hashCode.Add(ComparisonFunc);
        hashCode.Add(BorderColor[0]);
        hashCode.Add(BorderColor[1]);
        hashCode.Add(BorderColor[2]);
        hashCode.Add(BorderColor[3]);
        hashCode.Add(MinLOD);
        hashCode.Add(MaxLOD);
        return hashCode.ToHashCode();
    }
}
