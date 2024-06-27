// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

using Props = HDRToneMapProperties;
public sealed class HdrToneMap : ID2D1Effect
{
    public HdrToneMap(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.HdrToneMap))
    {
    }

    public HdrToneMap(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.HdrToneMap))
    {
    }

    public float InputMaxLuminance
    {
        set => SetValue((int)Props.InputMaxLuminance, value);
        get => GetFloatValue((int)Props.InputMaxLuminance);
    }
    public float OutputMaxLuminance
    {
        set => SetValue((int)Props.OutputMaxLuminance, value);
        get => GetFloatValue((int)Props.OutputMaxLuminance);
    }
    public HDRToneMapDisplayMode DisplayMode
    {
        set => SetValue((int)Props.DisplayMode, value);
        get => GetEnumValue<HDRToneMapDisplayMode>((int)Props.DisplayMode);
    }
}
