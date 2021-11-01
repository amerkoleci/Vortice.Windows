// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIFactory2
    {
        public int RegisterOcclusionStatusEvent(WaitHandle waitHandle)
        {
            return RegisterOcclusionStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }

        public int RegisterStereoStatusEvent(WaitHandle waitHandle)
        {
            return RegisterStereoStatusEvent(waitHandle.SafeWaitHandle.DangerousGetHandle());
        }
    }
}
