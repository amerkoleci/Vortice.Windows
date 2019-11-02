using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = PosterizeProperties;
    public class Posterize : ID2D1Effect
    {
        public Posterize(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Posterize, this);
        }
        public int RedValueCount
        {
            set => SetValue((int)Props.RedValueCount, value);
            get => GetIntValue((int)Props.RedValueCount);
        }
        public int GreenValueCount
        {
            set => SetValue((int)Props.GreenValueCount, value);
            get => GetIntValue((int)Props.GreenValueCount);
        }
        public int BlueValueCount
        {
            set => SetValue((int)Props.BlueValueCount, value);
            get => GetIntValue((int)Props.BlueValueCount);
        }
    }
}
