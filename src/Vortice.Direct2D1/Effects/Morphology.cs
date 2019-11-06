// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = MorphologyProperties;
    public class Morphology : ID2D1Effect
    {
        public Morphology(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Morphology, this);
        }
        public MorphologyMode Mode
        {
            set => SetValue((int)Props.Mode, value);
            get => GetEnumValue<MorphologyMode>((int)Props.Mode);
        }
        public uint Width
        {
            set => SetValue((int)Props.Width, value);
            get => GetUintValue((int)Props.Width);
        }
        public uint Height
        {
            set => SetValue((int)Props.Height, value);
            get => GetUintValue((int)Props.Height);
        }
    }
}
