// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Sharpen : ID2D1Effect
{
    public Sharpen(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.Sharpen))
    {
    }

    public Sharpen(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Sharpen))
    {
    }

    public float Sharpness
    {
        set => SetValue((int)SharpenProperties.Sharpness, value);
        get => GetFloatValue((int)SharpenProperties.Sharpness);
    }

    public float Threshold
    {
        set => SetValue((int)SharpenProperties.Threshold, value);
        get => GetFloatValue((int)SharpenProperties.Threshold);
    }
}
