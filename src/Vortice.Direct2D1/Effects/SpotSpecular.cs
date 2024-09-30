// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public class SpotSpecular : ID2D1Effect
{
    public SpotSpecular(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.SpotSpecular))
    {
    }

    public SpotSpecular(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.SpotSpecular))
    {
    }

    public Vector3 LightPosition
    {
        set => SetValue((int)SpotSpecularProperties.LightPosition, value);
        get => GetVector3Value((int)SpotSpecularProperties.LightPosition);
    }

    public Vector3 PointsAt
    {
        set => SetValue((int)SpotSpecularProperties.PointsAt, value);
        get => GetVector3Value((int)SpotSpecularProperties.PointsAt);
    }

    public float Focus
    {
        set => SetValue((int)SpotSpecularProperties.Focus, value);
        get => GetFloatValue((int)SpotSpecularProperties.Focus);
    }

    public float LimitingConeAngle
    {
        set => SetValue((int)SpotSpecularProperties.LimitingConeAngle, value);
        get => GetFloatValue((int)SpotSpecularProperties.LimitingConeAngle);
    }

    public float SpecularExponent
    {
        set => SetValue((int)SpotSpecularProperties.SpecularExponent, value);
        get => GetFloatValue((int)SpotSpecularProperties.SpecularExponent);
    }

    public float SpecularConstant
    {
        set => SetValue((int)SpotSpecularProperties.SpecularConstant, value);
        get => GetFloatValue((int)SpotSpecularProperties.SpecularConstant);
    }

    public float SurfaceScale
    {
        set => SetValue((int)SpotSpecularProperties.SurfaceScale, value);
        get => GetFloatValue((int)SpotSpecularProperties.SurfaceScale);
    }

    public Vector3 Color
    {
        set => SetValue((int)SpotSpecularProperties.Color, value);
        get => GetVector3Value((int)SpotSpecularProperties.Color);
    }

    public Vector2 KernelUnitLength
    {
        set => SetValue((int)SpotSpecularProperties.KernelUnitLength, value);
        get => GetVector2Value((int)SpotSpecularProperties.KernelUnitLength);
    }

    public SpotSpecularScaleMode ScaleMode
    {
        set => SetValue((int)SpotSpecularProperties.ScaleMode, value);
        get => GetEnumValue<SpotSpecularScaleMode>((int)SpotSpecularProperties.ScaleMode);
    }
}
