// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace Vortice.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PropertyKey
    {
        /// <summary>
        /// Format ID
        /// </summary>
        public readonly Guid FormatId;
        /// <summary>
        /// Property ID
        /// </summary>
        public readonly int PropertyId;

        /// <summary>
        /// <param name="formatId"></param>
        /// <param name="propertyId"></param>
        /// </summary>
        public PropertyKey(Guid formatId, int propertyId)
        {
            FormatId = formatId;
            PropertyId = propertyId;
        }
    }
}
