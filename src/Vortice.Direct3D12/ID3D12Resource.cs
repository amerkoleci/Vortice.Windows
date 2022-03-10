// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12Resource
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

    public Result Map(int subresource, void* data) => Map(subresource, default, data);

    public Span<T> Map<T>(int subresource, int length) where T : unmanaged
    {
        void* data;
        Map(subresource, null, &data).CheckError();
        return new Span<T>(data, length);
    }

    public T* Map<T>(int subresource) where T : unmanaged
    {
        T* data;
        Map(subresource, null, &data).CheckError();
        return data;
    }

    public void SetData<T>(T[] source, int offsetInBytes = 0) where T : unmanaged
    {
        ReadOnlySpan<T> span = source.AsSpan();
        SetData(span, offsetInBytes);
    }

    public void SetData<T>(ReadOnlySpan<T> source, int offsetInBytes = 0) where T : unmanaged
    {
        SetData(ref MemoryMarshal.GetReference(source), (uint)(source.Length * sizeof(T)), offsetInBytes);
    }

    public void SetData<T>(ref T source, uint sizeInBytes, int offsetInBytes = 0) where T : unmanaged
    {
        void* pMappedData;
        Map(0, default, &pMappedData).CheckError();
        fixed (void* sourcePointer = &source)
        {
            Unsafe.CopyBlockUnaligned((byte*)pMappedData + offsetInBytes, sourcePointer, sizeInBytes);
        }
        Unmap(0);
    }

    public T GetData<T>(int offsetInBytes = 0) where T : unmanaged
    {
        T data = new();
        GetData(ref data, (uint)sizeof(T), offsetInBytes);
        return data;
    }

    public void GetData<T>(Span<T> destination, int offsetInBytes = 0) where T : unmanaged
    {
        GetData(ref MemoryMarshal.GetReference(destination), (uint)(destination.Length * sizeof(T)), offsetInBytes);
    }

    //public T[] GetArray<T>(int offsetInBytes = 0) where T : unmanaged
    //{
    //    T[] data = new T[(SizeInBytes / Unsafe.SizeOf<T>()) - offsetInBytes];
    //    GetData(data.AsSpan(), offsetInBytes);
    //
    //    return data;
    //}

    public void GetData<T>(ref T destination, uint sizeInBytes, int offsetInBytes = 0) where T : unmanaged
    {
        void* pMappedData;
        Map(0, default, &pMappedData).CheckError();
        fixed (T* destinationPointer = &destination)
        {
            Unsafe.CopyBlockUnaligned(destinationPointer, (byte*)pMappedData + offsetInBytes, sizeInBytes);
        }
        Unmap(0);
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
        int destinationSubresource, in Int3 destinationOffset, in Size3 destinationExtent,
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
