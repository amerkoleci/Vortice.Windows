// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Fence
{
    public void SetEventOnCompletion(ulong value)
    {
        SetEventOnCompletion(value, IntPtr.Zero);
    }

    public void SetEventOnCompletion(ulong value, WaitHandle? waitHandle)
    {
        if (waitHandle == null)
        {
            SetEventOnCompletion(value, IntPtr.Zero);
        }
        else
        {
            SetEventOnCompletion(value, waitHandle!.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
