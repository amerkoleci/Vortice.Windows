// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XInput
{
    /// <summary>
    /// Retrieves the battery type and charge status of a wireless controller.
    /// </summary>
    public enum BatteryDeviceType
    {
        /// <summary>
        /// Index of the signed-in gamer associated with the device. 
        /// Can be a value in the range 0-4 ? 1.
        /// </summary>
        Gamepad,
        /// <summary>
        /// Specifies which device associated with this user index should be queried. Must be <strong>BATTERY_DEVTYPE_GAMEPAD</strong> or <strong>BATTERY_DEVTYPE_HEADSET</strong>.
        /// </summary>
        Headset
    }
}
