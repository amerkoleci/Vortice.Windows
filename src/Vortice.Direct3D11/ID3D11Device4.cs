// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Device4
{
    public int RegisterDeviceRemovedEvent(WaitHandle waitHandle)
    {
        return RegisterDeviceRemovedEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
    }
}
