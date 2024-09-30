// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

public sealed class Tile : ID2D1Effect
{
    public Tile(ID2D1DeviceContext context)
         : base(context.CreateEffect(EffectGuids.Tile))
    {
    }

    public Tile(ID2D1EffectContext context)
        : base(context.CreateEffect(EffectGuids.Tile))
    {
    }

    public Vector4 Rectangle
    {
        get => GetVector4Value((int)TileProperties.Rectangle);
        set => SetValue((int)TileProperties.Rectangle, value);
    }
}
