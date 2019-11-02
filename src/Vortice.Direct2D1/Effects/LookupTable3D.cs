using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = LookupTable3DProperties;
    public class LookupTable3D : ID2D1Effect
    {
        public LookupTable3D(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.LookupTable3D, this);
        }

        public ID2D1LookupTable3D LUT
        {
            set => SetValue((int)Props.Lut, value);
            get => GetIUnknownValue<ID2D1LookupTable3D>((int)Props.Lut);
        }
        public AlphaMode AlphaMode
        {
            set => SetValue((int)Props.AlphaMode, value);
            get => GetEnumValue<AlphaMode>((int)Props.Lut);
        }
    }
}
