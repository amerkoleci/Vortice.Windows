// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct2D1.Effects;

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
        get => (int)GetUIntValue((int)HistogramProperties.NumBins);
        set => SetValue((int)HistogramProperties.NumBins, (uint)value);
    }

    public ChannelSelector ChannelSelect
    {
        get => GetEnumValue<ChannelSelector>((int)HistogramProperties.ChannelSelect);
        set => SetValue((int)HistogramProperties.ChannelSelect, value);
    }

    public unsafe void GetHistogramOutput(float[] output)
    {
        int numBins = NumBins;
        if (output.Length < numBins)
        {
            throw new ArgumentException();
        }

        fixed (float* outputPtr = output)
        {
            GetValue((int)HistogramProperties.HistogramOutput, PropertyType.Blob, outputPtr, sizeof(float) * numBins);
        }
    }

    public unsafe void GetHistogramOutput(Span<float> output)
    {
        int numBins = NumBins;
        if (output.Length < numBins)
        {
            throw new ArgumentException();
        }

        fixed (float* outputPtr = output)
        {
            GetValue((int)HistogramProperties.HistogramOutput, PropertyType.Blob, outputPtr, sizeof(float) * numBins);
        }
    }
}
