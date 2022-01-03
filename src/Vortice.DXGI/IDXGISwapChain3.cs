// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.DXGI;

public partial class IDXGISwapChain3
{
    public Result ResizeBuffers1(int bufferCount, int width, int height, Format format = Format.Unknown, SwapChainFlags swapChainFlags = SwapChainFlags.None)
    {
        return ResizeBuffers1(bufferCount, width, height, format, swapChainFlags, null, null);
    }
}
