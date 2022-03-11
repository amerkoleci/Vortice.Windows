// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class AlphaMask : ID2D1Effect
{
    public AlphaMask(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.AlphaMask))
    {
    }

    public AlphaMask(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.AlphaMask))
    {
    }
}
