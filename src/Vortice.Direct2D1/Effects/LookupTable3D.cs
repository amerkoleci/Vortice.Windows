// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DCommon;

namespace Vortice.Direct2D1.Effects;

public sealed class LookupTable3D : ID2D1Effect
{
    public LookupTable3D(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.LookupTable3D))
    {
    }

    public LookupTable3D(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.LookupTable3D))
    {
    }

    public ID2D1LookupTable3D? LUT
    {
        get => GetIUnknownValue<ID2D1LookupTable3D>((int)LookupTable3DProperties.Lut);
        set => SetValue((int)LookupTable3DProperties.Lut, value);
    }

    public AlphaMode AlphaMode
    {
        get => GetEnumValue<AlphaMode>((int)LookupTable3DProperties.Lut);
        set => SetValue((int)LookupTable3DProperties.AlphaMode, value);
    }
}
