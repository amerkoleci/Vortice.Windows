// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIDeviceSubObject
{
    public T GetDevice<T>() where T : IDXGIDevice
    {
        GetDevice(typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result GetDevice<T>(out T? device) where T : IDXGIDevice
    {
        Result result = GetDevice(typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
