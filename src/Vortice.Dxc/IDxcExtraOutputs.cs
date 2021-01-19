// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcExtraOutputs
    {
        public T GetOutput<T>(int index, out IDxcBlobUtf16 outputType, out IDxcBlobUtf16 outputName) where T : IDxcBlob
        {
            Result result = GetOutput(index, typeof(T).GUID, out IntPtr nativePtr, out outputType, out outputName);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }

        public Result GetOutput<T>(int index, out T @object, out IDxcBlobUtf16 outputType, out IDxcBlobUtf16 outputName) where T : IDxcBlob
        {
            Result result = GetOutput(index, typeof(T).GUID, out IntPtr nativePtr, out outputType, out outputName);
            if (result.Failure)
            {
                @object = default;
                return result;
            }

            @object = FromPointer<T>(nativePtr);
            return result;
        }
    }
}
