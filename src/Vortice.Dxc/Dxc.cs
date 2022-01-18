// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

#if NET5_0_OR_GREATER
using System.IO;
using System.Reflection;
#endif

namespace Vortice.Dxc
{
    public static partial class Dxc
    {
        public static readonly Guid CLSID_DxcCompiler = new("73e22d93-e6ce-47f3-b5bf-f0664f39c1b0");
        public static readonly Guid CLSID_DxcLinker = new("EF6A8087-B0EA-4D56-9E45-D07E1A8B7806");
        public static readonly Guid CLSID_DxcDiaDataSource = new("CD1F6B73-2AB0-484D-8EDC-EBE7A43CA09F");
        public static readonly Guid CLSID_DxcCompilerArgs = new("3E56AE82-224D-470F-A1A1-FE3016EE9F9D");
        public static readonly Guid CLSID_DxcLibrary = new("6245D6AF-66E0-48FD-80B4-4D271796748C");
        public static readonly Guid CLSID_DxcUtils = new("6245D6AF-66E0-48FD-80B4-4D271796748C");
        public static readonly Guid CLSID_DxcValidator = new("8CA3E215-F728-4CF3-8CDD-88AF917587A1");
        public static readonly Guid CLSID_DxcAssembler = new("D728DB68-F903-4F80-94CD-DCCF76EC7151");
        public static readonly Guid CLSID_DxcContainerReflection = new("b9f54489-55b8-400c-ba3a-1675e4728b91");
        public static readonly Guid CLSID_DxcOptimizer = new("AE2CD79F-CC22-453F-9B6B-B124E7A5204C");
        public static readonly Guid CLSID_DxcContainerBuilder = new("94134294-411f-4574-b4d0-8741e25240d2");
        public static readonly Guid CLSID_DxcPdbUtils = new("54621dfb-f2ce-457e-ae8c-ec355faeec7c");

        public const int DXC_CP_UTF8 = 65001;
        public const int DXC_CP_UTF16 = 1200;
        public const int DXC_CP_ACP = 0;

#if NET5_0_OR_GREATER
        public static event DllImportResolver? ResolveLibrary;

        static Dxc()
        {
            ResolveLibrary += static (libraryName, assembly, searchPath) =>
            {
                if (libraryName is not "dxcompiler.dll")
                {
                    return IntPtr.Zero;
                }

                string rid = RuntimeInformation.ProcessArchitecture switch
                {
                    Architecture.X64 => "win-x64",
                    Architecture.Arm64 => "win-arm64",
                    _ => throw new NotSupportedException("Invalid process architecture")
                };

                // Test whether the native libraries are present in the same folder of the executable
                // (which is the case when the program was built with a runtime identifier), or whether
                // they are in the "runtimes\win-x64\native" folder in the executable directory.
                string nugetNativeLibsPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native");
                bool isNuGetRuntimeLibrariesDirectoryPresent = Directory.Exists(nugetNativeLibsPath);

                if (isNuGetRuntimeLibrariesDirectoryPresent)
                {
                    string dxcompilerPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native\dxcompiler.dll");
                    string dxilPath = Path.Combine(AppContext.BaseDirectory, $@"runtimes\{rid}\native\dxil.dll");

                    // Load DXIL first so that DXC doesn't fail to load it, and then DXIL, both from the NuGet path
                    if (NativeLibrary.TryLoad(dxilPath, out _) &&
                        NativeLibrary.TryLoad(dxcompilerPath, out IntPtr handle))
                    {
                        return handle;
                    }
                }
                else
                {
                    // Even when the two libraries are correctly copied next to the executable in use, we load them
                    // manually to ensure the operation is successful. This is to avoid failures in cases such as when
                    // doing "dotnet bin\MyApp.dll", ie. when the host is in another path than the executable in use.
                    // This is probably because DXIL is a native dependency for DXC, but the way Windows loads these
                    // libraries doesn't take into account the .NET concepts of "app directory": neither the current "bin"
                    // directory nor the "process directory", which is "C:\Program Files\dotnet", actually contain the
                    // native library we need, hence the runtime crash. Manually loading the library this way solves this.
                    if (NativeLibrary.TryLoad("dxil", assembly, searchPath, out _) &&
                        NativeLibrary.TryLoad("dxcompiler", assembly, searchPath, out IntPtr handle))
                    {
                        return handle;
                    }
                }

                return IntPtr.Zero;
            };

            NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), OnDllImport);
        }

        /// <summary>The default <see cref="DllImportResolver"/> for TerraFX.Interop.Windows.</summary>
        /// <inheritdoc cref="DllImportResolver"/>
        private static IntPtr OnDllImport(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (TryResolveLibrary(libraryName, assembly, searchPath, out IntPtr nativeLibrary))
            {
                return nativeLibrary;
            }

            return NativeLibrary.Load(libraryName, assembly, searchPath);
        }

        /// <summary>Tries to resolve a native library using the handlers for the <see cref="ResolveLibrary"/> event.</summary>
        /// <param name="libraryName">The native library to resolve.</param>
        /// <param name="assembly">The assembly requesting the resolution.</param>
        /// <param name="searchPath">The <see cref="DllImportSearchPath"/> value on the P/Invoke or assembly, or <see langword="null"/>.</param>
        /// <param name="nativeLibrary">The loaded library, if one was resolved.</param>
        /// <returns>Whether or not the requested library was successfully loaded.</returns>
        private static bool TryResolveLibrary(string libraryName, Assembly assembly, DllImportSearchPath? searchPath, out IntPtr nativeLibrary)
        {
            var resolveLibrary = ResolveLibrary;

            if (resolveLibrary != null)
            {
                var resolvers = resolveLibrary.GetInvocationList();

                foreach (DllImportResolver resolver in resolvers)
                {
                    nativeLibrary = resolver(libraryName, assembly, searchPath);

                    if (nativeLibrary != IntPtr.Zero)
                    {
                        return true;
                    }
                }
            }

            nativeLibrary = IntPtr.Zero;
            return false;
        }
#endif

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler CreateDxcCompiler()
        {
            DxcCreateInstance(CLSID_DxcCompiler, out IDxcCompiler? result).CheckError();
            return result!;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler2 CreateDxcCompiler2()
        {
            DxcCreateInstance(CLSID_DxcCompiler, out IDxcCompiler2? result).CheckError();
            return result!;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler3 CreateDxcCompiler3()
        {
            DxcCreateInstance(CLSID_DxcCompiler, out IDxcCompiler3? result).CheckError();
            return result!;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcUtils CreateDxcUtils()
        {
            DxcCreateInstance(CLSID_DxcUtils, out IDxcUtils? result).CheckError();
            return result!;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcAssembler CreateDxcAssembler()
        {
            DxcCreateInstance(CLSID_DxcAssembler, out IDxcAssembler? result).CheckError();
            return result!;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcLinker CreateDxcLinker()
        {
            DxcCreateInstance(CLSID_DxcLinker, out IDxcLinker? result).CheckError();
            return result!;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcContainerReflection CreateDxcContainerReflection()
        {
            DxcCreateInstance(CLSID_DxcContainerReflection, out IDxcContainerReflection? result).CheckError();
            return result!;
        }

        public static void LoadDxil()
        {
            try
            {
                LoadLibrary("dxil.dll");
            }
            catch
            {

            }
        }

        public static Result DxcCreateInstance<T>(Guid classGuid, out T? instance) where T : ComObject
        {
            Result result = DxcCreateInstance(classGuid, typeof(T).GUID, out IntPtr nativePtr);
            if (result.Success)
            {
                instance = MarshallingHelpers.FromPointer<T>(nativePtr);
                return result;
            }

            instance = null;
            return result;
        }

        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string fileName);
    }
}
