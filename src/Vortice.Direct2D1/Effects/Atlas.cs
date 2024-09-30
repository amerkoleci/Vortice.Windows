// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Atlas : ID2D1Effect
{
    public Atlas(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Atlas))
    {

    }

    public Atlas(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Atlas))
    {
    }

    public Vector4 InputRectangle
    {
        set => SetValue((int)AtlasProperties.InputRectangle, value);
        get => GetVector4Value((int)AtlasProperties.InputRectangle);
    }

    public Vector4 InputPaddingRect
    {
        set => SetValue((int)AtlasProperties.InputPaddingRectangle, value);
        get => GetVector4Value((int)AtlasProperties.InputPaddingRectangle);
    }
}
