// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    public class LuminanceToAlpha : ID2D1Effect
    {
        public LuminanceToAlpha(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.LuminanceToAlpha, this);
        }
    }
}
