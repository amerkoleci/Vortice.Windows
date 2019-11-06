// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = PointDiffuseProperties;
    public class PointDiffuse : ID2D1Effect
    {
        public PointDiffuse(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.PointDiffuse, this);
        }
        public Vector3 LightPosition
        {
            set => SetValue((int)Props.LightPosition, value);
            get => GetVector3Value((int)Props.LightPosition);
        }
        public float DiffuseConstant
        {
            set => SetValue((int)Props.DiffuseConstant, value);
            get => GetFloatValue((int)Props.DiffuseConstant);
        }
        public float SurfaceScale
        {
            set => SetValue((int)Props.SurfaceScale, value);
            get => GetFloatValue((int)Props.SurfaceScale);
        }
        public Vector3 Color
        {
            set => SetValue((int)Props.Color, value);
            get => GetVector3Value((int)Props.Color);
        }
        public Vector2 KernelUnitLength
        {
            set => SetValue((int)Props.KernelUnitLength, value);
            get => GetVector2Value((int)Props.KernelUnitLength);
        }
        public PointDiffuseScaleMode ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<PointDiffuseScaleMode>((int)Props.ScaleMode);
        }
    }
}
