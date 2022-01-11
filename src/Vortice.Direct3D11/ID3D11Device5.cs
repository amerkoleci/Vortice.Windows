// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

public partial class ID3D11Device5
{
    public ID3D11Fence CreateFence(ulong initialValue, FenceFlags flags = FenceFlags.None)
    {
        CreateFence(initialValue, flags, typeof(ID3D11Fence).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D11Fence(nativePtr);
    }

    public T CreateFence<T>(ulong initialValue, FenceFlags flags = FenceFlags.None) where T : ID3D11Fence
    {
        CreateFence(initialValue, flags, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result CreateFence<T>(ulong initialValue, FenceFlags flags, out T? fence) where T: ID3D11Fence
    {
        Result result = CreateFence(initialValue, flags, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            fence = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        fence = default;
        return result;
    }

    public ID3D11Fence OpenSharedFence(IntPtr fenceHandle) 
    {
        OpenSharedFence(fenceHandle, typeof(ID3D11Fence).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D11Fence(nativePtr);
    }

    public T OpenSharedFence<T>(IntPtr fenceHandle) where T : ID3D11Fence
    {
        OpenSharedFence(fenceHandle, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr);
    }

    public Result OpenSharedFence<T>(IntPtr fenceHandle, out T? fence) where T : ID3D11Fence
    {
        Result result = OpenSharedFence(fenceHandle, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Success)
        {
            fence = MarshallingHelpers.FromPointer<T>(nativePtr);
            return result;
        }

        fence = default;
        return result;
    }
}
