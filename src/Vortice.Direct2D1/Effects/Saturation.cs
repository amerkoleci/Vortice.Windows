// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = SaturationProperties;
    public class Saturation : ID2D1Effect
    {
        public Saturation(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Saturation, this);
        }
        public float Value
        {
            set => SetValue((int)Props.Saturation, value);
            get => GetFloatValue((int)Props.Saturation);
        }
    }
}
