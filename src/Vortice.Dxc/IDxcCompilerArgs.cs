// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public unsafe partial class IDxcCompilerArgs
{
    private string[]? _arguments;

    public char** GetArgumentsPtr()
    {
        const int GetArguments__vtbl_index = 3;
        return ((delegate* unmanaged[MemberFunction]<IntPtr, char**>)this[GetArguments__vtbl_index])(NativePointer);
    }

    public string[] Arguments
    {
        get
        {
            if (_arguments == null || _arguments.Length != Count)
            {
                _arguments = new string[Count];

                char** argsPtr = GetArgumentsPtr();
                for (int i = 0; i < _arguments.Length; i++)
                {
                    _arguments[i] = new string(argsPtr[i]);
                }
            }

            return _arguments!;
        }
    }

    public Result AddArguments(params string[] arguments)
    {
        Utf16PinnedStringArray native = new(arguments);
        try
        {
            return AddArguments(native.Handle, native.Length);
        }
        finally
        {
            native.Release();
        }
    }

    public Result AddArguments(string[] arguments, int argumentsCount)
    {
        Utf16PinnedStringArray native = new(arguments, argumentsCount);
        try
        {
            return AddArguments(native.Handle, native.Length);
        }
        finally
        {
            native.Release();
        }
    }

    public Result AddArgumentsUTF8(params string[] arguments)
    {
        Utf8PinnedStringArray native = new(arguments);
        try
        {
            return AddArgumentsUTF8(native.Handle, native.Length);
        }
        finally
        {
            native.Release();
        }
    }

    public Result AddArgumentsUTF8(string[] arguments, int argumentsCount)
    {
        Utf8PinnedStringArray native = new(arguments, argumentsCount);
        try
        {
            return AddArgumentsUTF8(native.Handle, native.Length);
        }
        finally
        {
            native.Release();
        }
    }
}
