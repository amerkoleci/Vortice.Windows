// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    /// <summary>
    /// Describes the capabilities of a connected controller. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Capabilities
    {
        /// <summary>
        /// Controller type. It must be one of the <see cref="DeviceType"/> values.
        /// </summary>
        public DeviceType Type;

        /// <summary>
        /// Subtype of the game controller. See <see cref="DeviceSubType"/> for a list of allowed subtypes.
        /// </summary>
        public DeviceSubType SubType;

        /// <summary>
        /// Features of the controller.
        /// </summary>
        public CapabilityFlags Flags;

        /// <summary>
        /// <see cref="Vortice.XInput.Gamepad"/> value that describes available controller features and control resolutions.
        /// </summary>
        public Gamepad Gamepad;

        /// <summary>
        /// <see cref="Vortice.XInput.Vibration"/> value that describes available vibration functionality and resolutions.
        /// </summary>
        public Vibration Vibration;
    }
}
