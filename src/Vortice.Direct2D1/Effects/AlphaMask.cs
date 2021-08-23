// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    public sealed class AlphaMask : ID2D1Effect
    {
        public AlphaMask(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.AlphaMask))
        {
        }

        public AlphaMask(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.AlphaMask))
        {
        }
    }
}
