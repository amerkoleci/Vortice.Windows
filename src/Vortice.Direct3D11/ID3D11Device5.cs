// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device5
    {
        public ID3D11Fence CreateFence(long initialValue, FenceFlags flags = FenceFlags.None)
        {
            return CreateFence(initialValue, flags, typeof(ID3D11Fence).GUID);
        }

        public ID3D11Fence OpenSharedFence(IntPtr fenceHandle)
        {
            return OpenSharedFence(fenceHandle, typeof(ID3D11Fence).GUID);
        }
    }
}
