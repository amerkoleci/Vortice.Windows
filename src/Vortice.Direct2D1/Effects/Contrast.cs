// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Contrast : ID2D1Effect
{
    public Contrast(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Contrast))
    {
    }

    public Contrast(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Contrast))
    {
    }

    public float Value
    {
        set => SetValue((int)ContrastProperties.Contrast, value);
        get => GetFloatValue((int)ContrastProperties.Contrast);
    }

    public bool ClampInput
    {
        set => SetValue((int)ContrastProperties.ClampInput, value);
        get => GetBoolValue((int)ContrastProperties.ClampInput);
    }
}
