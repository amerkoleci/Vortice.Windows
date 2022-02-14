// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;

namespace Vortice.D3DCompiler;

public unsafe static partial class Compiler
{
    #region Compile
    public static Span<byte> Compile(
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
                shaderSourcePtr,
                shaderSource.Length,
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
                    throw new SharpGenException(errorBlob.ConvertToString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            Span<byte> bytecode = blob.GetBytes();
            blob.Dispose();
            errorBlob?.Dispose();
            return bytecode;
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Span<byte> Compile(
        string shaderSource,
        string entryPoint,
        string sourceName,
        ShaderMacro[] macros,
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
                shaderSourcePtr,
                shaderSource.Length,
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
                    throw new SharpGenException(errorBlob.ConvertToString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            Span<byte> bytecode = blob.GetBytes();
            blob.Dispose();
            errorBlob?.Dispose();
            return bytecode;
        }
        finally
        {
            if (shaderSourcePtr != IntPtr.Zero)
                Marshal.FreeHGlobal(shaderSourcePtr);
        }
    }

    public static Span<byte> Compile(
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
                shaderSourcePtr,
                shaderSource.Length,
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
                    throw new SharpGenException(errorBlob.ConvertToString());
                }
                else
                {
                    throw new SharpGenException(result);
                }
            }

            Span<byte> bytecode = blob.GetBytes();
            blob.Dispose();
            errorBlob?.Dispose();
            return bytecode;
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
                shaderSourcePtr,
                shaderSource.Length,
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
                shaderSourcePtr,
                shaderSource.Length,
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

        var shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
        try
        {
            return Compile(
                shaderSourcePtr,
                shaderSource.Length,
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
                shaderSourcePtr,
                shaderSource.Length,
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
                shaderSourcePtr,
                shaderSource.Length,
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
        byte[] shaderSource,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (shaderSource == null || shaderSource.Length == 0)
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        unsafe
        {
            fixed (void* pData = &shaderSource[0])
            {
                return Compile(
                    (IntPtr)pData,
                    shaderSource.Length,
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
    }

    public static Result Compile(
        byte[] shaderSource,
        ShaderMacro[] defines,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (shaderSource == null || shaderSource.Length == 0)
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        unsafe
        {
            fixed (void* pData = &shaderSource[0])
            {
                return Compile(
                    (IntPtr)pData,
                    shaderSource.Length,
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
    }

    public static Result Compile(
        byte[] shaderSource,
        ShaderMacro[] defines,
        Include include,
        string entryPoint,
        string sourceName,
        string profile,
        out Blob blob,
        out Blob errorBlob)
    {
        if (shaderSource == null || shaderSource.Length == 0)
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        unsafe
        {
            fixed (void* pData = &shaderSource[0])
            {
                return Compile(
                    (IntPtr)pData,
                    shaderSource.Length,
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
    }

    public static Result Compile(
        byte[] shaderSource,
        ShaderMacro[] defines,
        Include include,
        string entryPoint,
        string sourceName,
        string profile,
        ShaderFlags shaderFlags,
        out Blob blob,
        out Blob errorBlob)
    {
        if (shaderSource == null || shaderSource.Length == 0)
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        unsafe
        {
            fixed (void* pData = &shaderSource[0])
            {
                return Compile(
                    (IntPtr)pData,
                    shaderSource.Length,
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
    }

    public static Result Compile(
        byte[] shaderSource,
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
        if (shaderSource == null || shaderSource.Length == 0)
        {
            throw new ArgumentNullException(nameof(shaderSource));
        }

        unsafe
        {
            fixed (void* pData = &shaderSource[0])
            {
                return Compile(
                    (IntPtr)pData,
                    shaderSource.Length,
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

    public static Span<byte> CompileFromFile(
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
                throw new SharpGenException(errorBlob.ConvertToString());
            }
            else
            {
                throw new SharpGenException(result);
            }
        }

        Span<byte> bytcode = blob.GetBytes();
        blob.Dispose();
        errorBlob?.Dispose();
        return bytcode;
    }

    public static Span<byte> CompileFromFile(
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
                throw new SharpGenException(errorBlob.ConvertToString());
            }
            else
            {
                throw new SharpGenException(result);
            }
        }

        Span<byte> bytcode = blob.GetBytes();
        blob.Dispose();
        errorBlob?.Dispose();
        return bytcode;
    }

    public static Span<byte> CompileFromFile(
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
                throw new SharpGenException(errorBlob.ConvertToString());
            }
            else
            {
                throw new SharpGenException(result);
            }
        }

        Span<byte> bytcode = blob.GetBytes();
        blob.Dispose();
        errorBlob?.Dispose();
        return bytcode;
    }
    #endregion

    public static Blob CreateBlob(PointerSize size)
    {
        CreateBlob(size, out Blob blob).CheckError();
        return blob;
    }

    public static T Reflect<T>(Span<byte> shaderBytecode) where T : ComObject
    {
        fixed (byte* shaderBytecodePtr = shaderBytecode)
        {
            Reflect(shaderBytecodePtr, shaderBytecode.Length, typeof(T).GUID, out IntPtr nativePtr).CheckError();
            return MarshallingHelpers.FromPointer<T>(nativePtr);
        }
    }

    public static Result Reflect<T>(Span<byte> shaderBytecode, out T? reflection) where T : ComObject
    {
        fixed (byte* shaderBytecodePtr = shaderBytecode)
        {
            Result result = Reflect(shaderBytecodePtr, shaderBytecode.Length, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Success)
            {
                reflection = MarshallingHelpers.FromPointer<T>(nativePtr);
                return result;
            }

            reflection = null;
            return result;
        }
    }

    public static ShaderBytecode CompressShaders(params ShaderBytecode[] shaderBytecodes)
    {
        Blob? blob = default;
        ShaderData[] shaderData = new ShaderData[shaderBytecodes.Length];
        GCHandle[] handles = new GCHandle[shaderBytecodes.Length];
        try
        {
            for (int i = 0; i < shaderBytecodes.Length; i++)
            {
                handles[i] = GCHandle.Alloc(shaderBytecodes[i].Data, GCHandleType.Pinned);

                shaderData[i] = new ShaderData
                {
                    BytecodePtr = handles[i].AddrOfPinnedObject(),
                    BytecodeLength = shaderBytecodes[i].Data.Length
                };
            }
            blob = CompressShaders(shaderBytecodes.Length, shaderData, 1);
        }
        finally
        {
            foreach (GCHandle handle in handles)
            {
                handle.Free();
            }
        }

        if (blob == null)
        {
            return default;
        }

        return new ShaderBytecode(blob);
    }
}
