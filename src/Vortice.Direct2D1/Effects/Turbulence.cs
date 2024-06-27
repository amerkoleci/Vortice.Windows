// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

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
        get => (int)GetUIntValue((int)TurbulenceProperties.NumOctaves);
        set => SetValue((int)TurbulenceProperties.NumOctaves, (uint)value);
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
