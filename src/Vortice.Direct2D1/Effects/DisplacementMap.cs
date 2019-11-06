// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = DisplacementMapProperties;
    public class DisplacementMap : ID2D1Effect
    {
        public DisplacementMap(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.DisplacementMap, this);
        }
        public float Scale
        {
            set => SetValue((int)Props.Scale, value);
            get => GetFloatValue((int)Props.Scale);
        }
        public ChannelSelector XChannelSelect
        {
            set => SetValue((int)Props.XChannelSelect, value);
            get => GetEnumValue<ChannelSelector>((int)Props.XChannelSelect);
        }
        public ChannelSelector YChannelSelect
        {
            set => SetValue((int)Props.YChannelSelect, value);
            get => GetEnumValue<ChannelSelector>((int)Props.YChannelSelect);
        }
    }
}
