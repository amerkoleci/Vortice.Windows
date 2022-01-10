// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIFactory7
{
    public int RegisterAdaptersChangedEvent(WaitHandle waitHandle)
    {
        return RegisterAdaptersChangedEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
    }
}
