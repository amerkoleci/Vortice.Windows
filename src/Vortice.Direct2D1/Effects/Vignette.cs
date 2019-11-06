// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = VignetteProperties;
    public class Vignette : ID2D1Effect
    {
        public Vignette(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Vignette, this);
        }
        public Vector3 Color
        {
            set => SetValue((int)Props.Color, value);
            get => GetVector3Value((int)Props.Color);
        }
        public float TransitionSize
        {
            set => SetValue((int)Props.TransitionSize, value);
            get => GetFloatValue((int)Props.TransitionSize);
        }
        public float Strength
        {
            set => SetValue((int)Props.Strength, value);
            get => GetFloatValue((int)Props.Strength);
        }

    }
}
