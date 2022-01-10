// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory2
{
    public int RegisterOcclusionStatusEvent(WaitHandle waitHandle)
    {
        return RegisterOcclusionStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
    }

    public int RegisterStereoStatusEvent(WaitHandle waitHandle)
    {
        return RegisterStereoStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
    }
}
