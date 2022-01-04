// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11DeviceContext3
{
    public void Flush1(ContextType contextType, WaitHandle waitHandle)
    {
        Flush1(contextType, waitHandle.SafeWaitHandle.DangerousGetHandle());
    }
}
