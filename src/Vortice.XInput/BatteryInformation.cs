// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

/// <summary>
/// Contains information on battery type and charge state.
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public readonly struct BatteryInformation
{
    /// <summary>
    /// The type of battery.
    /// </summary>
    public readonly BatteryType BatteryType;

    /// <summary>
    /// The charge state of the battery.
    /// </summary>
    public readonly BatteryLevel BatteryLevel;
}
