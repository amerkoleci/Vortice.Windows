// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public unsafe partial class IDxcCompiler2
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

    public IDxcOperationResult CompileWithDebug(IDxcBlob source,
        string sourceName,
        string entryPoint,
        string targetProfile,
        string[] arguments,
        int argumentsCount,
        DxcDefine[] defines,
        IDxcIncludeHandler includeHandler,
        out string? debugBlobName, out IDxcBlob? debugBlob)
    {
        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            IntPtr debugBlobNameOut = default;

            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            CompileWithDebug(source,
                sourceName,
                entryPoint, targetProfile,
                argumentsPtr!.Handle, argumentsCount,
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
            argumentsPtr.Release();
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

    public Result CompileWithDebug(IDxcBlob source,
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

        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            IntPtr debugBlobNameOut = default;

            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            Result hr = CompileWithDebug(source, sourceName,
                entryPoint, targetProfile,
                argumentsPtr.Handle, argumentsCount,
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
            argumentsPtr.Release();
        }
    }
}
