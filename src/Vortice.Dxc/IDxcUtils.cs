// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
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

        public Result CreateReflection<T>(IDxcBlob blob, out T? reflection) where T : ComObject
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

            reflection = MarshallingHelpers.FromPointer<T>(nativePtr);
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
            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }

        public IDxcCompilerArgs BuildArguments(string sourceName, string entryPoint, string targetProfile, string[] arguments, DxcDefine[] defines)
        {
            BuildArguments(sourceName, entryPoint, targetProfile, arguments, defines, out IDxcCompilerArgs? args).CheckError();
            return args!;
        }

        public IDxcCompilerArgs BuildArguments(string sourceName, string entryPoint, string targetProfile,
            string[] arguments, int argumentsCount, DxcDefine[] defines, int defineCount)
        {
            BuildArguments(sourceName, entryPoint, targetProfile, arguments, argumentsCount, defines, defineCount, out IDxcCompilerArgs? args).CheckError();
            return args!;
        }

        public unsafe Result BuildArguments(string sourceName, string entryPoint, string targetProfile,
            string[] arguments, DxcDefine[] defines, out IDxcCompilerArgs? args)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments?.Length > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, arguments.Length);
                }

                Result hr = BuildArguments(sourceName,
                    entryPoint,
                    targetProfile,
                    (IntPtr)argumentsPtr, (arguments?.Length) ?? 0,
                    defines, (defines?.Length) ?? 0,
                    out args);

                if (hr.Failure)
                {
                    args = default;
                    return default;
                }

                return hr;
            }
            finally
            {
                if (argumentsPtr != null)
                    Interop.Free(argumentsPtr);
            }
        }

        public unsafe Result BuildArguments(string sourceName, string entryPoint, string targetProfile,
            string[] arguments, int argumentsCount,
            DxcDefine[] defines, int defineCount, out IDxcCompilerArgs? args)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                Result hr = BuildArguments(sourceName,
                    entryPoint,
                    targetProfile,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines, defineCount,
                    out args);

                if (hr.Failure)
                {
                    args = default;
                    return default;
                }

                return hr;
            }
            finally
            {
                if (argumentsPtr != null)
                    Interop.Free(argumentsPtr);
            }
        }

        public IDxcBlobEncoding CreateBlob(IntPtr data, int size, int codePage)
        {
            CreateBlob(data, size, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlob CreateBlobFromBlob(IDxcBlob blob, int offset, int length)
        {
            CreateBlobFromBlob(blob, offset, length, out IDxcBlob result).CheckError();
            return result;
        }

        public IDxcBlobEncoding CreateBlobFromPinned(IntPtr data, int size, int codePage)
        {
            CreateBlobFromPinned(data, size, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlobUtf8 GetBlobAsUtf8(IDxcBlob blob)
        {
            GetBlobAsUtf8(blob, out IDxcBlobUtf8 result).CheckError();
            return result;
        }

        public IDxcBlobUtf16 GetBlobAsUtf16(IDxcBlob blob)
        {
            GetBlobAsUtf16(blob, out IDxcBlobUtf16 result).CheckError();
            return result;
        }

        public Result GetDxilContainerPart(string shaderSource, int dxcPart, out IntPtr partData, out int partSizeInBytesRef)
        {
            IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);

            DxcBuffer buffer = new DxcBuffer
            {
                Ptr = shaderSourcePtr,
                Size = shaderSource.Length,
                Encoding = Dxc.DXC_CP_ACP
            };

            try
            {
                Result hr = GetDxilContainerPart(ref buffer, dxcPart, out partData, out partSizeInBytesRef);
                if (hr.Failure)
                {
                    return hr;
                }

                return hr;
            }
            finally
            {
                if (shaderSourcePtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(shaderSourcePtr);
                }
            }
        }

        public IDxcBlob GetPDBContents(IDxcBlob pdbBlob, out IDxcBlob hash)
        {
            GetPDBContents(pdbBlob, out hash, out IDxcBlob result).CheckError();
            return result;
        }

        public IDxcBlobEncoding LoadFile(string fileName, int? codePage)
        {
            LoadFile(fileName, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlobEncoding MoveToBlob(IntPtr data, ComObject malloc, int size, int codePage)
        {
            MoveToBlob(data, malloc, size, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }
    }
}
