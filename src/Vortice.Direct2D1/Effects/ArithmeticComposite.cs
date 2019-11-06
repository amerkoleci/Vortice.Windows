// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = ArithmeticCompositeProperties;
    public class ArithmeticComposite : ID2D1Effect
    {
        public ArithmeticComposite(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.ArithmeticComposite, this);
        }

        public Vector4 Coefficients
        {
            set => SetValue((int)Props.Coefficients, value);
            get => GetVector4Value((int)Props.Coefficients);
        }
        public bool ClampOutput
        {
            set => SetValue((int)Props.ClampOutput, value);
            get => GetBoolValue((int)Props.ClampOutput);
        }
    }
}
