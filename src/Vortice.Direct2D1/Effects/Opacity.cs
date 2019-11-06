// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = OpacityProperties;
    public class Opacity : ID2D1Effect
    {
        public Opacity(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Opacity, this);
        }
        public float Value
        {
            set => SetValue((int)Props.Opacity, value);
            get => GetFloatValue((int)Props.Opacity);
        }
    }
}
