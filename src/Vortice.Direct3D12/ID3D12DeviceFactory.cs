// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12DeviceFactory
{
    public T GetConfigurationInterface<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid classId) where T : ComObject
    {
        GetConfigurationInterface(classId, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result GetConfigurationInterface<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid classId, out T? @interface) where T : ComObject
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

    public T CreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ComObject? adapter, FeatureLevel featureLevel) where T : ID3D12Device
    {
        CreateDevice(adapter != null ? adapter.NativePointer : IntPtr.Zero, featureLevel, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ComObject? adapter, FeatureLevel featureLevel, out T? device) where T : ID3D12Device
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

    public T CreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr adapterPtr, FeatureLevel featureLevel) where T : ID3D12Device
    {
        CreateDevice(adapterPtr, featureLevel, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateDevice<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IntPtr adapterPtr, FeatureLevel featureLevel, out T? device) where T : ID3D12Device
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

    public Result EnableExperimentalFeatures(int numFeatures, Span<Guid> features)
    {
        fixed (Guid* pIIDs = features)
            return EnableExperimentalFeatures(numFeatures, pIIDs, null, null);
    }

    public Result EnableExperimentalFeatures(Span<Guid> features)
    {
        fixed (Guid* pIIDs = features)
            return (Result)EnableExperimentalFeatures(features.Length, pIIDs, null, null);
    }

    public Result EnableExperimentalFeatures<T>(Span<Guid> features, Span<T> configurationStructs)
        where T : unmanaged
    {
        if (features.Length != configurationStructs.Length)
            throw new InvalidOperationException($"{nameof(features)}.Length must be equal to {nameof(configurationStructs)}.Length");

        Span<int> configurationStructSizes = stackalloc int[configurationStructs.Length];
        for (int i = 0; i < configurationStructs.Length; i++)
        {
            configurationStructSizes[i] = sizeof(T);
        }

        fixed (Guid* pIIDs = features)
        fixed (void* pConfigurationStructs = configurationStructs)
        fixed (int* pConfigurationStructSizes = configurationStructSizes)
            return (Result)EnableExperimentalFeatures(features.Length, pIIDs, pConfigurationStructs, pConfigurationStructSizes);
    }
}
