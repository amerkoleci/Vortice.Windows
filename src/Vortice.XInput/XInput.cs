// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    public static class XInput
    {
        private static readonly IXInput s_xInput;

        public static readonly XInputVersion Version;

        static XInput()
        {
#if WINDOWS_UWP
            s_xInput = new XInput14();
            Version = XInputVersion.Version14;
#else
            if (LoadLibrary("xinput1_4.dll") != IntPtr.Zero)
            {
                s_xInput = new XInput14();
                Version = XInputVersion.Version14;
            }
            else if (LoadLibrary("xinput1_3.dll") != IntPtr.Zero)
            {
                s_xInput = new XInput13();
                Version = XInputVersion.Version13;
            }
            else if (LoadLibrary("xinput9_1_0.dll") != IntPtr.Zero)
            {
                s_xInput = new XInput910();
                Version = XInputVersion.Version910;
            }
#endif
        }

        /// <summary>
        /// Retrieves the current state of the specified controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="state">Instance of <see cref="State"/> struct.</param>
        /// <returns>True if success, false if not connected or error.</returns>
        public static bool GetState(int userIndex, out State state)
        {
            return s_xInput.XInputGetState(userIndex, out state) == 0;
        }

        /// <summary>
        /// Sets the gamepad vibration.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="leftMotor">The level of the left vibration motor. Valid values are between 0.0 and 1.0, where 0.0 signifies no motor use and 1.0 signifies max vibration.</param>
        /// <param name="rightMotor">The level of the right vibration motor. Valid values are between 0.0 and 1.0, where 0.0 signifies no motor use and 1.0 signifies max vibration.</param>
        /// <returns>True if succeed, false otherwise.</returns>
        public static bool SetVibration(int userIndex, float leftMotor, float rightMotor)
        {
            var vibration = new Vibration
            {
                LeftMotorSpeed = (ushort)(leftMotor * ushort.MaxValue),
                RightMotorSpeed = (ushort)(rightMotor * ushort.MaxValue)
            };
            return s_xInput.XInputSetState(userIndex, vibration) == 0;
        }

        /// <summary>
        /// Sets the gamepad vibration.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="leftMotorSpeed">The level of the left vibration motor speed.</param>
        /// <param name="rightMotorSpeed">The level of the right vibration motor speed.</param>
        /// <returns>True if succeed, false otherwise.</returns>
        public static bool SetVibration(int userIndex, ushort leftMotorSpeed, ushort rightMotorSpeed)
        {
            var vibration = new Vibration
            {
                LeftMotorSpeed = leftMotorSpeed,
                RightMotorSpeed = rightMotorSpeed
            };
            return s_xInput.XInputSetState(userIndex, vibration) == 0;
        }

        /// <summary>
        /// Sets the gamepad vibration.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value from 0 to 3.</param>
        /// <param name="vibration">The <see cref="Vibration"/> to set.</param>
        /// <returns>True if succeed, false otherwise.</returns>
        public static bool SetVibration(int userIndex, Vibration vibration)
        {
            return s_xInput.XInputSetState(userIndex, vibration) == 0;
        }

        /// <summary>
        /// Sets the reporting.
        /// </summary>
        /// <param name="enableReporting">if set to <c>true</c> [enable reporting].</param>
        public static void SetReporting(bool enableReporting)
        {
            s_xInput.XInputEnable(enableReporting ? 1 : 0);
        }

        /// <summary>
        /// Retrieves the battery type and charge status of a wireless controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
        /// <param name="batteryDeviceType">Type of the battery device.</param>
        /// <returns>Instance of <see cref="BatteryInformation"/>.</returns>
        public static BatteryInformation GetBatteryInformation(int userIndex, BatteryDeviceType batteryDeviceType)
        {
            s_xInput.XInputGetBatteryInformation(userIndex, batteryDeviceType, out var result);
            return result;
        }

        /// <summary>
        /// Retrieves the battery type and charge status of a wireless controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
        /// <param name="batteryDeviceType">Type of the battery device.</param>
        /// <param name="batteryInformation">The battery information.</param>
        /// <returns>True if succeed, false otherwise.</returns>
        public static bool GetBatteryInformation(int userIndex, BatteryDeviceType batteryDeviceType, out BatteryInformation batteryInformation)
        {
            return s_xInput.XInputGetBatteryInformation(userIndex, batteryDeviceType, out batteryInformation) == 0;
        }

        /// <summary>
        /// Retrieves the capabilities and features of a connected controller.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
        /// <param name="deviceQueryType">Type of the device query.</param>
        /// <param name="capabilities">The capabilities of this controller.</param>
        /// <returns>True if the controller is connected and succeed, false otherwise.</returns>
        public static bool GetCapabilities(int userIndex, DeviceQueryType deviceQueryType, out Capabilities capabilities)
        {
            return s_xInput.XInputGetCapabilities(userIndex, deviceQueryType, out capabilities) == 0;
        }

        /// <summary>
        /// Retrieves a gamepad input event.
        /// </summary>
        /// <param name="userIndex">Index of the user's controller. Can be a value in the range 0–3. </param>
        /// <param name="keystroke">The keystroke.</param>
        /// <returns>False if the controller is not connected and no new keys have been pressed, true otherwise.</returns>
        public static bool GetKeystroke(int userIndex, out Keystroke keystroke)
        {
            return s_xInput.XInputGetKeystroke(userIndex, out keystroke) == 0;
        }


#if !WINDOWS_UWP
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string lpFileName);
#endif
    }
}
