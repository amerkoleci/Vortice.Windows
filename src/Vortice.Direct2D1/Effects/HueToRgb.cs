// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class HueToRgb : ID2D1Effect
    {
        public HueToRgb(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.HueToRgb))
        {
        }

        public HueToRgb(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.HueToRgb))
        {
        }

        public HueToRGBInputColorSpace InputColorSpace
        {
            get => GetEnumValue<HueToRGBInputColorSpace>((int)HueToRGBProperties.InputColorSpace);
            set => SetValue((int)HueToRGBProperties.InputColorSpace, value);
        }
    }
}
