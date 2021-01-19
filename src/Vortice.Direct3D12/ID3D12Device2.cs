// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device2
    {
        public unsafe ID3D12PipelineState CreatePipelineState<TData>(TData data) where TData : unmanaged
        {
            PipelineStateStreamDescription description = new PipelineStateStreamDescription
            {
                SizeInBytes = sizeof(TData),
                SubObjectStream = new IntPtr(&data)
            };

            return CreatePipelineState<ID3D12PipelineState>(description);
        }

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
