using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = SepiaProperties;
    public class Sepia : ID2D1Effect
    {
        public Sepia(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Sepia, this);
        }
        public float Intensity
        {
            set => SetValue((int)Props.Intensity, value);
            get => GetFloatValue((int)Props.Intensity);
        }
        public AlphaMode AlphaMode
        {
            set => SetValue((int)Props.AlphaMode, value);
            get => GetEnumValue<AlphaMode>((int)Props.AlphaMode);
        }
    }
}
