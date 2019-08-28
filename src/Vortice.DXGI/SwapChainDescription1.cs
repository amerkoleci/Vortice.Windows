// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DXGI
{
    public partial struct SwapChainDescription1
    {
        /// <summary>
        /// Create new instance of <see cref="SwapChainDescription1"/> struct.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="format"></param>
        /// <param name="stereo"></param>
        /// <param name="usage"></param>
        /// <param name="bufferCount"></param>
        /// <param name="scaling"></param>
        /// <param name="swapEffect"></param>
        /// <param name="alphaMode"></param>
        /// <param name="flags"></param>
        public SwapChainDescription1(
            int width,
            int height,
            Format format = Format.B8G8R8A8_UNorm,
            bool stereo = false,
            Usage usage = Usage.RenderTargetOutput,
            int bufferCount = 2,
            Scaling scaling = Scaling.None,
            SwapEffect swapEffect = SwapEffect.FlipDiscard,
            AlphaMode alphaMode = AlphaMode.Unspecified,
            SwapChainFlags flags = SwapChainFlags.None)
        {
            Width = width;
            Height = height;
            Format = format;
            Stereo = stereo;
            SampleDescription = new SampleDescription(1, 0);
            Usage = usage;
            BufferCount = bufferCount;
            Scaling = scaling;
            SwapEffect = swapEffect;
            AlphaMode = alphaMode;
            Flags = flags;
        }
    }
}
