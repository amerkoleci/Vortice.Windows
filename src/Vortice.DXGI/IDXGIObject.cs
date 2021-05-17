// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.DXGI
{
    public partial class IDXGIObject
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
                    IntPtr namePtr = Marshal.StringToHGlobalAnsi(value);
                    SetPrivateData(CommonGuid.DebugObjectName, value.Length, namePtr);
                }
            }

        }
        public T? GetParent<T>() where T : ComObject
        {
            if (GetParent(typeof(T).GUID, out IntPtr nativePtr).Failure)
            {
                return default;
            }

            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }
}
