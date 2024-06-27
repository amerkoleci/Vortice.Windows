// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Scale : ID2D1Effect
{
    public Scale(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Scale))
    {
    }

    public Scale(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Scale))
    {
    }

    public Vector2 Value
    {
        set => SetValue((int)ScaleProperties.Scale, value);
        get => GetVector2Value((int)ScaleProperties.Scale);
    }

    public Vector2 CenterPoint
    {
        set => SetValue((int)ScaleProperties.CenterPoint, value);
        get => GetVector2Value((int)ScaleProperties.CenterPoint);
    }

    public BorderMode BorderMode
    {
        set => SetValue((int)ScaleProperties.BorderMode, value);
        get => GetEnumValue<BorderMode>((int)ScaleProperties.BorderMode);
    }

    public float Sharpness
    {
        set => SetValue((int)ScaleProperties.Sharpness, value);
        get => GetFloatValue((int)ScaleProperties.Sharpness);
    }

    public ScaleInterpolationMode InterpolationMode
    {
        set => SetValue((int)ScaleProperties.InterpolationMode, value);
        get => GetEnumValue<ScaleInterpolationMode>((int)ScaleProperties.InterpolationMode);
    }
}
