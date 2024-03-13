// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

[StructLayout(LayoutKind.Sequential)]
public readonly struct Vibration(ushort leftMotorSpeed, ushort rightMotorSpeed)
{
    public readonly ushort LeftMotorSpeed = leftMotorSpeed;
    public readonly ushort RightMotorSpeed = rightMotorSpeed;
}
