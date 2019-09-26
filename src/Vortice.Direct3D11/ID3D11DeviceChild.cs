// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Direct3D11
{
    public partial class ID3D11DeviceChild
    {
        /// <summary>
        /// Gets or sets the debug-name for this object.
        /// </summary>
        public string DebugName
        {
            get
            {
                unsafe
                {
                    byte* pname = stackalloc byte[1024];
                    int size = 1024 - 1;
                    if (GetPrivateData(CommonGuid.DebugObjectName, ref size, new IntPtr(pname)).Failure)
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
                    SetPrivateData(CommonGuid.DebugObjectName, 0, IntPtr.Zero);
                }
                else
                {
                    var namePtr = Marshal.StringToHGlobalAnsi(value);
                    SetPrivateData(CommonGuid.DebugObjectName, value.Length, namePtr);
                    Marshal.FreeHGlobal(namePtr);
                }
            }
        }

        protected override void NativePointerUpdated(IntPtr oldNativePointer)
        {
            ReleaseDevice();
            base.NativePointerUpdated(oldNativePointer);
        }

        /// <inheritdoc/>
        protected override unsafe void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseDevice();
            }
            base.Dispose(disposing);
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
