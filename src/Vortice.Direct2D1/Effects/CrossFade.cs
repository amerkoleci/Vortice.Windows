// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = CrossFadeProperties;
    public class CrossFade : ID2D1Effect
    {
        public CrossFade(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.CrossFade, this);
        }

        public float Weight
        {
            set => SetValue((int)Props.Weight, value);
            get => GetFloatValue((int)Props.Weight);
        }
    }
}
