using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = DirectionalBlurProperties;
    public class DirectionalBlur : ID2D1Effect
    {
        public DirectionalBlur(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.DirectionalBlur, this);
        }
        public float StandardDeviation
        {
            set => SetValue((int)Props.StandardDeviation, value);
            get => GetFloatValue((int)Props.StandardDeviation);

        }
        public float Angle
        {
            set => SetValue((int)Props.Angle, value);
            get => GetFloatValue((int)Props.Angle);
        }
        public DirectionalBlurOptimization Optimization
        {
            set => SetValue((int)Props.Optimization, value);
            get => GetEnumValue<DirectionalBlurOptimization>((int)Props.Optimization);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
    }
}
