// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class LuminanceToAlpha : ID2D1Effect
{
    public LuminanceToAlpha(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.LuminanceToAlpha))
    {
    }

    public LuminanceToAlpha(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.LuminanceToAlpha))
    {
    }
}
