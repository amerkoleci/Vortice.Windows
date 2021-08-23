// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
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
}
