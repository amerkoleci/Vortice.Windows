// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class Turbulence : ID2D1Effect
    {
        public Turbulence(ID2D1DeviceContext context)
           : base(context.CreateEffect(EffectGuids.Turbulence))
        {
        }

        public Turbulence(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Turbulence))
        {
        }

        public Vector2 Offset
        {
            set => SetValue((int)TurbulenceProperties.Offset, value);
            get => GetVector2Value((int)TurbulenceProperties.Offset);
        }

        public Vector2 Size
        {
            set => SetValue((int)TurbulenceProperties.Size, value);
            get => GetVector2Value((int)TurbulenceProperties.Size);
        }

        public Vector2 BaseFrequency
        {
            set => SetValue((int)TurbulenceProperties.BaseFrequency, value);
            get => GetVector2Value((int)TurbulenceProperties.BaseFrequency);
        }

        public int NumOctaves
        {
            set => SetValue((int)TurbulenceProperties.NumOctaves, value);
            get => GetIntValue((int)TurbulenceProperties.NumOctaves);
        }

        public int Seed
        {
            set => SetValue((int)TurbulenceProperties.Seed, value);
            get => GetIntValue((int)TurbulenceProperties.Seed);
        }

        public TurbulenceNoise Noise
        {
            set => SetValue((int)TurbulenceProperties.Noise, value);
            get => GetEnumValue<TurbulenceNoise>((int)TurbulenceProperties.Noise);
        }

        public bool Stitchable
        {
            set => SetValue((int)TurbulenceProperties.Stitchable, value);
            get => GetBoolValue((int)TurbulenceProperties.Stitchable);
        }
    }
}
