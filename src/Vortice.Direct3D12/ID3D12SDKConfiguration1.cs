// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12SDKConfiguration1
{
    public ID3D12DeviceFactory CreateDeviceFactory(int sdkVersion, string sdkPath) 
    {
        CreateDeviceFactory(sdkVersion, sdkPath, typeof(ID3D12DeviceFactory).GUID, out IntPtr nativePtr).CheckError();
        return new(nativePtr);
    }

    public Result CreateDeviceFactory(int sdkVersion, string sdkPath, out ID3D12DeviceFactory? factory)
    {
        Result result = CreateDeviceFactory(sdkVersion, sdkPath, typeof(ID3D12DeviceFactory).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            factory = null;
            return result;
        }

        factory = new(nativePtr);
        return result;
    }

    public T CreateDeviceFactory<T>(int sdkVersion, string sdkPath) where T : ID3D12DeviceFactory
    {
        CreateDeviceFactory(sdkVersion, sdkPath, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDeviceFactory<T>(int sdkVersion, string sdkPath, out T? factory) where T : ID3D12DeviceFactory
    {
        Result result = CreateDeviceFactory(sdkVersion, sdkPath, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            factory = null;
            return result;
        }

        factory = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
