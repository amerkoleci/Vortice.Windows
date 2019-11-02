using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = TemperatureAndTintProperties;
    public class TemperatureAndTint : ID2D1Effect
    {
        public TemperatureAndTint(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.TemperatureTint, this);
        }
        public float Temperature
        {
            set => SetValue((int)Props.Temperature, value);
            get => GetFloatValue((int)Props.Temperature);
        }
        public float Tint
        {
            set => SetValue((int)Props.Tint, value);
            get => GetFloatValue((int)Props.Tint);
        }
    }
}
