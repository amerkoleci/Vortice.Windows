// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Posterize : ID2D1Effect
{
    public Posterize(ID2D1DeviceContext context)
       : base(context.CreateEffect(EffectGuids.Posterize))
    {
    }

    public Posterize(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Posterize))
    {
    }

    public int RedValueCount
    {
        get => (int)GetUIntValue((int)PosterizeProperties.RedValueCount);
        set => SetValue((int)PosterizeProperties.RedValueCount, (uint)value);
    }

    public int GreenValueCount
    {
        get => (int)GetUIntValue((int)PosterizeProperties.GreenValueCount);
        set => SetValue((int)PosterizeProperties.GreenValueCount, (uint)value);
    }

    public int BlueValueCount
    {
        get => (int)GetUIntValue((int)PosterizeProperties.BlueValueCount);
        set => SetValue((int)PosterizeProperties.BlueValueCount, (uint)value);
    }
}
