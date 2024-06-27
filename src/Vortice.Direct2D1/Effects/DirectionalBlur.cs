// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class DirectionalBlur : ID2D1Effect
{
    public DirectionalBlur(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.DirectionalBlur))
    {
    }

    public DirectionalBlur(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.DirectionalBlur))
    {
    }

    public float StandardDeviation
    {
        set => SetValue((int)DirectionalBlurProperties.StandardDeviation, value);
        get => GetFloatValue((int)DirectionalBlurProperties.StandardDeviation);

    }
    public float Angle
    {
        set => SetValue((int)DirectionalBlurProperties.Angle, value);
        get => GetFloatValue((int)DirectionalBlurProperties.Angle);
    }

    public DirectionalBlurOptimization Optimization
    {
        set => SetValue((int)DirectionalBlurProperties.Optimization, value);
        get => GetEnumValue<DirectionalBlurOptimization>((int)DirectionalBlurProperties.Optimization);
    }

    public BorderMode BorderMode
    {
        set => SetValue((int)DirectionalBlurProperties.BorderMode, value);
        get => GetEnumValue<BorderMode>((int)DirectionalBlurProperties.BorderMode);
    }
}
