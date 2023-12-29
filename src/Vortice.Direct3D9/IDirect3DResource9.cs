// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DResource9
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
            byte* pname = stackalloc byte[1024];
            int size = 1024 - 1;
            if (GetPrivateData(Helpers.DebugObjectName, new IntPtr(pname), ref size).Failure)
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
                SetPrivateData(Helpers.DebugObjectName, IntPtr.Zero, 0, 0);
            }
            else
            {
                IntPtr namePtr = Marshal.StringToHGlobalAnsi(value);
                SetPrivateData(Helpers.DebugObjectName, namePtr, value.Length, 0);
            }
        }
    }
}
