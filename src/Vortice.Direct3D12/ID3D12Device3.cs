// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Device3
{
    /// <summary>
    /// Asynchronously makes objects resident for the device.
    /// </summary>
    public Result EnqueueMakeResident(ResidencyFlags flags, ID3D12Pageable[] objects, ID3D12Fence fenceToSignal, ulong fenceValueToSignal)
    {
        return EnqueueMakeResident(flags, (uint)(objects?.Length ?? 0), objects, fenceToSignal, fenceValueToSignal);
    }

    /// <summary>
    /// Creates a special-purpose diagnostic heap in system memory from an address.
    /// The created heap can persist even in the event of a GPU-fault or device-removed scenario.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="address">The address used to create the heap.</param>
    /// <returns></returns>
    public T OpenExistingHeapFromAddress<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr address) where T : ID3D12Heap
    {
        OpenExistingHeapFromAddress(address, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result OpenExistingHeapFromAddress<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr address, out T? heap) where T : ID3D12Heap
    {
        Result result = OpenExistingHeapFromAddress(address, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            heap = default;
            return result;
        }

        heap = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T OpenExistingHeapFromFileMapping<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr fileMapping) where T : ID3D12Heap
    {
        OpenExistingHeapFromFileMapping(fileMapping, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result OpenExistingHeapFromFileMapping<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr fileMapping, out T? heap) where T : ID3D12Heap
    {
        Result result = OpenExistingHeapFromFileMapping(fileMapping, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            heap = default;
            return result;
        }

        heap = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
