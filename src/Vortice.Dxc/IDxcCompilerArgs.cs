// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public unsafe partial class IDxcCompilerArgs
{
    public Result AddArguments(string[] arguments)
    {
        return AddArguments(arguments, arguments.Length);
    }

    public Result AddArguments(string[] arguments, int argumentsCount)
    {
        IntPtr* argumentsPtr = (IntPtr*)0;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
            }

            return AddArguments((IntPtr)argumentsPtr, argumentsCount);
        }
        finally
        {
            if (argumentsPtr != null)
            {
                Interop.Free(argumentsPtr);
            }
        }
    }

    public Result AddArgumentsUTF8(string[] arguments)
    {
        return AddArgumentsUTF8(arguments, arguments.Length);
    }

    public unsafe Result AddArgumentsUTF8(string[] arguments, int argumentsCount)
    {
        IntPtr* argumentsPtr = (IntPtr*)0;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = Interop.AllocToPointers(arguments, argumentsCount);
            }

            return AddArgumentsUTF8((IntPtr)argumentsPtr, argumentsCount);
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
