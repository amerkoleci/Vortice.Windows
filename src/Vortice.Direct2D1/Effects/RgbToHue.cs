// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class RgbToHue : ID2D1Effect
    {
        public RgbToHue(ID2D1DeviceContext context)
           : base(context.CreateEffect_(EffectGuids.RgbToHue))
        {
        }

        public RgbToHue(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.RgbToHue))
        {
        }

        public RGBToHueOutputColorSpace OutputColorSpace
        {
            set => SetValue((int)RGBToHueProperties.OutputColorSpace, value);
            get => GetEnumValue<RGBToHueOutputColorSpace>((int)RGBToHueProperties.OutputColorSpace);
        }
    }
}
