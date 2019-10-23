// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.DXGI;

namespace Vortice.Direct3D11
{
    /// <summary>
    /// Provides access to subresource data.
    /// </summary>
    public partial struct MappedSubresource
    {
        /// <summary>
        /// Create a new instance of <see cref="MappedSubresource"/> struct.
        /// </summary>
        /// <param name="dataPointer">Pointer to the data</param>
        /// <param name="rowPitch">The row pitch, or width, or physical size (in bytes) of the data.</param>
        /// <param name="depthPitch">The depth pitch, or width, or physical size (in bytes)of the data.</param>
        public MappedSubresource(IntPtr dataPointer, int rowPitch, int depthPitch)
        {
            DataPointer = dataPointer;
            RowPitch = rowPitch;
            DepthPitch = depthPitch;
        }

        /// <summary>
        /// Create a new instance of <see cref="MappedSubresource"/> struct.
        /// </summary>
        /// <param name="dataPointer">Pointer to the data</param>
        public MappedSubresource(IntPtr dataPointer)
        {
            DataPointer = dataPointer;
            RowPitch = DepthPitch = 0;
        }

        public unsafe Span<T> AsSpan<T>(int sizeInBytes) => new Span<T>(DataPointer.ToPointer(), sizeInBytes);
        public unsafe Span<T> AsSpan<T>(ID3D11Buffer buffer) => new Span<T>(DataPointer.ToPointer(), buffer.Description.SizeInBytes);
        public unsafe Span<T> AsSpan<T>(ID3D11Texture1D resource, int mipSlice, int arraySlice)
        {
            resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
            return new Span<T>(DataPointer.ToPointer(), mipSize * resource.Description.Format.SizeOfInBytes());
        }

        public unsafe Span<T> AsSpan<T>(ID3D11Texture2D resource, int mipSlice, int arraySlice)
        {
            resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
            return new Span<T>(DataPointer.ToPointer(), mipSize * RowPitch);
        }

        public unsafe Span<T> AsSpan<T>(ID3D11Texture3D resource, int mipSlice, int arraySlice)
        {
            resource.CalculateSubResourceIndex(mipSlice, arraySlice, out int mipSize);
            return new Span<T>(DataPointer.ToPointer(), mipSize * DepthPitch);
        }
    }
}
