// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    /// <summary>
    /// Represents the state of a controller.
    /// </summary>
    /// <remarks>
    /// The <see cref="PacketNumber"/> member is incremented only if the status of the controller has changed since the controller was last polled.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct State
    {
        /// <summary>
        /// The packet number indicates whether there have been any changes in the state of the controller.
        /// If the <see cref="PacketNumber"/> member is the same in sequentially returned <see cref="State"/> structures, the controller state has not changed.
        /// </summary>
        public int PacketNumber;

        /// <summary>
        /// <dd> <p> <strong><see cref="T:SharpDX.XInput.Gamepad" /></strong> structure containing the current state of an Xbox 360 Controller.</p> </dd>
        /// </summary>
        public Gamepad Gamepad;
    }
}
