using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = MorphologyProperties;
    public class Morphology : ID2D1Effect
    {
        public Morphology(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Morphology, this);
        }
        public MorphologyMode Mode
        {
            set => SetValue((int)Props.Mode, value);
            get => GetEnumValue<MorphologyMode>((int)Props.Mode);
        }
        public int Width
        {
            set => SetValue((int)Props.Width, value);
            get => GetIntValue((int)Props.Width);
        }
        public int Height
        {
            set => SetValue((int)Props.Height, value);
            get => GetIntValue((int)Props.Height);
        }
    }
}
