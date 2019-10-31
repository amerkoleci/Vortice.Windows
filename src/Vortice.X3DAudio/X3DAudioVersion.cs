// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.X3DAudio
{
    /// <summary>
    /// An enum to select the XAudio version to load.
    /// </summary>
    public enum X3DAudioVersion
    {
        /// <summary>
        /// The default version (X3DAudio1_7.dll if it is installed, otherwise XAudio2_8.dll or from XAudio2_9.dll)
        /// </summary>
        Default,
        /// <summary>
        /// The X3DAudio1.7 version (X3DAudio1_7.dll).
        /// </summary>
        Version17,
        /// <summary>
        /// From the XAudio2.8 version (XAudio2_8.dll).
        /// </summary>
        Version28,
        /// <summary>
        /// From the XAudio2.9 version (XAudio2_9.dll).
        /// </summary>
        Version29
    }

}
