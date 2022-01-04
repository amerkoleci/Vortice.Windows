// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Device5
{
    public ID3D11Fence CreateFence(ulong initialValue, FenceFlags flags = FenceFlags.None)
    {
        return CreateFence(initialValue, flags, typeof(ID3D11Fence).GUID);
    }

    public ID3D11Fence OpenSharedFence(IntPtr fenceHandle)
    {
        return OpenSharedFence(fenceHandle, typeof(ID3D11Fence).GUID);
    }
}
