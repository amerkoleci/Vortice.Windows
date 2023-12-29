// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public partial class IDxcLinker
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

    public unsafe Result Link(string entryName, string targetProfile, string[] libNames, string[] arguments, out IDxcOperationResult? result)
    {
        IntPtr* libNamesPtr = (IntPtr*)0;
        IntPtr* argumentsPtr = (IntPtr*)0;

        try
        {
            if (libNames?.Length > 0)
            {
                libNamesPtr = Interop.AllocToPointers(libNames, libNames.Length);
            }

            if (arguments?.Length > 0)
            {
                argumentsPtr = Interop.AllocToPointers(arguments, arguments.Length);
            }

            Result hr = Link(entryName,
                targetProfile,
                (IntPtr)libNamesPtr, (libNames?.Length) ?? 0,
                (IntPtr)argumentsPtr, (arguments?.Length) ?? 0,
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
            if (libNamesPtr != null)
                NativeMemory.Free(libNamesPtr);

            if (argumentsPtr != null)
                NativeMemory.Free(argumentsPtr);
        }
    }

    public unsafe Result Link(string entryName, string targetProfile, string[] libNames, int libCount, string[] arguments, int argumentsCount, out IDxcOperationResult? result)
    {
        IntPtr* libNamesPtr = (IntPtr*)0;
        IntPtr* argumentsPtr = (IntPtr*)0;

        try
        {
            if (libNames != null && libCount > 0)
            {
                libNamesPtr = Interop.AllocToPointers(libNames, libCount);
            }

            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
            }

            Result hr = Link(entryName,
                targetProfile,
                (IntPtr)libNamesPtr, libCount,
                (IntPtr)argumentsPtr, argumentsCount,
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
            if (libNamesPtr != null)
                NativeMemory.Free(libNamesPtr);

            if (argumentsPtr != null)
                NativeMemory.Free(argumentsPtr);
        }
    }
}
