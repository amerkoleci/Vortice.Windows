using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = ExposureProperties;
    public class Exposure : ID2D1Effect
    {
        public Exposure(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Exposure, this);
        }
        public float Value
        {
            set => SetValue((int)Props.ExposureValue, value);
            get => GetFloatValue((int)Props.ExposureValue);
        }
    }
}
