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
}
