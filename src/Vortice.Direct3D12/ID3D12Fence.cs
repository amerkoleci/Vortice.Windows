// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Fence
{
    public Result SetEventOnCompletion(ulong value)
    {
        return SetEventOnCompletion(value, IntPtr.Zero);
    }

    public Result SetEventOnCompletion(ulong value, WaitHandle? waitHandle)
    {
        if (waitHandle == null)
        {
            return SetEventOnCompletion(value, IntPtr.Zero);
        }
        else
        {
            return SetEventOnCompletion(value, waitHandle!.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
