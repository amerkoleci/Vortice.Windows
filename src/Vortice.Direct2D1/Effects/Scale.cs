// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class Scale : ID2D1Effect
    {
        public Scale(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.Scale))
        {
        }

        public Scale(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Scale))
        {
        }

        public Vector2 Value
        {
            set => SetValue((int)ScaleProperties.Scale, value);
            get => GetVector2Value((int)ScaleProperties.Scale);
        }

        public Vector2 CenterPoint
        {
            set => SetValue((int)ScaleProperties.CenterPoint, value);
            get => GetVector2Value((int)ScaleProperties.CenterPoint);
        }

        public BorderMode BorderMode
        {
            set => SetValue((int)ScaleProperties.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)ScaleProperties.BorderMode);
        }

        public float Sharpness
        {
            set => SetValue((int)ScaleProperties.Sharpness, value);
            get => GetFloatValue((int)ScaleProperties.Sharpness);
        }

        public ScaleInterpolationMode InterpolationMode
        {
            set => SetValue((int)ScaleProperties.InterpolationMode, value);
            get => GetEnumValue<ScaleInterpolationMode>((int)ScaleProperties.InterpolationMode);
        }
    }
}
