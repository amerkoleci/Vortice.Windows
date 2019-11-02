using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = EmbossProperties;
    public class Emboss : ID2D1Effect
    {
        public Emboss(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Emboss, this);
        }
        public float Height
        {
            set => SetValue((int)Props.Height, value);
            get => GetFloatValue((int)Props.Height);
        }
        public float Direction
        {
            set => SetValue((int)Props.Direction, value);
            get => GetFloatValue((int)Props.Direction);
        }
    }
}
