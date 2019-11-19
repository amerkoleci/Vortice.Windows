// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Vortice.Direct2D1.Effects
{
    public sealed class OpacityMetadata : ID2D1Effect
    {
        public OpacityMetadata(ID2D1DeviceContext context)
             : base(context.CreateEffect_(EffectGuids.OpacityMetadata))
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
}
