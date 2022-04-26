
using Vortice.Direct3D12;

namespace Vortice.DirectML;

public static partial class DML
{
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
