// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Straighten : ID2D1Effect
{
    public Straighten(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Straighten))
    {
    }

    public Straighten(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Straighten))
    {
    }

    public float Angle
    {
        get => GetFloatValue((int)StraightenProperties.Angle);
        set => SetValue((int)StraightenProperties.Angle, value);
    }

    public bool MaintainSize
    {
        get => GetBoolValue((int)StraightenProperties.MaintainSize);
        set => SetValue((int)StraightenProperties.MaintainSize, value);
    }

    public StraightenModeProperties ScaleMode
    {
        get => GetEnumValue<StraightenModeProperties>((int)StraightenProperties.ScaleMode);
        set => SetValue((int)StraightenProperties.ScaleMode, value);
    }

}
