// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = EdgeDetectionProperties;
    public class EdgeDetection : ID2D1Effect
    {
        public EdgeDetection(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.EdgeDetection, this);
        }
        public float Strength
        {
            set => SetValue((int)Props.Strength, value);
            get => GetFloatValue((int)Props.Strength);
        }
        public float BlurRadius
        {
            set => SetValue((int)Props.BlurRadius, value);
            get => GetFloatValue((int)Props.BlurRadius);
        }
        public EdgeDetectionMode Mode
        {
            set => SetValue((int)Props.Mode, value);
            get => GetEnumValue<EdgeDetectionMode>((int)Props.Mode);
        }
        public bool OverlayEdges
        {
            set => SetValue((int)Props.OverlayEdges, value);
            get => GetBoolValue((int)Props.OverlayEdges);
        }
        public AlphaMode AlphaMode
        {
            set => SetValue((int)Props.AlphaMode, value);
            get => GetEnumValue<AlphaMode>((int)Props.AlphaMode);
        }
    }
}
