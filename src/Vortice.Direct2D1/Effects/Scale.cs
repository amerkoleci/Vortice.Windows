using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = ScaleProperties;
    public class Scale : ID2D1Effect
    {
        public Scale(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Scale, this);
        }

        public Vector2 Value
        {
            set => SetValue((int)Props.Scale, value);
            get => GetVector2Value((int)Props.Scale);
        }
        public Vector2 CenterPoint
        {
            set => SetValue((int)Props.CenterPoint, value);
            get => GetVector2Value((int)Props.CenterPoint);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
        public float Sharpness
        {
            set => SetValue((int)Props.Sharpness, value);
            get => GetFloatValue((int)Props.Sharpness);
        }
        public ScaleInterpolationMode InterpolationMode
        {
            set => SetValue((int)Props.InterpolationMode, value);
            get => GetEnumValue<ScaleInterpolationMode>((int)Props.InterpolationMode);
        }
    }
}
