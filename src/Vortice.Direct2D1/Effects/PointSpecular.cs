// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class PointSpecular : ID2D1Effect
    {
        public PointSpecular(ID2D1DeviceContext context)
             : base(context.CreateEffect(EffectGuids.PointSpecular))
        {
        }

        public PointSpecular(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.PointSpecular))
        {
        }

        public Vector3 LightPosition
        {
            get => GetVector3Value((int)PointSpecularProperties.LightPosition);
            set => SetValue((int)PointSpecularProperties.LightPosition, value);
        }

        public float SpecularExponent
        {
            get => GetFloatValue((int)PointSpecularProperties.SpecularExponent);
            set => SetValue((int)PointSpecularProperties.SpecularExponent, value);
        }

        public float SpecularConstant
        {
            get => GetFloatValue((int)PointSpecularProperties.SpecularConstant);
            set => SetValue((int)PointSpecularProperties.SpecularConstant, value);
        }

        public float SurfaceScale
        {
            get => GetFloatValue((int)PointSpecularProperties.SurfaceScale);
            set => SetValue((int)PointSpecularProperties.SurfaceScale, value);
        }

        public Vector3 Color
        {
            get => GetVector3Value((int)PointSpecularProperties.Color);
            set => SetValue((int)PointSpecularProperties.Color, value);
        }

        public Vector2 KernelUnitLength
        {
            get => GetVector2Value((int)PointSpecularProperties.KernelUnitLength);
            set => SetValue((int)PointSpecularProperties.KernelUnitLength, value);
        }

        public PointSpecularScaleMode ScaleMode
        {
            get => GetEnumValue<PointSpecularScaleMode>((int)PointSpecularProperties.ScaleMode);
            set => SetValue((int)PointSpecularProperties.ScaleMode, value);
        }
    }
}
