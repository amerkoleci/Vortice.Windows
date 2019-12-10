// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Premultiply : ID2D1Effect
    {
        public Premultiply(ID2D1DeviceContext context)
           : base(context.CreateEffect_(EffectGuids.Premultiply))
        {
        }

        public Premultiply(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Premultiply))
        {
        }
    }
}
