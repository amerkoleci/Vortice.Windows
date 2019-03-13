// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Threading;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory7
    {
        public int RegisterAdaptersChangedEvent(EventWaitHandle waitHandle)
        {
            Guard.NotNull(waitHandle, nameof(waitHandle));

            return RegisterAdaptersChangedEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
