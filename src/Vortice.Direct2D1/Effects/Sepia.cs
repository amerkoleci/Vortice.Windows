// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public class Sepia : ID2D1Effect
    {
        public Sepia(ID2D1DeviceContext context)
           : base(context.CreateEffect(EffectGuids.Sepia))
        {
        }

        public Sepia(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Sepia))
        {
        }

        public float Intensity
        {
            set => SetValue((int)SepiaProperties.Intensity, value);
            get => GetFloatValue((int)SepiaProperties.Intensity);
        }

        public AlphaMode AlphaMode
        {
            set => SetValue((int)SepiaProperties.AlphaMode, value);
            get => GetEnumValue<AlphaMode>((int)SepiaProperties.AlphaMode);
        }
    }
}
