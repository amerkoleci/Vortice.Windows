// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI.WinUI
{
    [Guid("81521d7e-ff74-4a6b-8289-44bfd11cf0cc")]
    public partial class ISurfaceImageSourceManagerNative : ComObject
    {
        public ISurfaceImageSourceManagerNative(IntPtr nativePtr) : base(nativePtr)
        {
        }

        public static explicit operator ISurfaceImageSourceManagerNative(IntPtr nativePtr) => (nativePtr == IntPtr.Zero) ? null : new ISurfaceImageSourceManagerNative(nativePtr);

        public unsafe Result FlushAllSurfacesWithDevice(IUnknown device)
        {
            IntPtr devicePtr = ToCallbackPtr<IUnknown>(device);
            Result result = LocalInterop.CalliStdCallint(_nativePointer, devicePtr.ToPointer(), (*(void***)_nativePointer)[3]);
            GC.KeepAlive(device);
            return result;
        }
    }
}
