using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = RGBToHueProperties;
    public class RgbToHue : ID2D1Effect
    {
        public RgbToHue(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.RgbToHue, this);
        }

        public RGBToHueOutputColorSpace OutputColorSpace
        {
            set => SetValue((int)Props.OutputColorSpace, value);
            get => GetEnumValue<RGBToHueOutputColorSpace>((int)Props.OutputColorSpace);
        }
    }
}
