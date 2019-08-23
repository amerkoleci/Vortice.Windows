// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.XInput
{
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
}
