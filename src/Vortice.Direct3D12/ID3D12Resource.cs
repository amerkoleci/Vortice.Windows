// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Resource
    {
        public HeapProperties HeapProperties
        {
            get
            {
                GetHeapProperties(out HeapProperties properties, out _);
                return properties;
            }
        }

        public HeapFlags HeapFlags
        {
            get
            {
                GetHeapProperties(out _, out HeapFlags heapFlags);
                return heapFlags;
            }
        }

        public unsafe Result Map(int subresource, void* data) => Map(subresource, null, data);

        public unsafe Span<T> Map<T>(int subresource, int length) where T : unmanaged
        {
            void* data;
            Map(subresource, null, &data).CheckError();
            return new Span<T>(data, length);
        }

        public unsafe T* Map<T>(int subresource) where T : unmanaged
        {
            T* data;
            Map(subresource, null, &data).CheckError();
            return data;
        }

        public ulong GetRequiredIntermediateSize(int firstSubresource, int numSubresources)
        {
            ResourceDescription desc = GetDescription();

            using (ID3D12Device? device = GetDevice<ID3D12Device>())
            {
                device!.GetCopyableFootprints(desc, firstSubresource, numSubresources, 0, out ulong requiredSize);
                return requiredSize;
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

        public unsafe Result WriteToSubresource<T>(
            int destinationSubresource,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                return WriteToSubresource(destinationSubresource, null, (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch);
            }
        }

        public unsafe Result WriteToSubresource<T>(
            int destinationSubresource, in Point3 destinationOffset, in Size3 destinationExtent,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                return WriteToSubresource(destinationSubresource, new Box(destinationOffset, destinationExtent),
                    (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                    );
            }
        }

        public unsafe Result WriteToSubresource<T>(
            int destinationSubresource,
            T[] sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* sourceDataPtr = &sourceData[0])
            {
                return WriteToSubresource(destinationSubresource, null, (IntPtr)sourceDataPtr, sourceRowPitch, srcDepthPitch);
            }
        }

        public unsafe Result WriteToSubresource<T>(
            int destinationSubresource, Box destinationBox,
            T[] sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* sourceDataPtr = &sourceData[0])
            {
                return WriteToSubresource(destinationSubresource, destinationBox, (IntPtr)sourceDataPtr, sourceRowPitch, srcDepthPitch);
            }
        }

        public unsafe Result ReadFromSubresource<T>(
            T[] destination, int destinationRowPitch, int destinationDepthPitch,
            int sourceSubresource, Box? sourceBox = null) where T : unmanaged
        {
            fixed (void* destinationPtr = &destination[0])
            {
                return ReadFromSubresource((IntPtr)destinationPtr, destinationRowPitch, destinationDepthPitch, sourceSubresource, sourceBox);
            }
        }
    }
}
