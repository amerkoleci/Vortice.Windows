// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device9
    {
        public T CreateCommandQueue1<T>(in CommandQueueDescription description, Guid creatorID) where T : ID3D12CommandQueue
        {
            CreateCommandQueue1(description, creatorID, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public Result CreateCommandQueue1<T>(in CommandQueueDescription description, Guid creatorID, out T? commandQueue) where T : ID3D12CommandQueue
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

        public T CreateShaderCacheSession<T>(ShaderCacheSessionDescription description) where T : ID3D12ShaderCacheSession
        {
            CreateShaderCacheSession(ref description, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }

        public Result CreateShaderCacheSession<T>(ShaderCacheSessionDescription description, out T? session) where T : ID3D12ShaderCacheSession
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
}
