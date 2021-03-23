// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcResult
    {
        public string GetErrors()
        {
            using (IDxcBlobUtf8? errors = GetOutput<IDxcBlobUtf8>(DxcOutKind.Errors))
            {
                return errors!.StringPointer;
            }
        }

        public Span<byte> GetObjectBytecode()
        {
            using (IDxcBlob? blob = GetOutput(DxcOutKind.Object))
            {
                return blob!.AsByte();
            }
        }

        public Span<byte> GetObjectBytecode(out IDxcBlobUtf16? outputName)
        {
            using (IDxcBlob? blob = GetOutput<IDxcBlob>(DxcOutKind.Object, out outputName))
            {
                return blob!.AsByte();
            }
        }

        public byte[] GetObjectBytecodeArray()
        {
            using (IDxcBlob? blob = GetOutput(DxcOutKind.Object))
            {
                return blob!.ToArray();
            }
        }

        public byte[] GetObjectBytecodeArray(out IDxcBlobUtf16? outputName)
        {
            using (IDxcBlob? blob = GetOutput<IDxcBlob>(DxcOutKind.Object, out outputName))
            {
                return blob!.ToArray();
            }
        }

        public IDxcBlob? GetOutput(DxcOutKind kind)
        {
            Result result = GetOutput(kind, typeof(IDxcBlob).GUID, out IntPtr nativePtr, IntPtr.Zero);
            if (result.Failure)
            {
                return default;
            }

            return new IDxcBlob(nativePtr);
        }


        public T? GetOutput<T>(DxcOutKind kind) where T : ComObject
        {
            Result result = GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, IntPtr.Zero);
            if (result.Failure)
            {
                return default;
            }

            return FromPointer<T>(nativePtr);
        }

        public unsafe T? GetOutput<T>(DxcOutKind kind, out IDxcBlobUtf16? outputName) where T : ComObject
        {
            IntPtr outputNamePtr = IntPtr.Zero;
            Result result = GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, new IntPtr(&outputNamePtr));
            if (result.Failure)
            {
                outputName = default;
                return default;
            }

            outputName = new IDxcBlobUtf16(outputNamePtr);
            return FromPointer<T>(nativePtr);
        }

        public Result GetOutput<T>(DxcOutKind kind, out T? @object) where T : ComObject
        {
            Result result = GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, IntPtr.Zero);
            if (result.Failure)
            {
                @object = default;
                return result;
            }

            @object = FromPointer<T>(nativePtr);
            return result;
        }

        public unsafe Result GetOutput<T>(DxcOutKind kind, out T? @object, out IDxcBlobUtf16? outputName) where T : ComObject
        {
            IntPtr outputNamePtr = IntPtr.Zero;
            Result result = GetOutput(kind, typeof(T).GUID, out IntPtr nativePtr, new IntPtr(&outputNamePtr));
            if (result.Failure)
            {
                @object = default;
                outputName = default;
                return result;
            }

            @object = FromPointer<T>(nativePtr);
            outputName = new IDxcBlobUtf16(outputNamePtr);
            return result;
        }
    }
}
