// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XInput
{
    /// <summary>
    /// Describes the library version.
    /// </summary>
    public enum XInputVersion
    {
        /// <summary>
        /// XInput 1.4 ships as part of Windows 8. Use this version for building Windows Store apps or if your desktop app requires Windows 8.
        /// </summary>
        Version14,
        /// <summary>
        /// XInput 9.1.0 ships as part of Windows Vista, Windows 7, and Windows 8. Use this version if your desktop app is intended to run on these versions of Windows and you are using basic XInput functionality.
        /// </summary>
        Version910,
        /// <summary>
        /// XInput 1.3 ships as a redistributable component in the DirectX SDK with support for Windows Vista, Windows 7, and Windows 8. Use this version if your desktop app is intended to run on these versions of Windows and you need functionality that is not supported by XInput 9.1.0.
        /// </summary>
        Version13
    }
}
