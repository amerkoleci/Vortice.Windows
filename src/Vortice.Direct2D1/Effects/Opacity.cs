using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = OpacityProperties;
    public class Opacity : ID2D1Effect
    {
        public Opacity(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Opacity, this);
        }
        public float Value
        {
            set => SetValue((int)Props.Opacity, value);
            get => GetFloatValue((int)Props.Opacity);
        }
    }
}
