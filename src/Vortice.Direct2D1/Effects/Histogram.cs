// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct2D1.Effects
{
    public sealed class Histogram : ID2D1Effect
    {
        public Histogram(ID2D1DeviceContext context)
            : base(context.CreateEffect(EffectGuids.Histogram))
        {
        }

        public Histogram(ID2D1EffectContext context)
            : base(context.CreateEffect(EffectGuids.Histogram))
        {
        }

        public int NumBins
        {
            get => GetIntValue((int)HistogramProperties.NumBins);
            set => SetValue((int)HistogramProperties.NumBins, value);
        }

        public ChannelSelector ChannelSelect
        {
            get => GetEnumValue<ChannelSelector>((int)HistogramProperties.ChannelSelect);
            set => SetValue((int)HistogramProperties.ChannelSelect, value);
        }

        public unsafe void GetHistogramOutput(float[] output)
        {
            var numBins = NumBins;
            if (output.Length < numBins)
            {
                throw new ArgumentException();
            }

            fixed (void* outputPtr = &output[0])
            {
                GetValue((int)HistogramProperties.HistogramOutput, PropertyType.Blob, (IntPtr)outputPtr, sizeof(float) * numBins);
            }
        }
    }
}
