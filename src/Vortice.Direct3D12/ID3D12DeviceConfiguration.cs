// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12DeviceConfiguration
{
    public ID3D12VersionedRootSignatureDeserializer CreateVersionedRootSignatureDeserializer(IntPtr blob, PointerSize size) 
    {
        CreateVersionedRootSignatureDeserializer(blob.ToPointer(), size, typeof(ID3D12VersionedRootSignatureDeserializer).GUID, out IntPtr nativePtr).CheckError();
        return new(nativePtr)!;
    }

    public Result CreateVersionedRootSignatureDeserializer(IntPtr blob, PointerSize size, out ID3D12VersionedRootSignatureDeserializer? deserializer) 
    {
        Result result = CreateVersionedRootSignatureDeserializer(blob.ToPointer(), size, typeof(ID3D12VersionedRootSignatureDeserializer).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            deserializer = default;
            return result;
        }

        deserializer = new(nativePtr);
        return result;
    }

    public T CreateVersionedRootSignatureDeserializer<T>(IntPtr blob, PointerSize size) where T : ID3D12VersionedRootSignatureDeserializer
    {
        CreateVersionedRootSignatureDeserializer(blob.ToPointer(), size, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateVersionedRootSignatureDeserializer<T>(IntPtr blob, PointerSize size, out T? deserializer) where T : ID3D12VersionedRootSignatureDeserializer
    {
        Result result = CreateVersionedRootSignatureDeserializer(blob.ToPointer(), size, typeof(T).GUID, out IntPtr nativePtr);

        if (result.Failure)
        {
            deserializer = default;
            return result;
        }

        deserializer = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
