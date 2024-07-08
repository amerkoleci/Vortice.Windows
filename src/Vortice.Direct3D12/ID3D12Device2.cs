// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public unsafe partial class ID3D12Device2
{
    public ID3D12PipelineState CreatePipelineState<TData>(TData data) where TData : unmanaged
    {

        PipelineStateStreamDescription description = new()
        {
            SizeInBytes = (nuint)sizeof(TData),
            SubObjectStream = new IntPtr(&data)
        };

        CreatePipelineState(description, typeof(ID3D12PipelineState).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12PipelineState(nativePtr);
    }

    public T CreatePipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T, TData>(TData data)
        where T : ID3D12PipelineState
        where TData : unmanaged
    {
        PipelineStateStreamDescription description = new()
        {
            SizeInBytes = (nuint)sizeof(TData),
            SubObjectStream = new IntPtr(&data)
        };

        return CreatePipelineState<T>(description);
    }

    public T CreatePipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(PipelineStateStreamDescription description) where T : ID3D12PipelineState
    {
        CreatePipelineState(description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreatePipelineState<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(PipelineStateStreamDescription description, out T? pipelineState) where T : ID3D12PipelineState
    {
        Result result = CreatePipelineState(description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            pipelineState = default;
            return result;
        }

        pipelineState = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
