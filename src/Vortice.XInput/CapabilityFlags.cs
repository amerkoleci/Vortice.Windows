// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.XInput
{
    /// <summary>
    /// Describes the features of the controller.
    /// </summary>
    [Flags]
    public enum CapabilityFlags : short
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Device supports force feedback functionality. Note that these force-feedback features beyond rumble are not currently supported through XINPUT on Windows.
        /// </summary>
        FfbSupported = 0x1,
        /// <summary>
        /// Device is wireless.
        /// </summary>
        Wireless = 0x2,
        /// <summary>
        /// Device has an integrated voice device.
        /// </summary>
        VoiceSupported = 0x4,
        /// <summary>
        /// Device supports plug-in modules. Note that plug-in modules like the text input device (TID) are not supported currently through XINPUT on Windows.
        /// </summary>
        PmdSupported = 0x8,
        /// <summary>
        /// Device lacks menu navigation buttons (START, BACK, DPAD).
        /// </summary>
        NoNavigation = 0x10,
    }
}
