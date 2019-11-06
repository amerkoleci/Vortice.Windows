// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    using Props = PosterizeProperties;
    public class Posterize : ID2D1Effect
    {
        public Posterize(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Posterize, this);
        }
        public uint RedValueCount
        {
            set => SetValue((int)Props.RedValueCount, value);
            get => GetUintValue((int)Props.RedValueCount);
        }
        public uint GreenValueCount
        {
            set => SetValue((int)Props.GreenValueCount, value);
            get => GetUintValue((int)Props.GreenValueCount);
        }
        public uint BlueValueCount
        {
            set => SetValue((int)Props.BlueValueCount, value);
            get => GetUintValue((int)Props.BlueValueCount);
        }
    }
}
