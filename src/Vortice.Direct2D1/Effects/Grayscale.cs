// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Grayscale : ID2D1Effect
{
    public Grayscale(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Grayscale))
    {
    }

    public Grayscale(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Grayscale))
    {
    }
}
