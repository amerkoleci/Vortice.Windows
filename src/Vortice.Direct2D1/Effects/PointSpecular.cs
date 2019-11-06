// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = PointSpecularProperties;
    public class PointSpecular : ID2D1Effect
    {
        public PointSpecular(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.PointSpecular, this);
        }
        public Vector3 LightPosition
        {
            set => SetValue((int)Props.LightPosition, value);
            get => GetVector3Value((int)Props.LightPosition);
        }
        public float SpecularExponent
        {
            set => SetValue((int)Props.SpecularExponent, value);
            get => GetFloatValue((int)Props.SpecularExponent);
        }
        public float SpecularConstant
        {
            set => SetValue((int)Props.SpecularConstant, value);
            get => GetFloatValue((int)Props.SpecularConstant);
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
        public PointSpecularScaleMode ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<PointSpecularScaleMode>((int)Props.ScaleMode);
        }
    }
}
