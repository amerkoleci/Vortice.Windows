// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.XInput
{
    [System.Flags]
    public enum KeyStrokeFlags : short
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,
        /// <summary>
        /// The key was pressed. 
        /// </summary>
        KeyDown = 0x0001,
        /// <summary>
        /// The key was released. 
        /// </summary>
        KeyUp = 0x0002,
        /// <summary>
        /// A repeat of a held key. 
        /// </summary>
        Repeat = 0x0004,
    }
}
