// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Transform3D : ID2D1Effect
{
    public Transform3D(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Transform3D))
    {
        
    }

    public Transform3D(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Transform3D))
    {
    }

    public Transform3DInterpolationMode InterPolationMode
    {
        set => SetValue((int)Transform3DProperties.InterpolationMode, value);
        get => GetEnumValue<Transform3DInterpolationMode>((int)Transform3DProperties.InterpolationMode);
    }
    public BorderMode BorderMode
    {
        set => SetValue((int)Transform3DProperties.BorderMode, value);
        get => GetEnumValue<BorderMode>((int)Transform3DProperties.BorderMode);
    }
    public Matrix4x4 TransformMatrix
    {
        set => SetValue((int)Transform3DProperties.TransformMatrix, value);
        get => GetMatrix4x4Value((int)Transform3DProperties.TransformMatrix);
    }
}
