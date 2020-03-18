// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct2D1.Effects
{
    public class ColorMatrix : ID2D1Effect
    {
        public ColorMatrix(ID2D1DeviceContext context)
           : base(context.CreateEffect_(EffectGuids.ColorMatrix))
        {
        }

        public ColorMatrix(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.ColorMatrix))
        {
        }

        public Matrix5x4 Matrix
        {
            set => SetValue((int)ColorMatrixProperties.ColorMatrix, value);
            get => GetMatrix5x4Value((int)ColorMatrixProperties.ColorMatrix);
        }

        public ColorMatrixAlphaMode AlphaMode
        {
            set => SetValue((int)ColorMatrixProperties.AlphaMode, value);
            get => GetEnumValue<ColorMatrixAlphaMode>((int)ColorMatrixProperties.AlphaMode);
        }
    }
}
