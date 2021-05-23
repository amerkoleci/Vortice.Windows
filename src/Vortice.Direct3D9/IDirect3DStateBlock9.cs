// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DStateBlock9
    {
        protected override void NativePointerUpdated(IntPtr oldNativePointer)
        {
            ReleaseDevice();
            base.NativePointerUpdated(oldNativePointer);
        }

        protected override void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            if (disposing)
                ReleaseDevice();

            base.DisposeCore(nativePointer, disposing);
        }

        private void ReleaseDevice()
        {
            if (Device__ != null)
            {
                // Don't use Dispose() in order to avoid circular references with DeviceContext
                ((IUnknown)Device__).Release();
                Device__ = null;
            }
        }
    }
}
