// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    public partial class ID3D11VideoProcessorEnumerator
    {
        public VideoProcessorFormatSupport CheckVideoProcessorFormat(Format format)
        {
            CheckVideoProcessorFormat(format, out VideoProcessorFormatSupport formatSupport).CheckError();
            return formatSupport;
        }
    }
}
