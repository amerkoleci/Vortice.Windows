// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class ArithmeticComposite : ID2D1Effect
    {
        public ArithmeticComposite(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.ArithmeticComposite))
        {
        }

        public ArithmeticComposite(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.ArithmeticComposite))
        {
        }

        public Vector4 Coefficients
        {
            set => SetValue((int)ArithmeticCompositeProperties.Coefficients, value);
            get => GetVector4Value((int)ArithmeticCompositeProperties.Coefficients);
        }

        public bool ClampOutput
        {
            set => SetValue((int)ArithmeticCompositeProperties.ClampOutput, value);
            get => GetBoolValue((int)ArithmeticCompositeProperties.ClampOutput);
        }
    }
}
