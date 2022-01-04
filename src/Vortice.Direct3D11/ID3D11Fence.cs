// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime.Win32;

namespace Vortice.Direct3D11;

public partial class ID3D11Fence
{
    private const int GENERIC_ALL = 0x10000000;

    public IntPtr CreateSharedHandle(SecurityAttributes? attributes, string name)
    {
        return CreateSharedHandle(attributes, GENERIC_ALL, name);
    }

    public void SetEventOnCompletion(ulong value, WaitHandle waitHandle)
    {
        SetEventOnCompletion(value, waitHandle.SafeWaitHandle.DangerousGetHandle());
    }
}
