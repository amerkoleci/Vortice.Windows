// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = CompositeProperties;
    public class Composite : ID2D1Effect
    {
        public Composite(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Composite, this);
        }

        public CompositeMode Mode
        {
            set => SetValue((int)Props.Mode, value);
            get => GetEnumValue<CompositeMode>((int)Props.Mode);
        }
    }
}
