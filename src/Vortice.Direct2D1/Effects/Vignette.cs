// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Vignette : ID2D1Effect
{
    public Vignette(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Vignette))
    {
    }

    public Vignette(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Vignette))
    {
    }

    public Vector4 Color
    {
        get => GetVector4Value((int)VignetteProperties.Color);
        set => SetValue((int)VignetteProperties.Color, value);
    }

    public float TransitionSize
    {
        get => GetFloatValue((int)VignetteProperties.TransitionSize);
        set => SetValue((int)VignetteProperties.TransitionSize, value);
    }

    public float Strength
    {
        get => GetFloatValue((int)VignetteProperties.Strength);
        set => SetValue((int)VignetteProperties.Strength, value);
    }

}
