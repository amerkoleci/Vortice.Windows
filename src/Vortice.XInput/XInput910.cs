// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    internal unsafe class XInput910 : IXInput
    {
        int IXInput.XInputGetState(int userIndex, out State state)
        {
            return XInputGetState(userIndex, out state);
        }

        int IXInput.XInputSetState(int userIndex, Vibration vibration)
        {
            return XInputSetState(userIndex, &vibration);
        }

        void IXInput.XInputEnable(int enable)
        {
            throw new NotSupportedException("XInputEnable is not supported on XInput9.1.0");
        }

        int IXInput.XInputGetCapabilities(int dwUserIndex, DeviceQueryType dwFlags, out Capabilities capabilities)
        {
            return XInputGetCapabilities(dwUserIndex, dwFlags, out capabilities);
        }

        int IXInput.XInputGetBatteryInformation(int dwUserIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation)
        {
            throw new NotSupportedException("XInputGetBatteryInformation is not supported on XInput9.1.0");
        }

        int IXInput.XInputGetKeystroke(int dwUserIndex, out Keystroke keystroke)
        {
            throw new NotSupportedException("XInputGetKeystroke is not supported on XInput9.1.0");
        }

        int IXInput.XInputGetAudioDeviceIds(int dwUserIndex, IntPtr renderDeviceId, IntPtr renderCount, IntPtr captureDeviceId, IntPtr captureCount)
        {
            throw new NotSupportedException("XInputGetAudioDeviceIds is not supported on XInput9.1.0");
        }


        [DllImport("xinput9_1_0.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetState(int dwUserIndex, out State state);

        [DllImport("xinput9_1_0.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputSetState(int dwUserIndex, Vibration* pVibration);

        [DllImport("xinput9_1_0.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputGetCapabilities(int dwUserIndex, DeviceQueryType dwFlags, out Capabilities capabilities);
    }
}
