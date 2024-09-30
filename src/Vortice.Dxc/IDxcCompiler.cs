// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public unsafe partial class IDxcCompiler
{
    public IDxcBlobEncoding Disassemble(IDxcBlob source)
    {
        Disassemble(source, out IDxcBlobEncoding result).CheckError();
        return result;
    }

    #region Compile
    public IDxcOperationResult Compile(IDxcBlob source,
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
            (uint)(arguments?.Length ?? 0),
            defines,
            includeHandler);
    }

    public unsafe IDxcOperationResult Compile(IDxcBlob source,
        string sourceName,
        string entryPoint,
        string targetProfile,
        string[] arguments,
        uint argumentsCount,
        DxcDefine[] defines,
        IDxcIncludeHandler includeHandler)
    {
        Utf16PinnedStringArray _argsPtr = default;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                _argsPtr = new(arguments, argumentsCount);
            }

            Compile(source, sourceName,
                entryPoint, targetProfile,
                _argsPtr.Handle, (uint)_argsPtr.Length,
                defines,
                includeHandler,
                out IDxcOperationResult? result).CheckError();
            return result!;
        }
        finally
        {
            _argsPtr.Release();
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
            (uint)(arguments?.Length ?? 0),
            defines,
            includeHandler,
            out result);
    }

    public Result Compile(IDxcBlob source,
                                 string sourceName,
                                 string entryPoint,
                                 string targetProfile,
                                 string[] arguments,
                                 uint argumentsCount,
                                 DxcDefine[] defines,
                                 IDxcIncludeHandler includeHandler,
                                 out IDxcOperationResult? result)
    {

        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            Result hr = Compile(source, sourceName,
                entryPoint, targetProfile,
                argumentsPtr!.Handle, argumentsCount,
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
            argumentsPtr.Release();
        }
    }
    #endregion Compile

    #region Preprocess
    public IDxcOperationResult Preprocess(IDxcBlob source,
        string sourceName,
        string[] arguments,
        DxcDefine[] defines,
        IDxcIncludeHandler includeHandler)
    {
        return Preprocess(
            source,
            sourceName,
            arguments,
            (uint)(arguments?.Length ?? 0),
            defines,
            includeHandler);
    }

    public unsafe IDxcOperationResult Preprocess(IDxcBlob source,
        string sourceName,
        string[] arguments,
        uint argumentsCount,
        DxcDefine[] defines,
        IDxcIncludeHandler includeHandler)
    {
        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            Preprocess(source, sourceName,
                argumentsPtr!.Handle, argumentsCount,
                defines,
                includeHandler,
                out IDxcOperationResult? result).CheckError();

            return result!;
        }
        finally
        {
            argumentsPtr.Release();
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
            (uint)(arguments?.Length ?? 0),
            defines,
            includeHandler,
            out result);
    }

    public unsafe Result Preprocess(IDxcBlob source,
        string sourceName,
        string[] arguments,
        uint argumentsCount,
        DxcDefine[] defines,
        IDxcIncludeHandler includeHandler,
        out IDxcOperationResult? result)
    {
        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            Result hr = Preprocess(source, sourceName,
                argumentsPtr!.Handle, argumentsCount,
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
            argumentsPtr.Release();
        }
    }
    #endregion
}
