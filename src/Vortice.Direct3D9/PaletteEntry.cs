// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Direct3D9
{
    /// <summary>
    /// The PaletteEntry struct contains the color and usage of an entry in a logical palette.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct PaletteEntry
    {
        /// <summary>
        /// The red intensity value for the palette entry.
        /// </summary>
        public byte Red;

        /// <summary>
        /// The green intensity value for the palette entry.
        /// </summary>
        public byte Green;

        /// <summary>
        /// The blue intensity value for the palette entry.
        /// </summary>
        public byte Blue;

        /// <summary>
        /// Indicates how the palette entry is to be used. 
        /// TODO define an enum for flags
        /// </summary>
        public byte Flags;
    }
}
