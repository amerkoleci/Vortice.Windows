// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DXGI
{
    /// <summary>
    /// Describes a JPEG quantization table.
    /// </summary>
    public unsafe partial struct JpegQuantizationTable
    {
        /// <summary>
        /// An array of bytes containing the elements of the quantization table.
        /// </summary>
        public fixed byte Elements[64];
    }
}
