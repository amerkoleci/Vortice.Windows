// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class YCbCr : ID2D1Effect
{
    public YCbCr(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.YCbCr))
    {
    }

    public YCbCr(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.YCbCr))
    {
    }

    public YCbCrChromaSubSampling ChromaSubsampling
    {
        set => SetValue((int)YCbCrProperties.ChromaSubsampling, value);
        get => GetEnumValue<YCbCrChromaSubSampling>((int)YCbCrProperties.ChromaSubsampling);
    }

    public Matrix3x2 TransformMatrix
    {
        set => SetValue((int)YCbCrProperties.TransformMatrix, value);
        get => GetMatrix3x2Value((int)YCbCrProperties.TransformMatrix);
    }

    public YCbCrInterpolationMode InterpolationMode
    {
        set => SetValue((int)YCbCrProperties.InterpolationMode, value);
        get => GetEnumValue<YCbCrInterpolationMode>((int)YCbCrProperties.InterpolationMode);
    }
}
