// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.D3DCompiler;

public unsafe static partial class Compiler
{
    #region Compile
    public static ReadOnlyMemory<byte> Compile(
        string shaderSource,
        string entryPoint,
        string sourceName,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            Result result = Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                null,
                null,
                entryPoint,
                profile,
                shaderFlags,
                effectFlags,
                out Blob blob,
                out Blob? errorBlob);

            if (result.Failure)
            {
                if (errorBlob != null)
                {
                    throw new SharpGenException(result, errorBlob.AsString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            errorBlob?.Dispose();
            ReadOnlyMemory<byte> bytecode = blob.AsMemory();
            blob.Dispose();
            return bytecode;
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(shaderSourcePtr);
            }
        }
    }

    public static ReadOnlyMemory<byte> Compile(
        string shaderSource,
        ShaderMacro[] macros,
        string entryPoint,
        string sourceName,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            Result result = Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                macros,
                null,
                entryPoint,
                profile,
                shaderFlags,
                effectFlags,
                out Blob blob,
                out Blob? errorBlob);

            if (result.Failure)
            {
                if (errorBlob != null)
                {
                    throw new SharpGenException(result, errorBlob.AsString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            errorBlob?.Dispose();
            ReadOnlyMemory<byte> bytecode = blob.AsMemory();
            blob.Dispose();
            return bytecode;
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static ReadOnlyMemory<byte> Compile(
        string shaderSource,
        ShaderMacro[] macros,
        Include include,
        string entryPoint,
        string sourceName,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            Result result = Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                macros,
                include,
                entryPoint,
                profile,
                shaderFlags,
                effectFlags,
                out Blob blob,
                out Blob? errorBlob);

            if (result.Failure)
            {
                if (errorBlob != null)
                {
                    throw new SharpGenException(result, errorBlob.AsString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            errorBlob?.Dispose();
            ReadOnlyMemory<byte> bytecode = blob.AsMemory();
            blob.Dispose();
            return bytecode;
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Blob Compile(
        string shaderSource,
        string entryPoint,
        string sourceName,
        ShaderMacro[] macros,
        Include include,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            Result result = Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                macros,
                include,
                entryPoint,
                profile,
                shaderFlags,
                effectFlags,
                out Blob blob,
                out Blob? errorBlob);

            if (result.Failure)
            {
                if (errorBlob != null)
                {
                    throw new SharpGenException(result, errorBlob.AsString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            errorBlob?.Dispose();
            return blob;
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Result Compile(
        string shaderSource,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            return Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                null,
                null,
                entryPoint,
                profile,
                ShaderFlags.None,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Result Compile(
       string shaderSource,
       ShaderMacro[] defines,
       string entryPoint,
       string sourceName,
       string profile,
       out Blob blob,
       out Blob errorBlob)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            return Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                defines,
                null,
                entryPoint,
                profile,
                ShaderFlags.None,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Result Compile(
       string shaderSource,
       ShaderMacro[] defines,
       Include include,
       string entryPoint,
       string sourceName,
       string profile,
       out Blob blob,
       out Blob errorBlob)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            return Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                defines,
                include,
                entryPoint,
                profile,
                ShaderFlags.None,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Result Compile(
       string shaderSource,
       ShaderMacro[] defines,
       Include include,
       string entryPoint,
       string sourceName,
       string profile,
       ShaderFlags shaderFlags,
       out Blob blob,
       out Blob errorBlob)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            return Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                defines,
                include,
                entryPoint,
                profile,
                shaderFlags,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Result Compile(
       string shaderSource,
       ShaderMacro[] defines,
       Include include,
       string entryPoint,
       string sourceName,
       string profile,
       ShaderFlags shaderFlags,
       EffectFlags effectFlags,
       out Blob blob,
       out Blob errorBlob)
    {
        if (string.IsNullOrEmpty(shaderSource))
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        IntPtr shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            return Compile(
                shaderSourcePtr.ToPointer(),
                (nuint)shaderSource.Length,
                sourceName,
                defines,
                include,
                entryPoint,
                profile,
                shaderFlags,
                effectFlags,
                out blob,
                out errorBlob);
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Result Compile(
        ReadOnlySpan<byte> source,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (source.Length == 0)
        {
            throw new ArgumentNullException(nameof(source));
        }

        fixed (byte* sourcePtr = source)
        {
            return Compile(
                sourcePtr,
                (nuint)source.Length,
                sourceName,
                null,
                null,
                entryPoint,
                profile,
                ShaderFlags.None,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
    }

    public static Result Compile(
        ReadOnlySpan<byte> source,
        ShaderMacro[] defines,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (source.Length == 0)
        {
            throw new ArgumentNullException(nameof(source));
        }

        fixed (byte* sourcePtr = source)
        {
            return Compile(
                sourcePtr,
                (nuint)source.Length,
                sourceName,
                defines,
                null,
                entryPoint,
                profile,
                ShaderFlags.None,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
    }

    public static Result Compile(
        ReadOnlySpan<byte> source,
        ShaderMacro[] defines,
        Include include,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (source.Length == 0)
        {
            throw new ArgumentNullException(nameof(source));
        }

        fixed (byte* sourcePtr = source)
        {
            return Compile(
                sourcePtr,
                (nuint)source.Length,
                sourceName,
                defines,
                include,
                entryPoint,
                profile,
                ShaderFlags.None,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
    }

    public static Result Compile(
        ReadOnlySpan<byte> source,
        ShaderMacro[] defines,
        Include include,
        string entryPoint,
        string sourceName,
        string profile,
        ShaderFlags shaderFlags,
        out Blob blob,
        out Blob errorBlob)
    {
        if (source.Length == 0)
        {
            throw new ArgumentNullException(nameof(source));
        }

        fixed (byte* sourcePtr = source)
        {
            return Compile(
                sourcePtr,
                (nuint)source.Length,
                sourceName,
                defines,
                include,
                entryPoint,
                profile,
                shaderFlags,
                EffectFlags.None,
                out blob,
                out errorBlob);
        }
    }

    public static Result Compile(
        ReadOnlySpan<byte> source,
        ShaderMacro[] defines,
        Include include,
        string entryPoint,
        string sourceName,
        string profile,
        ShaderFlags shaderFlags,
        EffectFlags effectFlags,
        out Blob blob,
        out Blob errorBlob)
    {
        if (source.Length == 0)
        {
            throw new ArgumentNullException(nameof(source));
        }

        fixed (byte* sourcePtr = source)
        {
            return Compile(
                sourcePtr,
                (nuint)source.Length,
                sourceName,
                defines,
                include,
                entryPoint,
                profile,
                shaderFlags,
                effectFlags,
                out blob,
                out errorBlob);
        }
    }

    public static unsafe Result Compile(void* srcData,
        PointerUSize srcDataSize,
        string sourceName,
        ShaderMacro[]? defines,
        Include? include,
        string entryPoint,
        string target,
        ShaderFlags flags1,
        EffectFlags flags2,
        out Blob code,
        out Blob errorMsgs)
    {
        return D3DCompile(srcData, srcDataSize, sourceName, PrepareMacros(defines), include, entryPoint, target, flags1, flags2, out code, out errorMsgs);
    }
    #endregion

    #region Compile2
    public static Result Compile2(
        nint srcData,
        PointerUSize srcDataSize,
        string sourceName,
        ShaderMacro[]? defines,
        Include? include,
        string entryPoint,
        string target,
        ShaderFlags flags1,
        EffectFlags flags2,
        SecondaryDataFlags secondaryDataFlags,
        nint secondaryData,
        PointerUSize secondaryDataSize, out Blob code, out Blob errorMsgs)
    {
        return D3DCompile2(srcData, srcDataSize, sourceName, PrepareMacros(defines), include, entryPoint, target, flags1, flags2, secondaryDataFlags, secondaryData, secondaryDataSize, out code, out errorMsgs);
    }
    #endregion

    #region CompileFromFile
    public static Result CompileFromFile(
        string fileName,
        string entryPoint,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        return CompileFromFile(
            fileName,
            null,
            null,
            entryPoint,
            profile,
            ShaderFlags.None,
            EffectFlags.None,
            out blob,
            out errorBlob);
    }

    public static Result CompileFromFile(
        string fileName,
        ShaderMacro[]? defines,
        string entryPoint,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        return CompileFromFile(
            fileName,
            defines,
            null,
            entryPoint,
            profile,
            ShaderFlags.None,
            EffectFlags.None,
            out blob,
            out errorBlob);
    }

    public static Result CompileFromFile(
        string fileName,
        ShaderMacro[]? defines,
        Include include,
        string entryPoint,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        return CompileFromFile(
            fileName,
            defines,
            include,
            entryPoint,
            profile,
            ShaderFlags.None,
            EffectFlags.None,
            out blob,
            out errorBlob);
    }

    public static Result CompileFromFile(
        string fileName,
        ShaderMacro[]? defines,
        Include include,
        string entryPoint,
        string profile,
        ShaderFlags shaderFlags,
        out Blob blob,
        out Blob errorBlob)
    {
        return CompileFromFile(
            fileName,
            defines,
            include,
            entryPoint,
            profile,
            shaderFlags,
            EffectFlags.None,
            out blob,
            out errorBlob);
    }

    public static ReadOnlyMemory<byte> CompileFromFile(
        string fileName,
        string entryPoint,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        Result result = CompileFromFile(
            fileName,
            null,
            null,
            entryPoint,
            profile,
            shaderFlags,
            effectFlags,
            out Blob blob,
            out Blob? errorBlob);

        if (result.Failure)
        {
            if (errorBlob != null)
            {
                throw new SharpGenException(result, errorBlob.AsString());
            }
            else
            {
                throw new SharpGenException(result);
            }
        }

        errorBlob?.Dispose();
        ReadOnlyMemory<byte> bytecode = blob.AsMemory();
        blob.Dispose();
        return bytecode;
    }

    public static ReadOnlyMemory<byte> CompileFromFile(
        string fileName,
        ShaderMacro[]? defines,
        string entryPoint,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        Result result = CompileFromFile(
            fileName,
            defines,
            null,
            entryPoint,
            profile,
            shaderFlags,
            effectFlags,
            out Blob blob,
            out Blob? errorBlob);

        if (result.Failure)
        {
            if (errorBlob != null)
            {
                throw new SharpGenException(result, errorBlob.AsString());
            }
            else
            {
                throw new SharpGenException(result);
            }
        }

        errorBlob?.Dispose();
        ReadOnlyMemory<byte> bytecode = blob.AsMemory();
        blob.Dispose();
        return bytecode;
    }

    public static ReadOnlyMemory<byte> CompileFromFile(
        string fileName,
        ShaderMacro[]? defines,
        Include include,
        string entryPoint,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        Result result = CompileFromFile(
            fileName,
            PrepareMacros(defines),
            include,
            entryPoint,
            profile,
            shaderFlags,
            effectFlags,
            out Blob blob,
            out Blob? errorBlob);

        if (result.Failure)
        {
            if (errorBlob != null)
            {
                throw new SharpGenException(result, errorBlob.AsString());
            }
            else
            {
                throw new SharpGenException(result);
            }
        }

        errorBlob?.Dispose();
        ReadOnlyMemory<byte> bytecode = blob.AsMemory();
        blob.Dispose();
        return bytecode;
    }

    public static Result CompileFromFile(string fileName,
        ShaderMacro[]? defines,
        Include? include,
        string entryPoint,
        string target,
        ShaderFlags flags1,
        EffectFlags flags2, out Blob code, out Blob errorMsgs)
    {
        return D3DCompileFromFile(fileName, PrepareMacros(defines), include, entryPoint, target, flags1, flags2, out code, out errorMsgs);
    }
    #endregion

    public static Blob CreateBlob(nuint size)
    {
        CreateBlob(size, out Blob blob).CheckError();
        return blob;
    }

    public static T Reflect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ReadOnlySpan<byte> shaderBytecode) where T : ComObject
    {
        fixed (byte* shaderBytecodePtr = shaderBytecode)
        {
            Reflect(shaderBytecodePtr, (nuint)shaderBytecode.Length, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr)!;
        }
    }

    public static Result Reflect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(ReadOnlySpan<byte> shaderBytecode, out T? reflection) where T : ComObject
    {
        fixed (byte* shaderBytecodePtr = shaderBytecode)
        {
            Result result = Reflect(shaderBytecodePtr, (nuint)shaderBytecode.Length, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Success)
            {
                reflection = MarshallingHelpers.FromPointer<T>(nativePtr);
                return result;
            }

            reflection = null;
            return result;
        }
    }

    public static T Reflect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(byte[] shaderBytecode) where T : ComObject
    {
        ReadOnlySpan<byte> span = shaderBytecode.AsSpan();
        return Reflect<T>(span);
    }

    public static Result Reflect<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(byte[] shaderBytecode, out T? reflection) where T : ComObject
    {
        ReadOnlySpan<byte> span = shaderBytecode.AsSpan();
        return Reflect(span, out reflection);
    }

    public static Result GetInputSignatureBlob(Blob srcData, out Blob signatureBlob)
    {
        return GetInputSignatureBlob(srcData.BufferPointer, srcData.BufferSize, out signatureBlob);
    }

    public static Blob GetInputSignatureBlob(Blob srcData)
    {
        GetInputSignatureBlob(srcData.BufferPointer, srcData.BufferSize, out Blob signatureBlob).CheckError();
        return signatureBlob;
    }

    public static Blob GetInputSignatureBlob(ReadOnlySpan<byte> srcData)
    {
        fixed (byte* pSrcData = srcData)
        {
            GetInputSignatureBlob((IntPtr)pSrcData, (nuint)srcData.Length, out Blob signatureBlob).CheckError();
            return signatureBlob;
        }
    }

    public static Result GetOutputSignatureBlob(Blob srcData, out Blob signatureBlob)
    {
        return GetOutputSignatureBlob(srcData.BufferPointer, srcData.BufferSize, out signatureBlob);
    }

    public static Blob GetOutputSignatureBlob(Blob srcData)
    {
        GetOutputSignatureBlob(srcData.BufferPointer, srcData.BufferSize, out Blob signatureBlob).CheckError();
        return signatureBlob;
    }

    public static Blob GetOutputSignatureBlob(ReadOnlySpan<byte> srcData)
    {
        fixed (byte* pSrcData = srcData)
        {
            GetOutputSignatureBlob((IntPtr)pSrcData, (nuint)srcData.Length, out Blob signatureBlob).CheckError();
            return signatureBlob;
        }
    }


    private static ShaderMacro[]? PrepareMacros(ShaderMacro[]? macros)
    {
        if (macros == null)
            return null;

        if (macros.Length == 0)
            return null;

        if (macros[macros.Length - 1].Name == null && macros[macros.Length - 1].Definition == null)
            return macros;

        ShaderMacro[] macroArray = new ShaderMacro[macros.Length + 1];

        Array.Copy(macros, macroArray, macros.Length);

        macroArray[macros.Length] = new ShaderMacro(null, null);
        return macroArray;
    }
}
