// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class Vignette : ID2D1Effect
    {
        public Vignette(ID2D1DeviceContext context)
             : base(context.CreateEffect_(EffectGuids.Vignette))
        {
        }

        public Vignette(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Vignette))
        {
        }

        public Vector3 Color
        {
            get => GetVector3Value((int)VignetteProperties.Color);
            set => SetValue((int)VignetteProperties.Color, value);
        }

        public float TransitionSize
        {
            get => GetFloatValue((int)VignetteProperties.TransitionSize);
            set => SetValue((int)VignetteProperties.TransitionSize, value);
        }

        public float Strength
        {
            get => GetFloatValue((int)VignetteProperties.Strength);
            set => SetValue((int)VignetteProperties.Strength, value);
        }

    }
}
