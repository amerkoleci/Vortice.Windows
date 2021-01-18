// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Dxc
{
    public partial class IDxcBlobUtf16
    {
        public string StringPointer
        {
            get => Marshal.PtrToStringUni(GetStringPointer());
        }

        private unsafe IntPtr GetStringPointer()
        {
            return LocalInterop.CalliStdCallSystemIntPtr(_nativePointer, (*(void***)_nativePointer)[6]);
        }
    }
}
