// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class ArithmeticComposite : ID2D1Effect
{
    public ArithmeticComposite(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.ArithmeticComposite))
    {
    }

    public ArithmeticComposite(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.ArithmeticComposite))
    {
    }

    public Vector4 Coefficients
    {
        set => SetValue((int)ArithmeticCompositeProperties.Coefficients, value);
        get => GetVector4Value((int)ArithmeticCompositeProperties.Coefficients);
    }

    public bool ClampOutput
    {
        set => SetValue((int)ArithmeticCompositeProperties.ClampOutput, value);
        get => GetBoolValue((int)ArithmeticCompositeProperties.ClampOutput);
    }
}
