// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12Device1
{
    public ID3D12PipelineLibrary CreatePipelineLibrary(byte[] blob)
    {
        fixed (byte* pBlob = blob)
        {
            CreatePipelineLibrary(pBlob, blob.Length, typeof(ID3D12PipelineLibrary).GUID, out IntPtr nativePtr).CheckError();
            return new ID3D12PipelineLibrary(nativePtr);
        }
    }

    public Result CreatePipelineLibrary(byte[] blob, out ID3D12PipelineLibrary? pipelineLibrary)
    {
        fixed (byte* pBlob = blob)
        {
            Result result = CreatePipelineLibrary(pBlob, blob.Length, typeof(ID3D12PipelineLibrary).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                pipelineLibrary = default;
                return result;
            }

            pipelineLibrary = new ID3D12PipelineLibrary(nativePtr);
            return result;
        }
    }

    public ID3D12PipelineLibrary CreatePipelineLibrary(Blob blob)
    {
        CreatePipelineLibrary(blob.BufferPointer.ToPointer(), blob.BufferSize, typeof(ID3D12PipelineLibrary).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12PipelineLibrary(nativePtr);
    }

    public Result CreatePipelineLibrary(Blob blob, out ID3D12PipelineLibrary? pipelineLibrary)
    {
        Result result = CreatePipelineLibrary(blob.BufferPointer.ToPointer(), blob.BufferSize, typeof(ID3D12PipelineLibrary).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineLibrary = default;
            return result;
        }

        pipelineLibrary = new ID3D12PipelineLibrary(nativePtr);
        return result;
    }

    public T CreatePipelineLibrary<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Blob blob) where T : ID3D12PipelineLibrary
    {
        CreatePipelineLibrary(blob.BufferPointer.ToPointer(), blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreatePipelineLibrary<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Blob blob, out T? pipelineLibrary) where T : ID3D12PipelineLibrary
    {
        Result result = CreatePipelineLibrary(blob.BufferPointer.ToPointer(), blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineLibrary = default;
            return result;
        }

        pipelineLibrary = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
