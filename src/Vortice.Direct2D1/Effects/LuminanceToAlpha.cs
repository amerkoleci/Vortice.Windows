// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class LuminanceToAlpha : ID2D1Effect
    {
        public LuminanceToAlpha(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.LuminanceToAlpha))
        {
        }

        public LuminanceToAlpha(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.LuminanceToAlpha))
        {
        }
    }
}
