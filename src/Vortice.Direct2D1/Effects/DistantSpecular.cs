// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = DistantSpecularProperties;
    public class DistantSpecular : ID2D1Effect
    {
        public DistantSpecular(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.DistantSpecular, this);
        }

        public float Azimuth
        {
            set => SetValue((int)Props.Azimuth, value);
            get => GetFloatValue((int)Props.Azimuth);
        }
        public float Elevation
        {
            set => SetValue((int)Props.Elevation, value);
            get => GetFloatValue((int)Props.Elevation);
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
        public DistantSpecularScaleMode ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<DistantSpecularScaleMode>((int)Props.ScaleMode);
        }
    }
}
