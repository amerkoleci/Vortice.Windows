// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Resource
    {
        public HeapProperties HeapProperties
        {
            get
            {
                GetHeapProperties(out HeapProperties properties, out var heapFlags);
                return properties;
            }
        }

        public HeapFlags HeapFlags
        {
            get
            {
                GetHeapProperties(out var properties, out HeapFlags heapFlags);
                return heapFlags;
            }
        }

        /// <summary>
        /// Calculates a subresource index for a texture.
        /// </summary>
        /// <param name="mipSlice">The zero-based index for the mipmap level to address; 0 indicates the first, most detailed mipmap level.</param>
        /// <param name="arraySlice">The zero-based index for the array level to address; always use 0 for volume (3D) textures.</param>
        /// <param name="planeSlice">The zero-based index for the plane level to address.</param>
        /// <param name="mipLevels">The number of mipmap levels in the resource.</param>
        /// <param name="arraySize">The number of elements in the array.</param>
        /// <returns>
        /// The index which equals mipSlice + arraySlice * mipLevels + planeSlice * mipLevels * arraySize.
        /// </returns>
        public static int CalculateSubResourceIndex(int mipSlice, int arraySlice, int planeSlice, int mipLevels, int arraySize)
        {
            return mipSlice + arraySlice * mipLevels + planeSlice * mipLevels * arraySize;
        }

        public unsafe void WriteToSubresource<T>(
            int destinationSubresource,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                WriteToSubresource(destinationSubresource, null,
                    (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                    );
            }
        }

        public unsafe void WriteToSubresource<T>(
            int destinationSubresource, in Offset3D destinationOffset, in Extent3D destinationExtent,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                WriteToSubresource(destinationSubresource, new Box(destinationOffset, destinationExtent),
                    (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                    );
            }
        }

        public unsafe void WriteToSubresource<T>(
            int destinationSubresource,
            T[] sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            WriteToSubresource(
                destinationSubresource, null,
                (IntPtr)Unsafe.AsPointer(ref sourceData[0]), sourceRowPitch, srcDepthPitch
                );
        }

        public unsafe void WriteToSubresource<T>(
            int destinationSubresource, Box destinationBox,
            T[] sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            WriteToSubresource(
                destinationSubresource, destinationBox,
                (IntPtr)Unsafe.AsPointer(ref sourceData[0]), sourceRowPitch, srcDepthPitch
                );
        }

        public unsafe void ReadFromSubresource<T>(
            T[] destination, int destinationRowPitch, int destinationDepthPitch,
            int sourceSubresource, Box? sourceBox = null) where T : unmanaged
        {
            ReadFromSubresource(
                (IntPtr)Unsafe.AsPointer(ref destination[0]), destinationRowPitch, destinationDepthPitch,
                sourceSubresource, sourceBox);
        }

        public static int CalculateSubresource(int mipSlice, int arraySlice, int planeSlice, int mipLevels, int arraySize)
        {
            return mipSlice + (arraySlice * mipLevels) + (planeSlice * mipLevels * arraySize);
        }
    }
}
