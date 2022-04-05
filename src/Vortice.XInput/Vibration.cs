// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public readonly struct Vibration
{
    public readonly ushort LeftMotorSpeed;
    public readonly ushort RightMotorSpeed;

    public Vibration(ushort leftMotorSpeed, ushort rightMotorSpeed)
    {
        LeftMotorSpeed = leftMotorSpeed;
        RightMotorSpeed = rightMotorSpeed;
    }
}
