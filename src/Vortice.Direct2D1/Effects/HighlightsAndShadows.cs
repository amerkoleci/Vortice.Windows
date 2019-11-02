using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = HighlightsAndShadowsProperties;
    public class HighlightsAndShadows : ID2D1Effect
    {
        public HighlightsAndShadows(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.HighlightsShadows, this);
        }
        public float Highlights
        {
            set => SetValue((int)Props.Highlights, value);
            get => GetFloatValue((int)Props.Highlights);
        }
        public float Shadows
        {
            set => SetValue((int)Props.Shadows, value);
            get => GetFloatValue((int)Props.Shadows);
        }
        public float Clarity
        {
            set => SetValue((int)Props.Clarity, value);
            get => GetFloatValue((int)Props.Clarity);
        }
        public HighlightsAndShadowsInputGamma InputGamma
        {
            set => SetValue((int)Props.InputGamma, value);
            get => GetEnumValue<HighlightsAndShadowsInputGamma>((int)Props.InputGamma);
        }
        public float MaskBlurRadius
        {
            set => SetValue((int)Props.MaskBlurRadius, value);
            get => GetFloatValue((int)Props.MaskBlurRadius);
        }

    }
}
