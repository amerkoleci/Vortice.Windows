// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device1
    {
        public T CreatePipelineLibrary<T>(Blob blob) where T : ID3D12PipelineLibrary
        {
            Result result = CreatePipelineLibrary(blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
                return default;

            return FromPointer<T>(nativePtr);
        }

        public Result CreatePipelineLibrary<T>(Blob blob, out T pipelineLibrary) where T : ID3D12PipelineLibrary
        {
            Result result = CreatePipelineLibrary(blob.BufferPointer, blob.BufferSize, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                pipelineLibrary = default;
                return result;
            }

            pipelineLibrary = FromPointer<T>(nativePtr);
            return result;
        }
    }
}
