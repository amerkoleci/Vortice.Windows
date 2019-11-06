// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = ChromakeyProperties;
    public class ChromaKey : ID2D1Effect
    {
        public ChromaKey(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.ChromaKey, this);
        }

        public Vector3 Color
        {
            set => SetValue((int)Props.Color, value);
            get => GetVector3Value((int)Props.Color);
        }

        public float Tolerance
        {
            set => SetValue((int)Props.Tolerance, value);
            get => GetFloatValue((int)Props.Tolerance);
        }

        public bool InvertAlpha
        {
            set => SetValue((int)Props.InverseErtAlpha, value);
            get => GetBoolValue((int)Props.InverseErtAlpha);
        }

        public bool Feather
        {
            set => SetValue((int)Props.Feather, value);
            get => GetBoolValue((int)Props.Feather);
        }
    }
}
