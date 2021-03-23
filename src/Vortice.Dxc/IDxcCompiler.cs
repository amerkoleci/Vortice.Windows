// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcCompiler
    {
        public IDxcBlobEncoding Disassemble(IDxcBlob source)
        {
            Disassemble(source, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        #region Compile
        public IDxcOperationResult? Compile(IDxcBlob source,
            string sourceName,
            string entryPoint,
            string targetProfile,
            string[] arguments,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler)
        {
            return Compile(
                source,
                sourceName,
                entryPoint,
                targetProfile,
                arguments,
                arguments?.Length ?? 0,
                defines,
                includeHandler);
        }

        public unsafe IDxcOperationResult? Compile(IDxcBlob source,
            string sourceName,
            string entryPoint,
            string targetProfile,
            string[] arguments,
            int argumentsCount,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                Result hr = Compile(source, sourceName,
                    entryPoint, targetProfile,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines,
                    includeHandler,
                    out IDxcOperationResult? result);

                if (hr.Failure)
                {
                    result = default;
                    return result;
                }

                return result;
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }

        public Result Compile(IDxcBlob source,
            string sourceName,
            string entryPoint,
            string targetProfile,
            string[] arguments,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler, out IDxcOperationResult? result)
        {
            return Compile(
                source,
                sourceName,
                entryPoint,
                targetProfile,
                arguments,
                arguments?.Length ?? 0,
                defines,
                includeHandler,
                out result);
        }

        public unsafe Result Compile(IDxcBlob source,
                                     string sourceName,
                                     string entryPoint,
                                     string targetProfile,
                                     string[] arguments,
                                     int argumentsCount,
                                     DxcDefine[] defines,
                                     IDxcIncludeHandler includeHandler,
                                     out IDxcOperationResult? result)
        {

            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                Result hr = Compile(source, sourceName,
                    entryPoint, targetProfile,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines,
                    includeHandler,
                    out result);

                if (hr.Failure)
                {
                    result = default;
                    return hr;
                }

                return hr;
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }
        #endregion Compile

        #region Preprocess
        public IDxcOperationResult? Preprocess(IDxcBlob source,
            string sourceName,
            string[] arguments,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler)
        {
            return Preprocess(
                source,
                sourceName,
                arguments,
                arguments?.Length ?? 0,
                defines,
                includeHandler);
        }

        public unsafe IDxcOperationResult? Preprocess(IDxcBlob source,
            string sourceName,
            string[] arguments,
            int argumentsCount,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                Result hr = Preprocess(source, sourceName,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines,
                    includeHandler,
                    out IDxcOperationResult? result);

                if (hr.Failure)
                {
                    result = default;
                    return default;
                }

                return result;
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }

        public Result Preprocess(IDxcBlob source,
            string sourceName,
            string[] arguments,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler,
            out IDxcOperationResult? result)
        {
            return Preprocess(
                source,
                sourceName,
                arguments,
                arguments?.Length ?? 0,
                defines,
                includeHandler,
                out result);
        }

        public unsafe Result Preprocess(IDxcBlob source,
            string sourceName,
            string[] arguments,
            int argumentsCount,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler,
            out IDxcOperationResult? result)
        {
            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                Result hr = Preprocess(source, sourceName,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines,
                    includeHandler,
                    out result);

                if (hr.Failure)
                {
                    result = default;
                    return hr;
                }

                return hr;
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }
        #endregion
    }
}
