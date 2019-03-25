// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpDXGI;

namespace SharpDirect3D11
{
    public partial class ID3D11Device5
    {
        public ID3D11Fence CreateFence(ulong initialValue, FenceFlags flags = FenceFlags.None)
        {
            return CreateFence(initialValue, flags, typeof(ID3D11Fence).GUID);
        }

        public ID3D11Fence OpenSharedFence(IntPtr fenceHandle)
        {
            Guard.IsTrue(fenceHandle != IntPtr.Zero, nameof(fenceHandle), "Invalid fence handle");

            return OpenSharedFence(fenceHandle, typeof(ID3D11Fence).GUID);
        }
    }
}
