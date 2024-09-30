// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Flood : ID2D1Effect
{
    public Flood(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Flood))
    {
    }

    public Flood(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Flood))
    {
    }

    public Vector4 Color
    {
        get => GetVector4Value((int)FloodProperties.Color);
        set => SetValue((int)FloodProperties.Color, value);
    }
}
