// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class SpotDiffuse : ID2D1Effect
{
    public SpotDiffuse(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.SpotDiffuse))
    {
    }

    public SpotDiffuse(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.SpotDiffuse))
    {
    }

    public Vector3 LightPosition
    {
        set => SetValue((int)SpotDiffuseProperties.LightPosition, value);
        get => GetVector3Value((int)SpotDiffuseProperties.LightPosition);
    }

    public Vector3 PointsAt
    {
        set => SetValue((int)SpotDiffuseProperties.PointsAt, value);
        get => GetVector3Value((int)SpotDiffuseProperties.PointsAt);
    }

    public float Focus
    {
        set => SetValue((int)SpotDiffuseProperties.Focus, value);
        get => GetFloatValue((int)SpotDiffuseProperties.Focus);
    }

    public float LimitingConeAngle
    {
        set => SetValue((int)SpotDiffuseProperties.LimitingConeAngle, value);
        get => GetFloatValue((int)SpotDiffuseProperties.LimitingConeAngle);
    }

    public float DiffuseConstant
    {
        set => SetValue((int)SpotDiffuseProperties.DiffuseConstant, value);
        get => GetFloatValue((int)SpotDiffuseProperties.DiffuseConstant);
    }

    public float SurfaceScale
    {
        set => SetValue((int)SpotDiffuseProperties.SurfaceScale, value);
        get => GetFloatValue((int)SpotDiffuseProperties.SurfaceScale);
    }

    public Vector3 Color
    {
        set => SetValue((int)SpotDiffuseProperties.Color, value);
        get => GetVector3Value((int)SpotDiffuseProperties.Color);
    }

    public Vector2 KernelUnitLength
    {
        set => SetValue((int)SpotDiffuseProperties.KernelUnitLength, value);
        get => GetVector2Value((int)SpotDiffuseProperties.KernelUnitLength);
    }

    public SpotDiffuseScaleMode ScaleMode
    {
        set => SetValue((int)SpotDiffuseProperties.ScaleMode, value);
        get => GetEnumValue<SpotDiffuseScaleMode>((int)SpotDiffuseProperties.ScaleMode);
    }
}
