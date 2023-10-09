// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12Device10
{
    public T CreateCommittedResource3<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        HeapProperties heapProperties,
        HeapFlags heapFlags,
        ResourceDescription1 description,
        BarrierLayout initialLayout,
        ClearValue? optimizedClearValue,
        ID3D12ProtectedResourceSession? protectedSession,
        Format[]? castableFormats) where T : ID3D12Resource
    {
        if (castableFormats?.Length > 0)
        {
            fixed (Format* pCastableFormats = castableFormats)
            {
                CreateCommittedResource3(
                    ref heapProperties,
                    heapFlags,
                    ref description,
                    initialLayout,
                    optimizedClearValue,
                    protectedSession,
                    castableFormats.Length,
                    pCastableFormats,
                    typeof(T).GUID, out IntPtr nativePtr).CheckError();

                return MarshallingHelpers.FromPointer<T>(nativePtr)!;
            }
        }
        else
        {
            CreateCommittedResource3(
                ref heapProperties,
                heapFlags,
                ref description,
                initialLayout,
                optimizedClearValue,
                protectedSession,
                0,
                null,
                typeof(T).GUID, out IntPtr nativePtr).CheckError();

            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }
    }

    public T CreatePlacedResource2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        ID3D12Heap heap,
        ulong heapOffset,
        ResourceDescription1 description,
        BarrierLayout initialLayout,
        ClearValue? optimizedClearValue,
        Format[]? castableFormats) where T : ID3D12Resource
    {
        if (castableFormats?.Length > 0)
        {
            fixed (Format* pCastableFormats = castableFormats)
            {
                CreatePlacedResource2(
                    heap, heapOffset,
                    ref description,
                    initialLayout,
                    optimizedClearValue,
                    castableFormats.Length, pCastableFormats,
                    typeof(T).GUID, out IntPtr nativePtr).CheckError();

                return MarshallingHelpers.FromPointer<T>(nativePtr)!;
            }
        }
        else
        {
            CreatePlacedResource2(
                heap, heapOffset,
                ref description,
                initialLayout,
                optimizedClearValue,
                0, null,
                typeof(T).GUID, out IntPtr nativePtr).CheckError();

            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }
    }

    public T CreateReservedResource2<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(
        ResourceDescription description,
        BarrierLayout initialLayout,
        ClearValue? optimizedClearValue,
        ID3D12ProtectedResourceSession? protectedSession,
        Format[]? castableFormats) where T : ID3D12Resource
    {
        if (castableFormats?.Length > 0)
        {
            fixed (Format* pCastableFormats = castableFormats)
            {
                CreateReservedResource2(
                    ref description,
                    initialLayout,
                    optimizedClearValue,
                    protectedSession,
                    castableFormats.Length,
                    pCastableFormats,
                    typeof(T).GUID, out IntPtr nativePtr).CheckError();

                return MarshallingHelpers.FromPointer<T>(nativePtr)!;
            }
        }
        else
        {
            CreateReservedResource2(
                ref description,
                initialLayout,
                optimizedClearValue,
                protectedSession,
                0, null,
                typeof(T).GUID, out IntPtr nativePtr).CheckError();

            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }
    }
}
