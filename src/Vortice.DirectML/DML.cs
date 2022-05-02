// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;

namespace Vortice.DirectML;

public static partial class DML
{
    public static IDMLDevice DMLCreateDevice(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags)
    {
        DMLCreateDevice(d3d12Device, createDeviceFlags, typeof(IDMLDevice).GUID, out IntPtr nativePtr).CheckError();
        return new(nativePtr);
    }

    public static T DMLCreateDevice<T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags)
        where T : IDMLDevice
    {
        DMLCreateDevice(d3d12Device, createDeviceFlags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public static Result DMLCreateDevice<T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags, out T? device)
        where T : IDMLDevice
    {
        Result result = DMLCreateDevice(
            d3d12Device,
            createDeviceFlags,
            typeof(T).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public static T DMLCreateDevice<T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags, FeatureLevel minimumFeatureLevel)
        where T : IDMLDevice
    {
        DMLCreateDevice1(
            d3d12Device,
            createDeviceFlags,
            minimumFeatureLevel,
            typeof(T).GUID,
            out IntPtr nativePtr).CheckError();

        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public static Result DMLCreateDevice<T>(ID3D12Device d3d12Device, CreateDeviceFlags createDeviceFlags, FeatureLevel minimumFeatureLevel, out T? device)
        where T : IDMLDevice
    {
        Result result = DMLCreateDevice1(
            d3d12Device,
            createDeviceFlags,
            minimumFeatureLevel,
            typeof(T).GUID,
            out IntPtr nativePtr);

        if (result.Failure)
        {
            device = default;
            return result;
        }

        device = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
