// Copyright © Amer Koleci and Contributors.
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
        set => SetValue((int)AffineTransform2DProperties.InterpolationMode, value);
        get => GetEnumValue<AffineTransform2DInterpolationMode>((int)AffineTransform2DProperties.InterpolationMode);
    }
    public BorderMode BorderMode
    {
        set => SetValue((int)AffineTransform2DProperties.BorderMode, value);
        get => GetEnumValue<BorderMode>((int)AffineTransform2DProperties.BorderMode);
    }

    public Matrix3x2 TransformMatrix
    {
        set => SetValue((int)AffineTransform2DProperties.TransformMatrix, value);
        get => GetMatrix3x2Value((int)AffineTransform2DProperties.TransformMatrix);
    }

    public float Sharpness
    {
        set => SetValue((int)AffineTransform2DProperties.Sharpness, value);
        get => GetFloatValue((int)AffineTransform2DProperties.Sharpness);
    }
}
