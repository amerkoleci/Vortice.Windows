// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    /// <summary>
    /// Describes the index buffer to view.
    /// </summary>
    public partial struct IndexBufferView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexBufferView"/> struct.
        /// </summary>
        /// <param name="bufferLocation">Specifies a gpu virtual address that identifies the address of the buffer.</param>
        /// <param name="sizeInBytes">Specifies the size in bytes of the index buffer.</param>
        /// <param name="format">Specifies the <see cref="DXGI.Format"/> for the index-buffer format.</param>
        public IndexBufferView(ulong bufferLocation, int sizeInBytes, Format format)
        {
            BufferLocation = bufferLocation;
            SizeInBytes = sizeInBytes;
            Format = format;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexBufferView"/> struct.
        /// </summary>
        /// <param name="bufferLocation">Specifies a gpu virtual address that identifies the address of the buffer.</param>
        /// <param name="sizeInBytes">Specifies the size in bytes of the index buffer.</param>
        /// <param name="is32Bit">Specifies if index buffer is 32 bit or 16 bit sized.</param>
        public IndexBufferView(ulong bufferLocation, int sizeInBytes, bool is32Bit = false)
            : this(bufferLocation, sizeInBytes, is32Bit ? Format.R32_UInt : Format.R16_UInt)
        {
        }
    }
}
