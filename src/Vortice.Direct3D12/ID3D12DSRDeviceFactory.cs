// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12DSRDeviceFactory
{
    public T CreateDSRDevice<T>(ID3D12Device d3D12Device, int nodeMask = 0) where T : ComObject
    {
        CreateDSRDevice(d3D12Device, nodeMask, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDSRDevice<T>(ID3D12Device d3D12Device, int nodeMask, out T? DSRDevice) where T : ComObject
    {
        Result result = CreateDSRDevice(d3D12Device, nodeMask, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            DSRDevice = null;
            return result;
        }

        DSRDevice = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
