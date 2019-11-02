using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = HDRToneMapProperties;
    public class HdrToneMap : ID2D1Effect
    {
        public HdrToneMap(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.HdrToneMap, this);
        }

        public float InputMaxLuminance
        {
            set => SetValue((int)Props.InputMaxLuminance, value);
            get => GetFloatValue((int)Props.InputMaxLuminance);
        }
        public float OutputMaxLuminance
        {
            set => SetValue((int)Props.OutputMaxLuminance, value);
            get => GetFloatValue((int)Props.OutputMaxLuminance);
        }
        public HDRToneMapDisplayMode DisplayMode
        {
            set => SetValue((int)Props.DisplayMode, value);
            get => GetEnumValue<HDRToneMapDisplayMode>((int)Props.DisplayMode);
        }
    }
}
