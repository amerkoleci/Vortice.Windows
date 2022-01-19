// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.InteropServices;

namespace Vortice.XInput;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct Vibration
{
    public ushort LeftMotorSpeed;
    public ushort RightMotorSpeed;
}
