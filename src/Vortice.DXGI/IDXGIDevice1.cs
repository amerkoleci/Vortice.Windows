// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIDevice1
{
    public int MaximumFrameLatency
    {
        get
        {
            GetMaximumFrameLatency(out int latency).CheckError();
            return latency;
        }
        set
        {
            SetMaximumFrameLatency(value).CheckError();
        }
    }
}
