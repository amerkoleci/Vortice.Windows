// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Composite : ID2D1Effect
{
    public Composite(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Composite))
    {
    }

    public Composite(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Composite))
    {
    }

    public CompositeMode Mode
    {
        set => SetValue((int)CompositeProperties.Mode, value);
        get => GetEnumValue<CompositeMode>((int)CompositeProperties.Mode);
    }
}
