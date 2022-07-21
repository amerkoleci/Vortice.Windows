// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D11;

public partial class ID3D11Device1
{
    public ID3D11DeviceContext1 CreateDeferredContext1()
    {
        return CreateDeferredContext1(0);
    }

    public ID3DDeviceContextState CreateDeviceContextState<T>(CreateDeviceContextStateFlags flags, FeatureLevel[] featureLevels, out FeatureLevel chosenFeatureLevel) where T : ComObject
    {
        return CreateDeviceContextState(
            flags, featureLevels, featureLevels.Length,
            D3D11.SdkVersion,
            typeof(T).GUID, out chosenFeatureLevel);
    }

    /// <summary>
    /// Gives a device access to a shared resource that is referenced by a handle and that was created on a different device.
    /// </summary>
    /// <typeparam name="T">A handle to the resource to open.</typeparam>
    /// <param name="handle"></param>
    /// <returns></returns>
    public T OpenSharedResource1<T>(IntPtr handle) where T : ID3D11Resource
    {
        OpenSharedResource1(handle, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result OpenSharedResource1<T>(IntPtr handle, out T? resource) where T : ID3D11Resource
    {
        Result result = OpenSharedResource1(handle, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            resource = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        resource = default;
        return result;
    }

    public T OpenSharedResourceByName<T>(string name, SharedResourceFlags access) where T : ID3D11Resource
    {
        OpenSharedResourceByName(name, (int)access, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result OpenSharedResourceByName<T>(string name, SharedResourceFlags access, out T? resource) where T : ID3D11Resource
    {
        Result result = OpenSharedResourceByName(name, (int)access, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            resource = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        resource = default;
        return result;
    }
}
