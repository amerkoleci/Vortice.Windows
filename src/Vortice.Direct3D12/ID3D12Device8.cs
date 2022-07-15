// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12;

public partial class ID3D12Device8
{
    public T CreateCommittedResource2<T>(
        HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription1 description,
        ResourceStates initialResourceState,
        ID3D12ProtectedResourceSession protectedSession) where T : ID3D12Resource
    {
        CreateCommittedResource2(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            null,
            protectedSession,
            typeof(T).GUID, out IntPtr nativePtr).CheckError();

        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T CreateCommittedResource2<T>(
        HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription1 description,
        ResourceStates initialResourceState,
        ClearValue optimizedClearValue,
        ID3D12ProtectedResourceSession protectedSession,
        Format[] castableFormats) where T : ID3D12Resource
    {
        CreateCommittedResource2(
            ref heapProperties,
            heapFlags,
            ref description,
            initialResourceState,
            optimizedClearValue,
            protectedSession,
            typeof(T).GUID, out IntPtr nativePtr).CheckError();

        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public T? CreatePlacedResource1<T>(ID3D12Heap heap, ulong heapOffset, ResourceDescription1 description, ResourceStates initialState, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
    {
        Result result = CreatePlacedResource1(
            heap,
            heapOffset,
            ref description,
            initialState,
            optimizedClearValue,
            typeof(T).GUID, out IntPtr nativePtr);

        if (result.Success)
        {
            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }

        return default;
    }

    public Result CreatePlacedResource1<T>(
        ID3D12Heap heap,
        ulong heapOffset,
        ResourceDescription1 description,
        ResourceStates initialState,
        out T? resource) where T : ID3D12Resource
    {
        Result result = CreatePlacedResource1(
            heap,
            heapOffset,
            ref description,
            initialState,
            null,
            typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr)!;
        return result;
    }

    public Result CreatePlacedResource1<T>(
        ID3D12Heap heap,
        ulong heapOffset,
        ResourceDescription1 description,
        ResourceStates initialState,
        ClearValue optimizedClearValue,
        out T? resource) where T : ID3D12Resource
    {
        Result result = CreatePlacedResource1(
            heap,
            heapOffset,
            ref description,
            initialState,
            optimizedClearValue,
            typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            resource = default;
            return result;
        }

        resource = MarshallingHelpers.FromPointer<T>(nativePtr)!;
        return result;
    }
}
