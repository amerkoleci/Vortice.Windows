// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public unsafe partial class IDxcLinker
{
    public IDxcOperationResult Link(string entryName, string targetProfile, string[] libNames, string[] arguments)
    {
        Link(entryName, targetProfile, libNames, arguments, out IDxcOperationResult? result).CheckError();
        return result!;
    }

    public IDxcOperationResult Link(string entryName, string targetProfile, string[] libNames, int libCount, string[] arguments, int argumentsCount)
    {
        Link(entryName, targetProfile, libNames, libCount, arguments, argumentsCount, out IDxcOperationResult? result).CheckError();
        return result!;
    }

    public Result Link(string entryName, string targetProfile, string[] libNames, string[] arguments, out IDxcOperationResult? result)
    {
        Utf16PinnedStringArray libNamesPtr =  default;
        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            if (libNames?.Length > 0)
            {
                libNamesPtr = new(libNames, libNames.Length);
            }

            if (arguments?.Length > 0)
            {
                argumentsPtr = new(arguments, arguments.Length);
            }

            Result hr = Link(entryName,
                targetProfile,
                libNamesPtr.Handle, libNamesPtr.Length,
                argumentsPtr.Handle, argumentsPtr.Length,
                out result);

            if (hr.Failure)
            {
                result = default;
                return default;
            }

            return hr;
        }
        finally
        {
            libNamesPtr.Release();
            argumentsPtr.Release();
        }
    }

    public Result Link(string entryName, string targetProfile, string[] libNames, int libCount, string[] arguments, int argumentsCount, out IDxcOperationResult? result)
    {
        Utf16PinnedStringArray libNamesPtr = default;
        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            if (libNames != null && libCount > 0)
            {
                libNamesPtr = new(libNames, libCount);
            }

            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            Result hr = Link(entryName,
                targetProfile,
                libNamesPtr.Handle, libCount,
                argumentsPtr.Handle, argumentsCount,
                out result);

            if (hr.Failure)
            {
                result = default;
                return default;
            }

            return hr;
        }
        finally
        {
            libNamesPtr.Release();
            argumentsPtr.Release();
        }
    }
}
