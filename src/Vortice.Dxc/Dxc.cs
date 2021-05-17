// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public static partial class Dxc
    {
        public static readonly Guid CLSID_DxcCompiler = new Guid("73e22d93-e6ce-47f3-b5bf-f0664f39c1b0");
        public static readonly Guid CLSID_DxcLinker = new Guid("EF6A8087-B0EA-4D56-9E45-D07E1A8B7806");
        public static readonly Guid CLSID_DxcDiaDataSource = new Guid("CD1F6B73-2AB0-484D-8EDC-EBE7A43CA09F");
        public static readonly Guid CLSID_DxcCompilerArgs = new Guid("3E56AE82-224D-470F-A1A1-FE3016EE9F9D");
        public static readonly Guid CLSID_DxcLibrary = new Guid("6245D6AF-66E0-48FD-80B4-4D271796748C");
        public static readonly Guid CLSID_DxcUtils = new Guid("6245D6AF-66E0-48FD-80B4-4D271796748C");
        public static readonly Guid CLSID_DxcValidator = new Guid("8CA3E215-F728-4CF3-8CDD-88AF917587A1");
        public static readonly Guid CLSID_DxcAssembler = new Guid("D728DB68-F903-4F80-94CD-DCCF76EC7151");
        public static readonly Guid CLSID_DxcContainerReflection = new Guid("b9f54489-55b8-400c-ba3a-1675e4728b91");
        public static readonly Guid CLSID_DxcOptimizer = new Guid("AE2CD79F-CC22-453F-9B6B-B124E7A5204C");
        public static readonly Guid CLSID_DxcContainerBuilder = new Guid("94134294-411f-4574-b4d0-8741e25240d2");
        public static readonly Guid CLSID_DxcPdbUtils = new Guid("54621dfb-f2ce-457e-ae8c-ec355faeec7c");

        public const int DXC_CP_UTF8 = 65001;
        public const int DXC_CP_UTF16 = 1200;
        public const int DXC_CP_ACP = 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler? CreateDxcCompiler()
        {
            DxcCreateInstance(CLSID_DxcCompiler, out IDxcCompiler? result).CheckError();
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler2? CreateDxcCompiler2()
        {
            DxcCreateInstance(CLSID_DxcCompiler, out IDxcCompiler2? result).CheckError();
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler3? CreateDxcCompiler3()
        {
            DxcCreateInstance(CLSID_DxcCompiler, out IDxcCompiler3? result).CheckError();
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcUtils? CreateDxcUtils()
        {
            DxcCreateInstance(CLSID_DxcUtils, out IDxcUtils? result).CheckError();
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcAssembler? CreateDxcAssembler()
        {
            DxcCreateInstance(CLSID_DxcAssembler, out IDxcAssembler? result).CheckError();
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcLinker? CreateDxcLinker()
        {
            DxcCreateInstance(CLSID_DxcLinker, out IDxcLinker? result).CheckError();
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcContainerReflection? CreateDxcContainerReflection()
        {
            DxcCreateInstance(CLSID_DxcContainerReflection, out IDxcContainerReflection? result).CheckError();
            return result;
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
