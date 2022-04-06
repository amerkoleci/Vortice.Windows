﻿// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Shadow : ID2D1Effect
{
    public Shadow(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.Shadow))
    {
    }

    public Shadow(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Shadow))
    {
    }

    public float BlurStandardDeviation
    {
        set => SetValue((int)ShadowProperties.BlurStandardDeviation, value);
        get => GetFloatValue((int)ShadowProperties.BlurStandardDeviation);
    }

    public Vector4 Color
    {
        set => SetValue((int)ShadowProperties.Color, value);
        get => GetVector4Value((int)ShadowProperties.Color);
    }

    public ShadowOptimization Optimization
    {
        set => SetValue((int)ShadowProperties.Optimization, value);
        get => GetEnumValue<ShadowOptimization>((int)ShadowProperties.Optimization);
    }
}
