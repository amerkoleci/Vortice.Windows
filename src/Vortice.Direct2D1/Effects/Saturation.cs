// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

using Props = SaturationProperties;
public sealed class Saturation : ID2D1Effect
{
    public Saturation(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Saturation))
    {
    }

    public Saturation(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Saturation))
    {
    }

    public float Value
    {
        set => SetValue((int)Props.Saturation, value);
        get => GetFloatValue((int)Props.Saturation);
    }
}
