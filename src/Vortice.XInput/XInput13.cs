// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    internal unsafe class XInput13 : IXInput
    {
        int IXInput.XInputGetState(int userIndex, out State state)
        {
            return XInputGetState(userIndex, out state);
        }

        int IXInput.XInputSetState(int userIndex, Vibration vibration)
        {
            return XInputSetState(userIndex, &vibration);
        }

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int XInputGetState(int dwUserIndex, out State state);

        [DllImport("xinput1_3.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int XInputSetState(int dwUserIndex, Vibration* pVibration);
    }
}
