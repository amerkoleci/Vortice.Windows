using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = TurbulenceProperties;
    public class Turbulence : ID2D1Effect
    {
        public Turbulence(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Turbulence, this);
        }

        public Vector2 Offset
        {
            set => SetValue((int)Props.Offset, value);
            get => GetVector2Value((int)Props.Offset);
        }
        public Vector2 Size
        {
            set => SetValue((int)Props.Size, value);
            get => GetVector2Value((int)Props.Size);
        }
        public Vector2 BaseFrequency
        {
            set => SetValue((int)Props.BaseFrequency, value);
            get => GetVector2Value((int)Props.BaseFrequency);
        }
        public uint NumOctaves
        {
            set => SetValue((int)Props.NumOctaves, value);
            get => GetUintValue((int)Props.NumOctaves);
        }
        public uint Seed
        {
            set => SetValue((int)Props.Seed, value);
            get => GetUintValue((int)Props.Seed);
        }
        public TurbulenceNoise Noise
        {
            set => SetValue((int)Props.Noise, value);
            get => GetEnumValue<TurbulenceNoise>((int)Props.Noise);
        }
        public bool Stitchable
        {
            set => SetValue((int)Props.Stitchable, value);
            get => GetBoolValue((int)Props.Stitchable);
        }
    }
}
