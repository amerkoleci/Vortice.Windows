// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = GaussianBlurProperties;
    public class GaussianBlur : ID2D1Effect
    {
        public GaussianBlur(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.GaussianBlur, this);
        }

        public float StandardDeviation
        {
            set => SetValue((int)Props.StandardDeviation, value);
            get => GetFloatValue((int)Props.StandardDeviation);
        }

        public GaussianBlurOptimization Optimization
        {
            set => SetValue((int)Props.Optimization, value);
            get => GetEnumValue<GaussianBlurOptimization>((int)Props.Optimization);
        }

        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
    }
}
