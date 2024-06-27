// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Emboss : ID2D1Effect
{
    public Emboss(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Emboss))
    {
    }

    public Emboss(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Emboss))
    {
    }

    public float Height
    {
        get => GetFloatValue((int)EmbossProperties.Height);
        set => SetValue((int)EmbossProperties.Height, value);
    }

    public float Direction
    {
        get => GetFloatValue((int)EmbossProperties.Direction);
        set => SetValue((int)EmbossProperties.Direction, value);
    }
}
