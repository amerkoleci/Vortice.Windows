// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGISwapChain3
{
    public SwapChainColorSpaceSupportFlags CheckColorSpaceSupport(ColorSpaceType colorSpace)
    {
        Result result = CheckColorSpaceSupport(colorSpace, out SwapChainColorSpaceSupportFlags colorSpaceSupport);
        if (result.Failure)
        {
            return SwapChainColorSpaceSupportFlags.None;
        }

        return colorSpaceSupport;
    }

    public Result ResizeBuffers1(uint bufferCount, uint width, uint height, Format format = Format.Unknown, SwapChainFlags swapChainFlags = SwapChainFlags.None)
    {
        return ResizeBuffers1(bufferCount, width, height, format, swapChainFlags, null, null);
    }
}
