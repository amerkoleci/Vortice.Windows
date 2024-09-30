// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class AffineTransform2D : ID2D1Effect
{
    public AffineTransform2D(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.AffineTransform2D))
    {
    }

    public AffineTransform2D(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.AffineTransform2D))
    {
    }

    public AffineTransform2DInterpolationMode InterPolationMode
    {
        get => GetEnumValue<AffineTransform2DInterpolationMode>((int)AffineTransform2DProperties.InterpolationMode);
        set => SetValue((int)AffineTransform2DProperties.InterpolationMode, value);
    }
    public BorderMode BorderMode
    {
        get => GetEnumValue<BorderMode>((uint)AffineTransform2DProperties.BorderMode);
        set => SetValue((int)AffineTransform2DProperties.BorderMode, value);
    }

    public Matrix3x2 TransformMatrix
    {
        set => SetValue((uint)AffineTransform2DProperties.TransformMatrix, value);
        get => GetMatrix3x2Value((uint)AffineTransform2DProperties.TransformMatrix);
    }

    public float Sharpness
    {
        set => SetValue((int)AffineTransform2DProperties.Sharpness, value);
        get => GetFloatValue((int)AffineTransform2DProperties.Sharpness);
    }
}
