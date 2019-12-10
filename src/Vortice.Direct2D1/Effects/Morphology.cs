// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Morphology : ID2D1Effect
    {
        public Morphology(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.Morphology))
        {
        }

        public Morphology(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Morphology))
        {
        }

        public MorphologyMode Mode
        {
            get => GetEnumValue<MorphologyMode>((int)MorphologyProperties.Mode);
            set => SetValue((int)MorphologyProperties.Mode, value);
        }

        public int Width
        {
            get => GetIntValue((int)MorphologyProperties.Width);
            set => SetValue((int)MorphologyProperties.Width, value);
        }

        public int Height
        {
            get => GetIntValue((int)MorphologyProperties.Height);
            set => SetValue((int)MorphologyProperties.Height, value);
        }
    }
}
