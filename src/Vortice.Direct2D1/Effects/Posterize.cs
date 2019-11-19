// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects
{
    public sealed class Posterize : ID2D1Effect
    {
        public Posterize(ID2D1DeviceContext context)
           : base(context.CreateEffect_(EffectGuids.Posterize))
        {
        }

        public Posterize(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Posterize))
        {
        }

        public int RedValueCount
        {
            set => SetValue((int)PosterizeProperties.RedValueCount, value);
            get => GetIntValue((int)PosterizeProperties.RedValueCount);
        }

        public int GreenValueCount
        {
            set => SetValue((int)PosterizeProperties.GreenValueCount, value);
            get => GetIntValue((int)PosterizeProperties.GreenValueCount);
        }

        public int BlueValueCount
        {
            set => SetValue((int)PosterizeProperties.BlueValueCount, value);
            get => GetIntValue((int)PosterizeProperties.BlueValueCount);
        }
    }
}
