// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11;

public partial class ID3D11VideoProcessorEnumerator
{
    public VideoProcessorFormatSupport CheckVideoProcessorFormat(Format format)
    {
        CheckVideoProcessorFormat(format, out VideoProcessorFormatSupport formatSupport).CheckError();
        return formatSupport;
    }

    public VideoProcessorRateConversionCaps GetVideoProcessorRateConversionCaps(int typeIndex)
    {
        GetVideoProcessorRateConversionCaps(typeIndex, out VideoProcessorRateConversionCaps caps).CheckError();
        return caps;
    }

    public VideoProcessorCustomRate GetVideoProcessorCustomRate(int typeIndex, int customRateIndex)
    {
        GetVideoProcessorCustomRate(typeIndex, customRateIndex, out VideoProcessorCustomRate rate).CheckError();
        return rate;
    }

    public VideoProcessorFilterRange GetVideoProcessorFilterRange(VideoProcessorFilter filter)
    {
        GetVideoProcessorFilterRange(filter, out VideoProcessorFilterRange filterRange).CheckError();
        return filterRange;
    }
}
