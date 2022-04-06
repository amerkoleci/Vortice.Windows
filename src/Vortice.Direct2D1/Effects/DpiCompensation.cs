// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class DpiCompensation : ID2D1Effect
{
    public DpiCompensation(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.DpiCompensation))
    {
    }

    public DpiCompensation(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.DpiCompensation))
    {
    }

    public DpiCompensationInterpolationMode InterpolationMode
    {
        get => GetEnumValue<DpiCompensationInterpolationMode>((int)DpiCompensationProperties.InterpolationMode);
        set => SetValue((int)DpiCompensationProperties.InterpolationMode, value);
    }

    public BorderMode BorderMode
    {
        get => GetEnumValue<BorderMode>((int)DpiCompensationProperties.BorderMode);
        set => SetValue((int)DpiCompensationProperties.BorderMode, value);
    }

    public float InputDpi
    {
        get => GetFloatValue((int)DpiCompensationProperties.InputDpi);
        set => SetValue((int)DpiCompensationProperties.InputDpi, value);
    }
}
