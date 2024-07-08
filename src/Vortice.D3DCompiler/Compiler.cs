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
        ShaderMacro[] defines,
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
        ShaderMacro[] defines,
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
        ShaderMacro[] defines,
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
        ShaderMacro[] macros,
        string entryPoint,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        Result result = CompileFromFile(
            fileName,
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

    public static ReadOnlyMemory<byte> CompileFromFile(
        string fileName,
        ShaderMacro[] macros,
        Include include,
        string entryPoint,
        string profile,
        ShaderFlags shaderFlags = ShaderFlags.None,
        EffectFlags effectFlags = EffectFlags.None)
    {
        Result result = CompileFromFile(
            fileName,
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
}
