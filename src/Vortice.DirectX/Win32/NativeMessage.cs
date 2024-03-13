// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Win32;

[StructLayout(LayoutKind.Sequential)]
public partial struct NativeMessage
{
    //[NativeTypeName("HWND")]
    public nint hwnd;
    public uint msg;
    //[NativeTypeName("WPARAM")]
    public nuint wParam;
    //[NativeTypeName("LPARAM")]
    public nint lParam;
    public uint time;
    public Int2 pt;
}
