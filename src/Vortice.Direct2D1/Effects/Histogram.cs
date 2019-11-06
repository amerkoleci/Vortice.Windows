// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.Direct2D1.Effects
{
    using Props = HistogramProperties;
    public class Histogram : ID2D1Effect
    {
        public Histogram(ID2D1DeviceContext deviceContext) : base(IntPtr.Zero)
        {
            deviceContext.CreateEffect(EffectGuids.Histogram, this);
        }
        public uint NumBins
        {
            set => SetValue((int)Props.NumBins, value);
            get => GetUintValue((int)Props.NumBins);
        }
        public ChannelSelector ChannelSelect
        {
            set => SetValue((int)Props.ChannelSelect, value);
            get => GetEnumValue<ChannelSelector>((int)Props.ChannelSelect);
        }
        public unsafe void GetHistogramOutput(float[] output)
        {
            var numBins = (int)NumBins;
            if (output.Length < numBins)
                throw new ArgumentException();
            GetValue((int)Props.HistogramOutput, PropertyType.Blob, new IntPtr(Unsafe.AsPointer(ref output[0])), sizeof(float) * numBins);
        }

    }
}
