// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = TileProperties;
    public class Tile : ID2D1Effect
    {
        public Tile(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Tile, this);
        }

        public Vector4 Rectangle
        {
            set => SetValue((int)Props.Rectangle, value);
            get => GetVector4Value((int)Props.Rectangle);
        }
    }
}
