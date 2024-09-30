// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public unsafe partial class IDxcUtils
{
    public IDxcIncludeHandler CreateDefaultIncludeHandler()
    {
        CreateDefaultIncludeHandler(out IDxcIncludeHandler handler).CheckError();
        return handler;
    }

    public Result CreateReflection<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IDxcBlob blob, out T? reflection) where T : ComObject
    {
        DxcBuffer reflectionData = new DxcBuffer
        {
            Ptr = blob.GetBufferPointer(),
            Size = blob.GetBufferSize(),
            Encoding = Dxc.DXC_CP_ACP
        };

        Result result = CreateReflection(ref reflectionData, typeof(T).GUID, out IntPtr nativePtr);
        if (result.Failure)
        {
            reflection = default;
            return result;
        }

        reflection = MarshallingHelpers.FromPointer<T>(nativePtr);
        return result;
    }

    public T CreateReflection<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(IDxcBlob blob) where T : ComObject
    {
        DxcBuffer reflectionData = new DxcBuffer
        {
            Ptr = blob.GetBufferPointer(),
            Size = blob.GetBufferSize(),
            Encoding = Dxc.DXC_CP_ACP
        };

        CreateReflection(ref reflectionData, typeof(T).GUID, out IntPtr nativePtr).CheckError();
        return MarshallingHelpers.FromPointer<T>(nativePtr)!;
    }

    public IDxcCompilerArgs BuildArguments(string sourceName, string entryPoint, string targetProfile, string[] arguments, DxcDefine[] defines)
    {
        BuildArguments(sourceName, entryPoint, targetProfile, arguments, defines, out IDxcCompilerArgs? args).CheckError();
        return args!;
    }

    public IDxcCompilerArgs BuildArguments(string sourceName, string entryPoint, string targetProfile,
        string[] arguments, uint argumentsCount, DxcDefine[] defines, uint defineCount)
    {
        BuildArguments(sourceName, entryPoint, targetProfile, arguments, argumentsCount, defines, defineCount, out IDxcCompilerArgs? args).CheckError();
        return args!;
    }

    public Result BuildArguments(string sourceName, string entryPoint, string targetProfile,
        string[] arguments, DxcDefine[] defines, out IDxcCompilerArgs? args)
    {
        Utf16PinnedStringArray argsPtr = default;

        try
        {
            if (arguments?.Length > 0)
            {
                argsPtr = new(arguments, (uint)arguments.Length);
            }

            Result hr = BuildArguments(sourceName,
                entryPoint,
                targetProfile,
                argsPtr.Handle, argsPtr.Length,
                defines,
                (uint)(defines?.Length ?? 0),
                out args);

            if (hr.Failure)
            {
                args = default;
                return default;
            }

            return hr;
        }
        finally
        {
            argsPtr.Release();
        }
    }

    public Result BuildArguments(string sourceName, string entryPoint, string targetProfile,
        string[] arguments, uint argumentsCount,
        DxcDefine[] defines, uint defineCount, out IDxcCompilerArgs? args)
    {
        Utf16PinnedStringArray argumentsPtr = default;

        try
        {
            if (arguments != null && argumentsCount > 0)
            {
                argumentsPtr = new(arguments, argumentsCount);
            }

            Result hr = BuildArguments(sourceName,
                entryPoint,
                targetProfile,
                argumentsPtr.Handle, argumentsCount,
                defines, defineCount,
                out args);

            if (hr.Failure)
            {
                args = default;
                return default;
            }

            return hr;
        }
        finally
        {
            argumentsPtr.Release();
        }
    }

    public IDxcBlobEncoding CreateBlob(IntPtr data, uint size, uint codePage)
    {
        CreateBlob(data, size, codePage, out IDxcBlobEncoding result).CheckError();
        return result;
    }

    public IDxcBlob CreateBlobFromBlob(IDxcBlob blob, uint offset, uint length)
    {
        CreateBlobFromBlob(blob, offset, length, out IDxcBlob result).CheckError();
        return result;
    }

    public IDxcBlobEncoding CreateBlobFromPinned(IntPtr data, uint size, uint codePage)
    {
        CreateBlobFromPinned(data, size, codePage, out IDxcBlobEncoding result).CheckError();
        return result;
    }

    public IDxcBlobUtf8 GetBlobAsUtf8(IDxcBlob blob)
    {
        GetBlobAsUtf8(blob, out IDxcBlobUtf8 result).CheckError();
        return result;
    }

    public IDxcBlobWide GetBlobAsWide(IDxcBlob blob)
    {
        GetBlobAsWide(blob, out IDxcBlobWide result).CheckError();
        return result;
    }

    public IDxcBlobWide GetBlobAsUtf16(IDxcBlob blob)
    {
        GetBlobAsWide(blob, out IDxcBlobWide result).CheckError();
        return result;
    }

    public Result GetDxilContainerPart(string shaderSource, uint dxcPart, out IntPtr partData, out uint partSizeInBytesRef)
    {
        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);

        DxcBuffer buffer = new()
        {
            Ptr = shaderSourcePtr,
            Size = (nuint)shaderSource.Length,
            Encoding = Dxc.DXC_CP_ACP
        };

        try
        {
            Result hr = GetDxilContainerPart(ref buffer, dxcPart, out partData, out partSizeInBytesRef);
            if (hr.Failure)
            {
                return hr;
            }

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

    public IDxcBlob GetPDBContents(IDxcBlob pdbBlob, out IDxcBlob hash)
    {
        GetPDBContents(pdbBlob, out hash, out IDxcBlob result).CheckError();
        return result;
    }

    public IDxcBlobEncoding LoadFile(string fileName, uint? codePage)
    {
        LoadFile(fileName, codePage, out IDxcBlobEncoding result).CheckError();
        return result;
    }

    public IDxcBlobEncoding MoveToBlob(IntPtr data, ComObject malloc, uint size, uint codePage)
    {
        MoveToBlob(data, malloc, size, codePage, out IDxcBlobEncoding result).CheckError();
        return result;
    }
}
