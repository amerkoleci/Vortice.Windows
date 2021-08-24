// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Threading;

namespace Vortice.Direct3D11
{
    public partial class ID3D11Device4
    {
        public int RegisterDeviceRemovedEvent(WaitHandle waitHandle)
        {
            return RegisterDeviceRemovedEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
