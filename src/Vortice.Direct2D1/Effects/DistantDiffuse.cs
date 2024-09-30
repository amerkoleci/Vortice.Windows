// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class DistantDiffuse : ID2D1Effect
{
    public DistantDiffuse(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.DistantDiffuse))
    {
    }

    public DistantDiffuse(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.DistantDiffuse))
    {
    }

    public float Azimuth
    {
        set => SetValue((int)DistantDiffuseProperties.Azimuth, value);
        get => GetFloatValue((int)DistantDiffuseProperties.Azimuth);
    }

    public float Elevation
    {
        set => SetValue((int)DistantDiffuseProperties.Elevation, value);
        get => GetFloatValue((int)DistantDiffuseProperties.Elevation);
    }

    public float DiffuseConstant
    {
        set => SetValue((int)DistantDiffuseProperties.DiffuseConstant, value);
        get => GetFloatValue((int)DistantDiffuseProperties.DiffuseConstant);
    }

    public float SurfaceScale
    {
        set => SetValue((int)DistantDiffuseProperties.SurfaceScale, value);
        get => GetFloatValue((int)DistantDiffuseProperties.SurfaceScale);
    }

    public Vector3 Color
    {
        set => SetValue((int)DistantDiffuseProperties.Color, value);
        get => GetVector3Value((int)DistantDiffuseProperties.Color);
    }

    public Vector2 KernelUnitLength
    {
        set => SetValue((int)DistantDiffuseProperties.KernelUnitLength, value);
        get => GetVector2Value((int)DistantDiffuseProperties.KernelUnitLength);
    }

    public DistantDiffuseScaleMode ScaleMode
    {
        set => SetValue((int)DistantDiffuseProperties.ScaleMode, value);
        get => GetEnumValue<DistantDiffuseScaleMode>((int)DistantDiffuseProperties.ScaleMode);
    }
}
