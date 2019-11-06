// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = SharpenProperties;
    public class Sharpen : ID2D1Effect
    {
        public Sharpen(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Sharpen, this);
        }

        public float Sharpness
        {
            set => SetValue((int)Props.Sharpness, value);
            get => GetFloatValue((int)Props.Sharpness);
        }
        public float Threshold
        {
            set => SetValue((int)Props.Threshold, value);
            get => GetFloatValue((int)Props.Threshold);
        }

    }
}
