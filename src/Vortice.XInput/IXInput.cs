// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.XInput
{
    internal interface IXInput
    {
        int XInputGetState(int userIndex, out State state);
        int XInputSetState(int userIndex, Vibration vibration);
        void XInputEnable(int enable);
        int XInputGetCapabilities(int dwUserIndex, DeviceQueryType dwFlags, out Capabilities capabilities);
        int XInputGetBatteryInformation(int dwUserIndex, BatteryDeviceType devType, out BatteryInformation batteryInformation);
        int XInputGetKeystroke(int dwUserIndex, out Keystroke keystroke);
        int XInputGetAudioDeviceIds(int dwUserIndex, IntPtr renderDeviceId, IntPtr renderCount, IntPtr captureDeviceId, IntPtr captureCount);
    }
}
