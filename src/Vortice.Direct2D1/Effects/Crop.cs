// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Crop : ID2D1Effect
{
    public Crop(ID2D1DeviceContext context)
        : base(context.CreateEffect(EffectGuids.Crop))
    {
    }

    public Crop(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Crop))
    {
    }

    public Vector4 Rectangle
    {
        set => SetValue((int)CropProperties.Rectangle, value);
        get => GetVector4Value((int)CropProperties.Rectangle);
    }

    public BorderMode BorderMode
    {
        set => SetValue((int)CropProperties.BorderMode, value);
        get => GetEnumValue<BorderMode>((int)CropProperties.BorderMode);
    }
}
