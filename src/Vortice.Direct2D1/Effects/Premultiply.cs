// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Premultiply : ID2D1Effect
{
    public Premultiply(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.Premultiply))
    {
    }

    public Premultiply(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Premultiply))
    {
    }
}
