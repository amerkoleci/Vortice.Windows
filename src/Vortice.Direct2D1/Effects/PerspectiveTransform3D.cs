// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class PerspectiveTransform3D : ID2D1Effect
{
    public PerspectiveTransform3D(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.PerspectiveTransform3D))
    {
    }

    public PerspectiveTransform3D(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.PerspectiveTransform3D))
    {
    }


    public PerspectiveTransform3DInteroplationMode InterpolationMode
    {
        get => GetEnumValue<PerspectiveTransform3DInteroplationMode>((int)PerspectiveTransform3DProperties.InterpolationMode);
        set => SetValue((int)PerspectiveTransform3DProperties.InterpolationMode, value);
    }

    public BorderMode BorderMode
    {
        get => GetEnumValue<BorderMode>((int)PerspectiveTransform3DProperties.BorderMode);
        set => SetValue((int)PerspectiveTransform3DProperties.BorderMode, value);
    }

    public float Depth
    {
        get => GetFloatValue((int)PerspectiveTransform3DProperties.Depth);
        set => SetValue((int)PerspectiveTransform3DProperties.Depth, value);
    }

    public Vector2 PerspectiveOrigin
    {
        get => GetVector2Value((int)PerspectiveTransform3DProperties.PerspectiveOrigin);
        set => SetValue((int)PerspectiveTransform3DProperties.PerspectiveOrigin, value);
    }

    public Vector3 LocalOffset
    {
        get => GetVector3Value((int)PerspectiveTransform3DProperties.LocalOffset);
        set => SetValue((int)PerspectiveTransform3DProperties.LocalOffset, value);
    }

    public Vector3 GlobalOffset
    {
        get => GetVector3Value((int)PerspectiveTransform3DProperties.GlobalOffset);
        set => SetValue((int)PerspectiveTransform3DProperties.GlobalOffset, value);
    }

    public Vector3 RotationOrigin
    {
        get => GetVector3Value((int)PerspectiveTransform3DProperties.RotationOrigin);
        set => SetValue((int)PerspectiveTransform3DProperties.RotationOrigin, value);
    }

    public Vector3 Rotation
    {
        get => GetVector3Value((int)PerspectiveTransform3DProperties.Rotation);
        set => SetValue((int)PerspectiveTransform3DProperties.Rotation, value);
    }
}
