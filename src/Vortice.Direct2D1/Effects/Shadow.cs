// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = ShadowProperties;
    public class Shadow : ID2D1Effect
    {
        public Shadow(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Shadow, this);
        }

        public float BlurStandardDeviation
        {
            set => SetValue((int)Props.BlurStandardDeviation, value);
            get => GetFloatValue((int)Props.BlurStandardDeviation);
        }
        public Vector4 Color
        {
            set => SetValue((int)Props.Color, value);
            get => GetVector4Value((int)Props.Color);
        }
        public ShadowOptimization Optimization
        {
            set => SetValue((int)Props.Optimization, value);
            get => GetEnumValue<ShadowOptimization>((int)Props.Optimization);
        }
    }
}
