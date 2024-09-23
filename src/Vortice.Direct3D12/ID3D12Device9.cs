// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Device9
{
    public ID3D12CommandQueue CreateCommandQueue1(in CommandQueueDescription description, Guid creatorID)
    {
        CreateCommandQueue1(description, creatorID, typeof(ID3D12CommandQueue).GUID, out IntPtr nativePtr).CheckError();
        return new ID3D12CommandQueue(nativePtr);
    }

    public Result CreateCommandQueue1(in CommandQueueDescription description, Guid creatorID, out ID3D12CommandQueue? commandQueue)
    {
        Result result = CreateCommandQueue1(description, creatorID, typeof(ID3D12CommandQueue).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandQueue = default;
            return result;
        }

        commandQueue = new ID3D12CommandQueue(nativePtr);
        return result;
    }

    public T CreateCommandQueue1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in CommandQueueDescription description, Guid creatorID) where T : ID3D12CommandQueue
    {
        CreateCommandQueue1(description, creatorID, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateCommandQueue1<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in CommandQueueDescription description, Guid creatorID, out T? commandQueue) where T : ID3D12CommandQueue
    {
        Result result = CreateCommandQueue1(description, creatorID, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            commandQueue = default;
            return result;
        }

        commandQueue = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T CreateShaderCacheSession<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ShaderCacheSessionDescription description) where T : ID3D12ShaderCacheSession
    {
        CreateShaderCacheSession(ref description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public Result CreateShaderCacheSession<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ShaderCacheSessionDescription description, out T? session) where T : ID3D12ShaderCacheSession
    {
        Result result = CreateShaderCacheSession(ref description, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            session = default;
            return result;
        }

        session = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }
}
