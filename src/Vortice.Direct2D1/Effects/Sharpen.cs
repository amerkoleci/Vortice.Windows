// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Sharpen : ID2D1Effect
    {
        public Sharpen(ID2D1DeviceContext context)
           : base(context.CreateEffect_(EffectGuids.Sharpen))
        {
        }

        public Sharpen(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Sharpen))
        {
        }

        public float Sharpness
        {
            set => SetValue((int)SharpenProperties.Sharpness, value);
            get => GetFloatValue((int)SharpenProperties.Sharpness);
        }

        public float Threshold
        {
            set => SetValue((int)SharpenProperties.Threshold, value);
            get => GetFloatValue((int)SharpenProperties.Threshold);
        }
    }
}
