// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Brightness : ID2D1Effect
{
    public Brightness(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.Brightness))
    {
    }

    public Brightness(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Brightness))
    {
    }

    public Vector2 WhitePoint
    {
        set => SetValue((int)BrightnessProperties.WhitePoint, value);
        get => GetVector2Value((int)BrightnessProperties.WhitePoint);
    }

    public Vector2 BlackPoint
    {
        set => SetValue((int)BrightnessProperties.BlackPoint, value);
        get => GetVector2Value((int)BrightnessProperties.BlackPoint);
    }
}
