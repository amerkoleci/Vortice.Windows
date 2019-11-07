// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class DistantSpecular : ID2D1Effect
    {
        public DistantSpecular(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.DistantSpecular))
        {
        }

        public DistantSpecular(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.DistantSpecular))
        {
        }

        public float Azimuth
        {
            set => SetValue((int)DistantSpecularProperties.Azimuth, value);
            get => GetFloatValue((int)DistantSpecularProperties.Azimuth);
        }

        public float Elevation
        {
            set => SetValue((int)DistantSpecularProperties.Elevation, value);
            get => GetFloatValue((int)DistantSpecularProperties.Elevation);
        }

        public float SpecularExponent
        {
            set => SetValue((int)DistantSpecularProperties.SpecularExponent, value);
            get => GetFloatValue((int)DistantSpecularProperties.SpecularExponent);
        }

        public float SpecularConstant
        {
            set => SetValue((int)DistantSpecularProperties.SpecularConstant, value);
            get => GetFloatValue((int)DistantSpecularProperties.SpecularConstant);
        }

        public float SurfaceScale
        {
            set => SetValue((int)DistantSpecularProperties.SurfaceScale, value);
            get => GetFloatValue((int)DistantSpecularProperties.SurfaceScale);
        }

        public Vector3 Color
        {
            set => SetValue((int)DistantSpecularProperties.Color, value);
            get => GetVector3Value((int)DistantSpecularProperties.Color);
        }

        public Vector2 KernelUnitLength
        {
            set => SetValue((int)DistantSpecularProperties.KernelUnitLength, value);
            get => GetVector2Value((int)DistantSpecularProperties.KernelUnitLength);
        }

        public DistantSpecularScaleMode ScaleMode
        {
            set => SetValue((int)DistantSpecularProperties.ScaleMode, value);
            get => GetEnumValue<DistantSpecularScaleMode>((int)DistantSpecularProperties.ScaleMode);
        }
    }
}
