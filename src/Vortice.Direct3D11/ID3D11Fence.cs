// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime.Win32;
using System.Threading;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Fence
    {
        private const int GENERIC_ALL = 0x10000000;

        public IntPtr CreateSharedHandle(SecurityAttributes? attributes, string name)
        {
            return CreateSharedHandle(attributes, GENERIC_ALL, name);
        }

        public void SetEventOnCompletion(ulong value, EventWaitHandle waitHandle)
        {
            SetEventOnCompletion(value, waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
