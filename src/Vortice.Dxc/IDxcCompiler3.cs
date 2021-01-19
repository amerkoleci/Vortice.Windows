// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.Runtime.InteropServices;
using System.Text;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcCompiler3
    {
        public unsafe IDxcResult Compile(string source, string[] arguments, IDxcIncludeHandler includeHandler)
        {
            Compile(source, arguments, includeHandler, out IDxcResult result).CheckError();
            return result;
        }

        public unsafe Result Compile<T>(string source, string[] arguments, IDxcIncludeHandler includeHandler, out T result) where T : ComObject
        {
            IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(source);
            IntPtr* argumentsPtr = (IntPtr*)0;

            DxcBuffer buffer = new DxcBuffer
            {
                Ptr = shaderSourcePtr,
                Size = source.Length,
                Encoding = Dxc.DXC_CP_ACP
            };

            try
            {
                int argumentsCount = 0;
                if (arguments != null && arguments.Length > 0)
                {
                    argumentsCount = arguments.Length;
                    argumentsPtr = Interop.AllocToPointers(arguments);
                }

                Result hr = Compile(ref buffer, (IntPtr)argumentsPtr, argumentsCount, includeHandler, typeof(T).GUID, out IntPtr nativePtr);
                if (hr.Failure)
                {
                    result = default;
                    return hr;
                }

                result = FromPointer<T>(nativePtr);
                return hr;
            }
            finally
            {
                if (shaderSourcePtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(shaderSourcePtr);
                }

                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }

        public T Disassemble<T>(in DxcBuffer buffer) where T : IDxcResult
        {
            Disassemble(buffer, out T result);
            return result;
        }

        public unsafe Result Disassemble<T>(DxcBuffer buffer, out T result) where T : IDxcResult
        {
            Result hr = Disassemble(ref buffer, typeof(T).GUID, out IntPtr nativePtr);
            if (hr.Failure)
            {
                result = default;
                return hr;
            }

            result = FromPointer<T>(nativePtr);
            return hr;
        }

        public T Disassemble<T>(string source) where T : IDxcResult
        {
            Disassemble(source, out T result);
            return result;
        }

        public unsafe Result Disassemble<T>(string source, out T result) where T : IDxcResult
        {
            IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(source);

            DxcBuffer buffer = new DxcBuffer
            {
                Ptr = shaderSourcePtr,
                Size = source.Length,
                Encoding = Dxc.DXC_CP_ACP
            };

            try
            {
                Result hr = Disassemble(ref buffer, typeof(T).GUID, out IntPtr nativePtr);
                if (hr.Failure)
                {
                    result = default;
                    return hr;
                }

                result = FromPointer<T>(nativePtr);
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
    }
}
