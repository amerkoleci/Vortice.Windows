// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class CrossFade : ID2D1Effect
    {
        public CrossFade(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.CrossFade))
        {
        }

        public CrossFade(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.CrossFade))
        {
        }

        public float Weight
        {
            set => SetValue((int)CrossFadeProperties.Weight, value);
            get => GetFloatValue((int)CrossFadeProperties.Weight);
        }
    }
}
