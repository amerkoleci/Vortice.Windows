// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12DSRDeviceFactory
{
    public T CreateDSRDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ID3D12Device device, uint nodeMask) where T : ComObject
    {
        CreateDSRDevice(device, nodeMask, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDSRDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ID3D12Device device, uint nodeMask, out T? DSRDevice) where T : ComObject
    {
        Result result = CreateDSRDevice(
            device,
            nodeMask,
            typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            DSRDevice = null;
            return result;
        }

        DSRDevice = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
