// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class DisplacementMap : ID2D1Effect
{
    public DisplacementMap(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.DisplacementMap))
    {
    }

    public DisplacementMap(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.DisplacementMap))
    {
    }

    public float Scale
    {
        set => SetValue((int)DisplacementMapProperties.Scale, value);
        get => GetFloatValue((int)DisplacementMapProperties.Scale);
    }

    public ChannelSelector XChannelSelect
    {
        set => SetValue((int)DisplacementMapProperties.XChannelSelect, value);
        get => GetEnumValue<ChannelSelector>((int)DisplacementMapProperties.XChannelSelect);
    }

    public ChannelSelector YChannelSelect
    {
        set => SetValue((int)DisplacementMapProperties.YChannelSelect, value);
        get => GetEnumValue<ChannelSelector>((int)DisplacementMapProperties.YChannelSelect);
    }
}
