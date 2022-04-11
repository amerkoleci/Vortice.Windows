// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

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
        get => (int)GetUIntValue((int)MorphologyProperties.Width);
        set => SetValue((int)MorphologyProperties.Width, (uint)value);
    }

    public int Height
    {
        get => (int)GetUIntValue((int)MorphologyProperties.Height);
        set => SetValue((int)MorphologyProperties.Height, (uint)value);
    }
}
