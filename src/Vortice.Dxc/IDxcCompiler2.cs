// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcCompiler2
    {
        public IDxcOperationResult CompileWithDebug(IDxcBlob source,
            string sourceName,
            string entryPoint,
            string targetProfile,
            string[] arguments,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler,
            out string? debugBlobName, out IDxcBlob? debugBlob)
        {
            return CompileWithDebug(
                source,
                sourceName,
                entryPoint,
                targetProfile,
                arguments,
                arguments?.Length ?? 0,
                defines,
                includeHandler,
                out debugBlobName, out debugBlob);
        }

        public unsafe IDxcOperationResult CompileWithDebug(IDxcBlob source,
            string sourceName,
            string entryPoint,
            string targetProfile,
            string[] arguments,
            int argumentsCount,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler,
            out string? debugBlobName, out IDxcBlob? debugBlob)
        {

            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                IntPtr debugBlobNameOut = default;

                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                CompileWithDebug(source,
                    sourceName,
                    entryPoint, targetProfile,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines,
                    includeHandler,
                    out IDxcOperationResult? result,
                    new IntPtr(&debugBlobNameOut),
                    out debugBlob).CheckError();

                if (debugBlobNameOut != IntPtr.Zero)
                {
                    debugBlobName = Marshal.PtrToStringUni(debugBlobNameOut);
                }
                else
                {
                    debugBlobName = string.Empty;
                }

                return result!;
            }
            finally
            {
                if (argumentsPtr != null)
                {
                    Interop.Free(argumentsPtr);
                }
            }
        }

        public Result CompileWithDebug(IDxcBlob source,
            string sourceName,
            string entryPoint,
            string targetProfile,
            string[] arguments,
            DxcDefine[] defines,
            IDxcIncludeHandler includeHandler,
            out IDxcOperationResult? result, out string? debugBlobName, out IDxcBlob? debugBlob)
        {
            return CompileWithDebug(
                source,
                sourceName,
                entryPoint,
                targetProfile,
                arguments,
                arguments?.Length ?? 0,
                defines,
                includeHandler,
                out result, out debugBlobName, out debugBlob);
        }

        public unsafe Result CompileWithDebug(IDxcBlob source,
                                              string sourceName,
                                              string entryPoint,
                                              string targetProfile,
                                              string[] arguments,
                                              int argumentsCount,
                                              DxcDefine[] defines,
                                              IDxcIncludeHandler includeHandler,
                                              out IDxcOperationResult? result,
                                              out string? debugBlobName, out IDxcBlob? debugBlob)
        {

            IntPtr* argumentsPtr = (IntPtr*)0;

            try
            {
                IntPtr debugBlobNameOut = default;

                if (arguments != null && argumentsCount > 0)
                {
                    argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
                }

                Result hr = CompileWithDebug(source, sourceName,
                    entryPoint, targetProfile,
                    (IntPtr)argumentsPtr, argumentsCount,
                    defines,
                    includeHandler,
                    out result,
                    new IntPtr(&debugBlobNameOut),
                    out debugBlob);

                if (debugBlobNameOut != IntPtr.Zero)
                {
                    debugBlobName = Marshal.PtrToStringUni(debugBlobNameOut);
                }
                else
                {
                    debugBlobName = string.Empty;
                }

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
    }
}
