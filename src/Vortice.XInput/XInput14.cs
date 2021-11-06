// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    internal class XInput14 : IXInput
    {
        private Func<bool> allowUnofficialAPI;

        internal XInput14(Func<bool> allowUnofficialAPI)
        {
            this.allowUnofficialAPI = allowUnofficialAPI;
        }

        int IXInput.XInputGetState(int userIndex, out State state)
        {
            return allowUnofficialAPI() ? XInputGetStateUnofficial(userIndex, out state) : XInputGetState(userIndex, out state);
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
            return XInputGetAudioDeviceIds(dwUserIndex, renderDeviceId, renderCount, captureDeviceId, captureCount);
        }

        [DllImport("xinput1_4.dll", EntryPoint = "#100", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetStateUnofficial(int dwUserIndex, out State state);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetState(int dwUserIndex, out State state);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputSetState(int dwUserIndex, ref Vibration pVibration);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void XInputEnable(int enable);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetCapabilities(int dwUserIndex, DeviceQueryType dwFlags, out Capabilities capabilities);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetBatteryInformation(int dwUserIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetKeystroke(int dwUserIndex, int dwReserved, out Keystroke keystroke);

        [DllImport("xinput1_4.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetAudioDeviceIds(int dwUserIndex, IntPtr renderDeviceId, IntPtr renderCount, IntPtr captureDeviceId, IntPtr captureCount);
    }
}
