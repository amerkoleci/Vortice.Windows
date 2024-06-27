// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Opacity : ID2D1Effect
{
    public Opacity(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Opacity))
    {
    }

    public Opacity(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Opacity))
    {
    }

    public float Value
    {
        get => GetFloatValue((int)OpacityProperties.Opacity);
        set => SetValue((int)OpacityProperties.Opacity, value);
    }
}
