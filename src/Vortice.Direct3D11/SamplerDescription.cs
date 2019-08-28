// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Describes a sampler state.
    /// </summary>
    public partial struct SamplerDescription
    {
        /// <summary>
        /// A built-in description with default settings.
        /// </summary>
        public static readonly SamplerDescription Default = new SamplerDescription(Filter.MinMagMipLinear, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

        public static readonly SamplerDescription PointWrap = new SamplerDescription(Filter.MinMagMipPoint, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap);
        public static readonly SamplerDescription PointClamp = new SamplerDescription(Filter.MinMagMipPoint, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

        public static readonly SamplerDescription LinearWrap = new SamplerDescription(Filter.MinMagMipLinear, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap);
        public static readonly SamplerDescription LinearClamp = new SamplerDescription(Filter.MinMagMipLinear, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

        public static readonly SamplerDescription AnisotropicWrap = new SamplerDescription(Filter.Anisotropic, TextureAddressMode.Wrap, TextureAddressMode.Wrap, TextureAddressMode.Wrap);
        public static readonly SamplerDescription AnisotropicClamp = new SamplerDescription(Filter.Anisotropic, TextureAddressMode.Clamp, TextureAddressMode.Clamp, TextureAddressMode.Clamp);

        /// <summary>
        /// Initializes a new instance of the <see cref="SamplerDescription"/> struct.
        /// </summary>
        /// <param name="filter">Filtering method to use when sampling a texture.</param>
        /// <param name="addressU">Method to use for resolving a u texture coordinate that is outside the 0 to 1 range.</param>
        /// <param name="addressV">Method to use for resolving a v texture coordinate that is outside the 0 to 1 range.</param>
        /// <param name="addressW">Method to use for resolving a w texture coordinate that is outside the 0 to 1 range.</param>
        /// <param name="mipLODBias">Offset from the calculated mipmap level.</param>
        /// <param name="maxAnisotropy">Clamping value used if <see cref="Filter.Anisotropic"/> or <see cref="Filter.ComparisonAnisotropic"/> is specified in Filter. Valid values are between 1 and 16.</param>
        /// <param name="comparisonFunction">A function that compares sampled data against existing sampled data. </param>
        /// <param name="borderColor">Border color to use if <see cref="TextureAddressMode.Border"/> is specified for AddressU, AddressV, or AddressW.</param>
        /// <param name="minLOD">Lower end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed.</param>
        /// <param name="maxLOD">Upper end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed. This value must be greater than or equal to MinLOD. </param>
        public SamplerDescription(
            Filter filter,
            TextureAddressMode addressU,
            TextureAddressMode addressV,
            TextureAddressMode addressW,
            float mipLODBias,
            int maxAnisotropy,
            ComparisonFunction comparisonFunction,
            Color4 borderColor,
            float minLOD,
            float maxLOD)
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
        /// <param name="comparisonFunction">A function that compares sampled data against existing sampled data. </param>
        /// <param name="minLOD">Lower end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed.</param>
        /// <param name="maxLOD">Upper end of the mipmap range to clamp access to, where 0 is the largest and most detailed mipmap level and any level higher than that is less detailed. This value must be greater than or equal to MinLOD. </param>
        public SamplerDescription(
            Filter filter,
            TextureAddressMode addressU,
            TextureAddressMode addressV,
            TextureAddressMode addressW,
            float mipLODBias = 0.0f,
            int maxAnisotropy = 1,
            ComparisonFunction comparisonFunction = ComparisonFunction.Never,
            float minLOD = float.MinValue,
            float maxLOD = float.MaxValue)
        {
            Filter = filter;
            AddressU = addressU;
            AddressV = addressV;
            AddressW = addressW;
            MipLODBias = mipLODBias;
            MaxAnisotropy = maxAnisotropy;
            ComparisonFunction = comparisonFunction;
            BorderColor = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            MinLOD = minLOD;
            MaxLOD = maxLOD;
        }
    }
}
