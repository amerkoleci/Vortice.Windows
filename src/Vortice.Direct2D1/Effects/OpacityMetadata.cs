// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    using Props = OpacityMetadataProperties;
    public class OpacityMetadata : ID2D1Effect
    {
        public OpacityMetadata(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.OpacityMetadata, this);
        }
        public Vector4 InputOpaqueRectangle
        {
            set => SetValue((int)Props.InputOpaqueRectangle, value);
            get => GetVector4Value((int)Props.InputOpaqueRectangle);
        }
    }
}
