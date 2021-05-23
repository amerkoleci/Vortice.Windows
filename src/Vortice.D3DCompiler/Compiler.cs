// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice.D3DCompiler
{
    public unsafe static partial class Compiler
    {
        #region Compile
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

            var shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
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

            var shaderSourcePtr = Marshal.StringToHGlobalAnsi(shaderSource);
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
        #endregion

        public static Blob CreateBlob(PointerSize size)
        {
            CreateBlob(size, out var blob).CheckError();
            return blob;
        }

        public static Result Reflect<T>(byte[] shaderBytecode, out T reflection) where T : ComObject
        {
            var interfaceGuid = typeof(T).GUID;
            fixed (void* shaderBytecodePtr = shaderBytecode)
            {
                PointerSize srcDataSize = shaderBytecode.Length;
                var result = Reflect(new IntPtr(shaderBytecodePtr), srcDataSize, interfaceGuid, out var nativePtr);
                if (result.Success)
                {
                    reflection = CppObject.FromPointer<T>(nativePtr);
                    return result;
                }

                reflection = null;
                return result;
            }
        }

        public static ShaderBytecode CompressShaders(params ShaderBytecode[] shaderBytecodes)
        {
            Blob blob = null;
            var shaderData = new ShaderData[shaderBytecodes.Length];
            var handles = new GCHandle[shaderBytecodes.Length];
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
                foreach (var handle in handles)
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
}
