// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime;

namespace Vortice.MediaFoundation;

public partial class IMFDXGIDeviceManager
{
    public uint ResetToken { get; internal set; }

    public Result ResetDevice(ComObject direct3D11Device) => ResetDevice(direct3D11Device, ResetToken);

    public T LockDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr deviceHandle, bool block) where T : ComObject
    {
        LockDevice(deviceHandle, typeof(T).GUID, out IntPtr unkDevice, block).CheckError();
        return MarshallingHelpers.FromPointer<T>(unkDevice)!;
    }

    public Result LockDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr deviceHandle, bool block, out T? device) where T : ComObject
    {
        Result result = LockDevice(deviceHandle, typeof(T).GUID, out IntPtr unkDevice, block);
        if (result.Failure)
        {
            device = null;
            return result;

        }

        device = MarshallingHelpers.FromPointer<T>(unkDevice);
        return result;
    }

    public Result UnlockDevice(IntPtr hDevice) => UnlockDevice(hDevice, false);

    public T GetVideoService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr deviceHandle) where T : ComObject
    {
        GetVideoService(deviceHandle, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result GetVideoService<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr deviceHandle, out T? service) where T : ComObject
    {
        Result result = GetVideoService(deviceHandle, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            service = null;
            return result;

        }

        service = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
