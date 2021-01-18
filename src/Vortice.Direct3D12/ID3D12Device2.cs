// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device2
    {
        public unsafe T CreatePipelineState<T, TData>(TData data)
            where T : ID3D12PipelineState
            where TData : unmanaged
        {
            PipelineStateStreamDescription description = new PipelineStateStreamDescription
            {
                SizeInBytes = sizeof(TData),
                SubObjectStream = new IntPtr(&data)
            };

            return CreatePipelineState<T>(description);
        }

        public unsafe T CreatePipelineState<T>(byte[] data) where T : ID3D12PipelineState
        {
            fixed (byte* dataPtr = &data[0])
            {
                PipelineStateStreamDescription description = new PipelineStateStreamDescription
                {
                    SizeInBytes = data.Length,
                    SubObjectStream = (IntPtr)dataPtr
                };

                return CreatePipelineState<T>(description);
            }
        }

        public T CreatePipelineState<T>(PipelineStateStreamDescription description) where T : ID3D12PipelineState
        {
            Result result = CreatePipelineState(ref description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreatePipelineState<T>(PipelineStateStreamDescription description, out T pipelineState) where T : ID3D12PipelineState
        {
            Result result = CreatePipelineState(ref description, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                pipelineState = default;
                return result;
            }

            pipelineState = FromPointer<T>(nativePtr);
            return result;
        }
    }
}
