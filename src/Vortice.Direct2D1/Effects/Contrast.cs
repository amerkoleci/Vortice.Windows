using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = ContrastProperties;
    public class Contrast : ID2D1Effect
    {
        public Contrast(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Contrast, this);
        }
        public float Value
        {
            set => SetValue((int)Props.Contrast, value);
            get => GetFloatValue((int)Props.Contrast);
        }
        public bool ClampInput
        {
            set => SetValue((int)Props.ClampInput, value);
            get => GetBoolValue((int)Props.ClampInput);
        }
    }
}
