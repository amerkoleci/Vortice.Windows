// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class OpacityMetadata : ID2D1Effect
{
    public OpacityMetadata(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.OpacityMetadata))
    {
    }

    public OpacityMetadata(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.OpacityMetadata))
    {
    }

    public Vector4 InputOpaqueRectangle
    {
        get => GetVector4Value((int)OpacityMetadataProperties.InputOpaqueRectangle);
        set => SetValue((int)OpacityMetadataProperties.InputOpaqueRectangle, value);
    }
}
