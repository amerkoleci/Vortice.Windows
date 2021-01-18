// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Dxc
{
    public partial class IDxcCompilerArgs
    {
        public string Arguments
        {
            get => Marshal.PtrToStringUni(GetArguments());
        }

        private unsafe IntPtr GetArguments()
        {
            return LocalInterop.CalliStdCallSystemIntPtr(_nativePointer, (*(void***)_nativePointer)[3]);
        }
    }
}
