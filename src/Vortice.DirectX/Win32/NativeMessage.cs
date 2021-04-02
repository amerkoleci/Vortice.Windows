// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.Mathematics;

namespace Vortice.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct NativeMessage
    {
        public readonly IntPtr hwnd;
        public readonly uint msg;
        public readonly nuint wParam;
        public readonly nint lParam;
        public readonly uint time;
        public readonly Point pt;
    }
}
