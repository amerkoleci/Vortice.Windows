// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Opacity : ID2D1Effect
    {
        public Opacity(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.Opacity))
        {
        }

        public Opacity(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Opacity))
        {
        }

        public float Value
        {
            get => GetFloatValue((int)OpacityProperties.Opacity);
            set => SetValue((int)OpacityProperties.Opacity, value);
        }
    }
}
