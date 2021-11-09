// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    internal class XInput13 : IXInput
    {
        int IXInput.XInputGetState(int userIndex, out State state)
        {
            return XInputGetState(userIndex, out state);
        }

        int IXInput.XInputSetState(int userIndex, Vibration vibration)
        {
            return XInputSetState(userIndex, ref vibration);
        }

        void IXInput.XInputEnable(int enable)
        {
            XInputEnable(enable);
        }

        int IXInput.XInputGetCapabilities(int dwUserIndex, DeviceQueryType dwFlags, out Capabilities capabilities)
        {
            return XInputGetCapabilities(dwUserIndex, dwFlags, out capabilities);
        }

        int IXInput.XInputGetBatteryInformation(int dwUserIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation)
        {
            return XInputGetBatteryInformation(dwUserIndex, devType, out batteryInformation);
        }

        int IXInput.XInputGetKeystroke(int dwUserIndex, out Keystroke keystroke)
        {
            return XInputGetKeystroke(dwUserIndex, 0, out keystroke);
        }

        int IXInput.XInputGetAudioDeviceIds(int dwUserIndex, IntPtr renderDeviceId, IntPtr renderCount, IntPtr captureDeviceId, IntPtr captureCount)
        {
            throw new NotSupportedException("XInputGetAudioDeviceIds is not supported on XInput1.3");
        }

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetState(int dwUserIndex, out State state);

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputSetState(int dwUserIndex, ref Vibration pVibration);

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void XInputEnable(int enable);

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetCapabilities(int dwUserIndex, DeviceQueryType dwFlags, out Capabilities capabilities);

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetBatteryInformation(int dwUserIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation);

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetKeystroke(int dwUserIndex, int dwReserved, out Keystroke keystroke);
    }
}
