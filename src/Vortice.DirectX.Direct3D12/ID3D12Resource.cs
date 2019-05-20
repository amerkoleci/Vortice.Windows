// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.DirectX.Direct3D12
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
            int destinationSubresource, Box destinationBox,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                WriteToSubresource(destinationSubresource, destinationBox,
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
