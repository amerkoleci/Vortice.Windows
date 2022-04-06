// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class PointDiffuse : ID2D1Effect
{
    public PointDiffuse(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.PointDiffuse))
    {
    }

    public PointDiffuse(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.PointDiffuse))
    {
    }

    public Vector3 LightPosition
    {
        get => GetVector3Value((int)PointDiffuseProperties.LightPosition);
        set => SetValue((int)PointDiffuseProperties.LightPosition, value);
    }

    public float DiffuseConstant
    {
        get => GetFloatValue((int)PointDiffuseProperties.DiffuseConstant);
        set => SetValue((int)PointDiffuseProperties.DiffuseConstant, value);
    }

    public float SurfaceScale
    {
        get => GetFloatValue((int)PointDiffuseProperties.SurfaceScale);
        set => SetValue((int)PointDiffuseProperties.SurfaceScale, value);
    }

    public Vector3 Color
    {
        get => GetVector3Value((int)PointDiffuseProperties.Color);
        set => SetValue((int)PointDiffuseProperties.Color, value);
    }

    public Vector2 KernelUnitLength
    {
        get => GetVector2Value((int)PointDiffuseProperties.KernelUnitLength);
        set => SetValue((int)PointDiffuseProperties.KernelUnitLength, value);
    }

    public PointDiffuseScaleMode ScaleMode
    {
        get => GetEnumValue<PointDiffuseScaleMode>((int)PointDiffuseProperties.ScaleMode);
        set => SetValue((int)PointDiffuseProperties.ScaleMode, value);
    }
}
