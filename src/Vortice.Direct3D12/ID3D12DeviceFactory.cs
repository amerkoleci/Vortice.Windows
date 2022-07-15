// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D12;

public partial class ID3D12DeviceFactory
{
    public T GetConfigurationInterface<T>(Guid classId) where T : ComObject
    {
        GetConfigurationInterface(classId, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result GetConfigurationInterface<T>(Guid classId, out T? @interface) where T : ComObject
    {
        Result result = GetConfigurationInterface(classId, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            @interface = null;
            return result;
        }

        @interface = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T CreateDevice<T>(ComObject? adapter, FeatureLevel featureLevel) where T : ID3D12Device
    {
        CreateDevice(adapter != null ? adapter.NativePointer : IntPtr.Zero, featureLevel, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDevice<T>(ComObject? adapter, FeatureLevel featureLevel, out T? device) where T : ID3D12Device
    {
        Result result = CreateDevice(
            adapter != null ? adapter.NativePointer : IntPtr.Zero,
            featureLevel,
            typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            device = null;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T CreateDevice<T>(IntPtr adapterPtr, FeatureLevel featureLevel) where T : ID3D12Device
    {
        CreateDevice(adapterPtr, featureLevel, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDevice<T>(IntPtr adapterPtr, FeatureLevel featureLevel, out T? device) where T : ID3D12Device
    {
        Result result = CreateDevice(
            adapterPtr,
            featureLevel,
            typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            device = null;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
