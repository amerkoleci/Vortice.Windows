// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DResource9
    {
        /// <summary>
        /// Gets or sets the debug-name for this object.
        /// </summary>
        /// <value>
        /// The debug name.
        /// </value>
        public string DebugName
        {
            get
            {
                unsafe
                {
                    byte* pname = stackalloc byte[1024];
                    int size = 1024 - 1;
                    if (GetPrivateData(CommonGuid.DebugObjectName, new IntPtr(pname), ref size).Failure)
                    {
                        return string.Empty;
                    }

                    pname[size] = 0;
                    return Marshal.PtrToStringAnsi(new IntPtr(pname));
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    SetPrivateData(CommonGuid.DebugObjectName, IntPtr.Zero, 0, 0);
                }
                else
                {
                    var namePtr = Marshal.StringToHGlobalAnsi(value);
                    SetPrivateData(CommonGuid.DebugObjectName, namePtr, value.Length, 0);
                }
            }
        }

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
