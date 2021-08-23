// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Exposure : ID2D1Effect
    {
        public Exposure(ID2D1DeviceContext context)
             : base(context.CreateEffect(EffectGuids.Exposure))
        {
        }

        public Exposure(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Exposure))
        {
        }

        public float Value
        {
            get => GetFloatValue((int)ExposureProperties.ExposureValue);
            set => SetValue((int)ExposureProperties.ExposureValue, value);
        }
    }
}
