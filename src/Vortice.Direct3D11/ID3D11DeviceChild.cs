// Copyright (c) Amer Koleci and Contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D11
{
    public partial class ID3D11DeviceChild
    {
        /// <summary>
        /// Gets or sets the debug-name for this object.
        /// </summary>
        public unsafe string DebugName
        {
            get
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
    }
}
