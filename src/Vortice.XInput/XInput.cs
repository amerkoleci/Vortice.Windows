// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    public static class XInput
    {
        private static readonly IXInput s_xInput;

        static XInput()
        {
            if (LoadLibrary("xinput1_4.dll") != IntPtr.Zero)
            {
                s_xInput = new XInput14();
            }
            else if (LoadLibrary("xinput1_3.dll") != IntPtr.Zero)
            {
                s_xInput = new XInput13();
            }
            else if (LoadLibrary("xinput9_1_0.dll") != IntPtr.Zero)
            {
                s_xInput = new XInput910();
            }
        }

        /// <summary>
        /// Retrieves the current state of the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="state">Instance of <see cref="State"/> struct.</param>
        /// <returns>True if success, false if not connected or error.</returns>
        public static bool GetState(UserIndex userIndex, out State state)
        {
            return s_xInput.XInputGetState((int)userIndex, out state) == 0;
        }

        /// <summary>
        /// Sets the gamepad vibration.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="leftMotor">The level of the left vibration motor. Valid values are between 0.0 and 1.0, where 0.0 signifies no motor use and 1.0 signifies max vibration.</param>
        /// <param name="rightMotor">The level of the right vibration motor. Valid values are between 0.0 and 1.0, where 0.0 signifies no motor use and 1.0 signifies max vibration.</param>
        /// <returns>True if succeed, false otherwise.</returns>
        public static bool SetVibration(UserIndex userIndex, float leftMotor, float rightMotor)
        {
            var vibration = new Vibration
            {
                LeftMotorSpeed = (ushort)(leftMotor * ushort.MaxValue),
                RightMotorSpeed = (ushort)(rightMotor * ushort.MaxValue)
            };
            return s_xInput.XInputSetState((int)userIndex, vibration) == 0;
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);
    }
}
