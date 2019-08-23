// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace Vortice.XInput
{
    /// <summary>
    /// Specifies keystroke data returned by <strong>XInputGetKeystroke</strong>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct Keystroke
    {
        /// <summary>
        /// No documentation.
        /// </summary>
        public GamepadVirtualKey VirtualKey;

        /// <summary>
        /// This member is unused and the value is zero.
        /// </summary>
        public char Unicode;

        /// <summary>
        /// Flags that indicate the keyboard state at the time of the input event. 
        /// </summary>
        public KeyStrokeFlags Flags;

        /// <summary>
        /// Index of the signed-in gamer associated with the device. Can be a value in the range 0–3.
        /// </summary>
        public int UserIndex;

        /// <summary>
        /// HID code corresponding to the input. If there is no corresponding HID code, this value is zero.
        /// </summary>
        public byte HidCode;
    }
}
