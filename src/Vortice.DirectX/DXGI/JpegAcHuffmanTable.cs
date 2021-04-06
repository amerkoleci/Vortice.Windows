// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DXGI
{
    /// <summary>
    /// Describes a JPEG DC huffman table.
    /// </summary>
    public unsafe partial struct JpegAcHuffmanTable
    {
        /// <summary>
        /// The number of codes for each code length.
        /// </summary>
        public fixed byte CodeCounts[12];
        /// <summary>
        /// The Huffman code values, in order of increasing code length.
        /// </summary>
        public fixed byte CodeValues[12];
    }
}
