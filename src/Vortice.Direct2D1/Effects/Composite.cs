using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = CompositeProperties;
    public class Composite : ID2D1Effect
    {
        public Composite(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Composite, this);
        }

        public CompositeMode Mode
        {
            set => SetValue((int)Props.Mode, value);
            get => GetEnumValue<CompositeMode>((int)Props.Mode);
        }
    }
}
