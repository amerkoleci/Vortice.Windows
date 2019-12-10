// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class Flood : ID2D1Effect
    {
        public Flood(ID2D1DeviceContext context)
             : base(context.CreateEffect_(EffectGuids.Flood))
        {
        }

        public Flood(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Flood))
        {
        }

        public Vector4 Color
        {
            get => GetVector4Value((int)FloodProperties.Color);
            set => SetValue((int)FloodProperties.Color, value);
        }
    }
}
