// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.XInput;

/// <summary>
/// Describes the battery type.
/// </summary>
public enum BatteryType : byte
{
    /// <summary>
    /// The device is not connected. 
    /// </summary>
    Disconnected = 0,
    /// <summary>
    /// The device is a wired device and does not have a battery. 
    /// </summary>
    Wired = 1,
    /// <summary>
    /// The device has an alkaline battery. 
    /// </summary>
    Alkaline = 2,
    /// <summary>
    /// The device has a nickel metal hydride battery. 
    /// </summary>
    Nimh = 3,
    /// <summary>
    /// The device has an unknown battery type. 
    /// </summary>
    Unknown = byte.MaxValue
}
