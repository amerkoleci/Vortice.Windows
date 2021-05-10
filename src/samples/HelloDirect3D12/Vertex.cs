// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;
using Vortice.Mathematics;

namespace HelloDirect3D12
{
    public readonly struct VertexPositionColor
    {
        public readonly Vector3 Position;
        public readonly Color4 Color;

        public VertexPositionColor(in Vector3 position, in Color4 color)
        {
            Position = position;
            Color = color;
        }
    }
}
