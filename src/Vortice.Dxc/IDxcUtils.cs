// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcUtils
    {
        public IDxcIncludeHandler CreateDefaultIncludeHandler()
        {
            CreateDefaultIncludeHandler(out IDxcIncludeHandler handler).CheckError();
            return handler;
        }

        public Result CreateReflection<T>(IDxcBlob blob, out T reflection) where T : ComObject
        {
            DxcBuffer reflectionData = new DxcBuffer
            {
                Ptr = blob.GetBufferPointer(),
                Size = blob.GetBufferSize(),
                Encoding = Dxc.DXC_CP_ACP
            };

            Result result = CreateReflection(ref reflectionData, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Failure)
            {
                reflection = default;
                return result;
            }

            reflection = FromPointer<T>(nativePtr);
            return result;
        }

        public T CreateReflection<T>(IDxcBlob blob) where T : ComObject
        {
            DxcBuffer reflectionData = new DxcBuffer
            {
                Ptr = blob.GetBufferPointer(),
                Size = blob.GetBufferSize(),
                Encoding = Dxc.DXC_CP_ACP
            };

            CreateReflection(ref reflectionData, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return FromPointer<T>(nativePtr);
        }
    }
}
