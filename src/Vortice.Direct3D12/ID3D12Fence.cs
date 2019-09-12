// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Threading;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Fence
    {
        public void SetEventOnCompletion(long value, EventWaitHandle waitHandle)
        {
            SetEventOnCompletion(value, waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
