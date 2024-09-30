// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.DXGI;

public partial class IDXGIDecodeSwapChain
{
    public SizeI DestSize
    {
        get
        {
            GetDestSize(out uint width, out uint height);
            return new((int)width, (int)height);
        }
        set
        {
            SetDestSize((uint)value.Width, (uint)value.Height);
        }
    }

    public Result PresentBuffer(uint bufferToPresent, uint syncInterval)
    {
        return PresentBuffer(bufferToPresent, syncInterval, PresentFlags.None);
    }
}
