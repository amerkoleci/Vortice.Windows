// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = PerspectiveTransform3DProperties;
    public class PerspectiveTransform3D : ID2D1Effect
    {
        public PerspectiveTransform3D(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.PerspectiveTransform3D, this);
        }


        public PerspectiveTransform3DInteroplationMode InterpolationMode
        {
            set => SetValue((int)Props.InterpolationMode, value);
            get => GetEnumValue<PerspectiveTransform3DInteroplationMode>((int)Props.InterpolationMode);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
        public float Depth
        {
            set => SetValue((int)Props.Depth, value);
            get => GetFloatValue((int)Props.Depth);
        }
        public Vector2 PerspectiveOrigin
        {
            set => SetValue((int)Props.PerspectiveOrigin, value);
            get => GetVector2Value((int)Props.PerspectiveOrigin);
        }
        public Vector3 LocalOffset
        {
            set => SetValue((int)Props.LocalOffset, value);
            get => GetVector3Value((int)Props.LocalOffset);
        }
        public Vector3 GlobalOffset
        {
            set => SetValue((int)Props.GlobalOffset, value);
            get => GetVector3Value((int)Props.GlobalOffset);
        }
        public Vector3 RotationOrigin
        {
            set => SetValue((int)Props.RotationOrigin, value);
            get => GetVector3Value((int)Props.RotationOrigin);
        }
        public Vector3 Rotation
        {
            set => SetValue((int)Props.Rotation, value);
            get => GetVector3Value((int)Props.Rotation);
        }

    }
}
