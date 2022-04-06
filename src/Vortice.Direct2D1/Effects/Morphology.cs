// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Morphology : ID2D1Effect
{
    public Morphology(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Morphology))
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
        get => (int)GetUintValue((int)MorphologyProperties.Width);
        set => SetValue((int)MorphologyProperties.Width, (uint)value);
    }

    public int Height
    {
        get => (int)GetUintValue((int)MorphologyProperties.Height);
        set => SetValue((int)MorphologyProperties.Height, (uint)value);
    }
}
