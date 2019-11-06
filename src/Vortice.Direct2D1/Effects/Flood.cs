// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = FloodProperties;
    public class Flood : ID2D1Effect
    {
        public Flood(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Flood, this);
        }
        public Vector4 Color
        {
            set => SetValue((int)Props.Color, value);
            get => GetVector4Value((int)Props.Color);
        }
    }
}
