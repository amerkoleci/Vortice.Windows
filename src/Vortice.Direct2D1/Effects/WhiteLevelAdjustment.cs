// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = WhiteLevelAdjustmentProperties;
    public class WhiteLevelAdjustment : ID2D1Effect
    {
        public WhiteLevelAdjustment(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.WhiteLevelAdjustment, this);
        }

        public float InputWhiteLebel
        {
            set => SetValue((int)Props.InputWhiteLevel, value);
            get => GetFloatValue((int)Props.InputWhiteLevel);
        }
        public float OutputWhiteLevel
        {
            set => SetValue((int)Props.OutputWhiteLevel, value);
            get => GetFloatValue((int)Props.OutputWhiteLevel);
        }
    }
}
