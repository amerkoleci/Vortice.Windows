// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = BrightnessProperties;
    public class Brightness : ID2D1Effect
    {
        public Brightness(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Brightness, this);
        }

        public Vector2 WhitePoint
        {
            set => SetValue((int)Props.WhitePoint, value);
            get => GetVector2Value((int)Props.WhitePoint);
        }

        public Vector2 BlackPoint
        {
            set => SetValue((int)Props.BlackPoint, value);
            get => GetVector2Value((int)Props.BlackPoint);
        }
    }
}
