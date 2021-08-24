// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Threading;

namespace Vortice.Direct3D12
{
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
}
