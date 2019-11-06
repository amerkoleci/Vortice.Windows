// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = StraightenProperties;
    public class Straighten : ID2D1Effect
    {
        public Straighten(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Straighten, this);
        }
        public float Angle
        {
            set => SetValue((int)Props.Angle, value);
            get => GetFloatValue((int)Props.Angle);
        }
        public bool MaintainSize
        {
            set => SetValue((int)Props.MaintainSize, value);
            get => GetBoolValue((int)Props.MaintainSize);
        }
        public StraightenModeProperties ScaleMode
        {
            set => SetValue((int)Props.ScaleMode, value);
            get => GetEnumValue<StraightenModeProperties>((int)Props.ScaleMode);
        }

    }
}
