using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = SpotSpecularProperties;
    public class SpotSpecular : ID2D1Effect
    {
        public SpotSpecular(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.SpotSpecular, this);
        }
        public Vector3 LightPosition
        {
            set => SetValue((int)Props.LightPosition, value);
            get => GetVector3Value((int)Props.LightPosition);
        }
        public Vector3 PointsAt
        {
            set => SetValue((int)Props.PointsAt, value);
            get => GetVector3Value((int)Props.PointsAt);
        }
        public float Focus
        {
            set => SetValue((int)Props.Focus, value);
            get => GetFloatValue((int)Props.Focus);
        }
        public float LimitingConeAngle
        {
            set => SetValue((int)Props.LimitingConeAngle, value);
            get => GetFloatValue((int)Props.LimitingConeAngle);
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
        public SpotSpecularScaleMode ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<SpotSpecularScaleMode>((int)Props.ScaleMode);
        }
    }
}
