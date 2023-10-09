// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

namespace Vortice.Dxc;

public partial class IDxcCompiler3
{
    public unsafe IDxcResult Compile(string source, string[] arguments, IDxcIncludeHandler includeHandler)
    {
        Compile(source, arguments, includeHandler, out IDxcResult? result).CheckError();
        return result!;
    }

    public unsafe Result Compile<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string source, string[] arguments, IDxcIncludeHandler includeHandler, out T? result) where T : ComObject
    {
        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(source);
        IntPtr* argumentsPtr = (IntPtr*)0;

        DxcBuffer buffer = new()
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

            result = MarshallingHelpers.FromPointer<T>(nativePtr);
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

    public T Disassemble<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(in DxcBuffer buffer) where T : IDxcResult
    {
        Disassemble(buffer, out T? result).CheckError();
        return result!;
    }

    public unsafe Result Disassemble<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(DxcBuffer buffer, out T? result) where T : IDxcResult
    {
        Result hr = Disassemble(ref buffer, typeof(T).GUID, out IntPtr nativePtr);
        if (hr.Failure)
        {
            result = default;
            return hr;
        }

        result = MarshallingHelpers.FromPointer<T>(nativePtr);
        return hr;
    }

    public T Disassemble<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string source) where T : IDxcResult
    {
        Disassemble(source, out T? result).CheckError();
        return result!;
    }

    public unsafe Result Disassemble<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(string source, out T? result) where T : IDxcResult
    {
        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(source);

        DxcBuffer buffer = new()
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

            result = MarshallingHelpers.FromPointer<T>(nativePtr);
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
