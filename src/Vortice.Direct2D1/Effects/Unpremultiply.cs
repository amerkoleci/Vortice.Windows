// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Unpremultiply : ID2D1Effect
{
    public Unpremultiply(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.UnPremultiply))
    {
    }

    public Unpremultiply(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.UnPremultiply))
    {
    }
}
