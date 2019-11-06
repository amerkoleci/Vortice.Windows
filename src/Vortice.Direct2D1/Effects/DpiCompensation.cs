// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = DpiCompensationProperties;
    public class DpiCompensation : ID2D1Effect
    {
        public DpiCompensation(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.DpiCompensation, this);
        }
        public DpiCompensationInterpolationMode InterpolationMode
        {
            set => SetValue((int)Props.InterpolationMode, value);
            get => GetEnumValue<DpiCompensationInterpolationMode>((int)Props.InterpolationMode);
        }
        public BorderMode BorderMode
        {
            set => SetValue((int)Props.BorderMode, value);
            get => GetEnumValue<BorderMode>((int)Props.BorderMode);
        }
        public float InputDpi
        {
            set => SetValue((int)Props.InputDpi, value);
            get => GetFloatValue((int)Props.InputDpi);
        }
    }
}
