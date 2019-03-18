// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Threading;
using SharpDXGI;

namespace SharpD3D11
{
    public partial class ID3D11Device4
    {
        public int RegisterDeviceRemovedEvent(EventWaitHandle waitHandle)
        {
            Guard.NotNull(waitHandle, nameof(waitHandle));

            return RegisterDeviceRemovedEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
