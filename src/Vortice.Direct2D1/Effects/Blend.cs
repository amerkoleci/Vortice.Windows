// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Blend : ID2D1Effect
    {
        public Blend(ID2D1DeviceContext context)
           : base(context.CreateEffect(EffectGuids.Blend))
        {
        }

        public Blend(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Blend))
        {
        }

        public BlendMode Mode
        {
            set => SetValue((int)BlendProperties.Mode, value);
            get => GetEnumValue<BlendMode>((int)BlendProperties.Mode);
        }
    }
}
