// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = HDRToneMapProperties;
    public sealed class HdrToneMap : ID2D1Effect
    {
        public HdrToneMap(ID2D1DeviceContext context)
            : base(context.CreateEffect_(EffectGuids.HdrToneMap))
        {
        }

        public HdrToneMap(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.HdrToneMap))
        {
        }

        public float InputMaxLuminance
        {
            set => SetValue((int)Props.InputMaxLuminance, value);
            get => GetFloatValue((int)Props.InputMaxLuminance);
        }
        public float OutputMaxLuminance
        {
            set => SetValue((int)Props.OutputMaxLuminance, value);
            get => GetFloatValue((int)Props.OutputMaxLuminance);
        }
        public HDRToneMapDisplayMode DisplayMode
        {
            set => SetValue((int)Props.DisplayMode, value);
            get => GetEnumValue<HDRToneMapDisplayMode>((int)Props.DisplayMode);
        }
    }
}
