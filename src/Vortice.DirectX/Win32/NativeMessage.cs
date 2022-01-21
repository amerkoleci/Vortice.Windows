// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Win32;

[StructLayout(LayoutKind.Sequential)]
public readonly struct NativeMessage
{
    public readonly IntPtr hwnd;
    public readonly uint msg;
    public readonly nuint wParam;
    public readonly nint lParam;
    public readonly uint time;
    public readonly PointI pt;
}
