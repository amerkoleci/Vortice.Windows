// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using NativeLibraryLoader;

namespace Vortice.Dxc
{
    [ComImport]
    [Guid("8BA5FB08-5195-40e2-AC58-0D989C3A0102")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcBlob
    {
        [PreserveSig]
        unsafe char* GetBufferPointer();

        [PreserveSig]
        uint GetBufferSize();
    }

    [ComImport]
    [Guid("8BA5FB08-5195-40e2-AC58-0D989C3A0102")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcBlobEncoding : IDxcBlob
    {
        uint GetEncoding(out bool unknown, out uint codePage);
    }

    [ComImport]
    [Guid("CEDB484A-D4E9-445A-B991-CA21CA157DC2")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcOperationResult
    {
        int GetStatus();
        IDxcBlob GetResult();
        IDxcBlobEncoding GetErrors();
    }

    [ComImport]
    [Guid("7f61fc7d-950d-467f-b3e3-3c02fb49187c")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcIncludeHandler
    {
        IDxcBlob LoadSource(string fileName);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DXCEncodedText
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string pText;

        public uint Size;

        public uint CodePage; //should always be UTF-8 for this use
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DXCDefine
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pName;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string pValue;
    }

    [ComImport]
    [Guid("e5204dc7-d18c-4c3c-bdfb-851673980fe7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcLibrary
    {
        void SetMalloc(object malloc);
        IDxcBlob CreateBlobFromBlob(IDxcBlob blob, UInt32 offset, UInt32 length);
        IDxcBlobEncoding CreateBlobFromFile(string fileName, System.IntPtr codePage);
        IDxcBlobEncoding CreateBlobWithEncodingFromPinned(byte[] text, UInt32 size, UInt32 codePage);
        // IDxcBlobEncoding CreateBlobWithEncodingOnHeapCopy(IntrPtr text, UInt32 size, UInt32 codePage);
        IDxcBlobEncoding CreateBlobWithEncodingOnHeapCopy(string text, UInt32 size, UInt32 codePage);
        IDxcBlobEncoding CreateBlobWithEncodingOnMalloc(string text, object malloc, UInt32 size, UInt32 codePage);
        IDxcIncludeHandler CreateIncludeHandler();
        IStream CreateStreamFromBlobReadOnly(IDxcBlob blob);
        IDxcBlobEncoding GetBlobAstUf8(IDxcBlob blob);
        IDxcBlobEncoding GetBlobAstUf16(IDxcBlob blob);
    }

    [ComImport]
    [Guid("8c210bf3-011f-4422-8d70-6f9acb8db617")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcCompiler
    {
        IDxcOperationResult Compile(IDxcBlob source, string sourceName, string entryPoint, string targetProfile,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType =UnmanagedType.LPWStr)]
            string[] arguments,
            int argCount, DXCDefine[] defines, int defineCount, IDxcIncludeHandler includeHandler);
        IDxcOperationResult Preprocess(IDxcBlob source, string sourceName, string[] arguments, int argCount,
            DXCDefine[] defines, int defineCount, IDxcIncludeHandler includeHandler);
        IDxcBlobEncoding Disassemble(IDxcBlob source);
    }



    [ComImport]
    [Guid("091f7a26-1c1f-4948-904b-e6e3a8a771d5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcAssembler
    {
        IDxcOperationResult AssembleToContainer(IDxcBlob source);
    }

    [ComImport]
    [Guid("F1B5BE2A-62DD-4327-A1C2-42AC1E1E78E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcLinker : IDxcCompiler
    {
        // Register a library with name to ref it later.
        int RegisterLibrary(string libName, IDxcBlob library);

        int Link(
            string entryName,
            string targetProfile,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType =UnmanagedType.LPWStr)]
            string[] libNames,
            int libCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType =UnmanagedType.LPWStr)]
            string[] pArguments,
            int argCount,
            out IDxcOperationResult result
            );
    }

    [ComImport]
    [Guid("d2c21b26-8350-4bdc-976a-331ce6f4c54c")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcContainerReflection
    {
        void Load(IDxcBlob container);
        uint GetPartCount();
        uint GetPartKind(uint idx);
        IDxcBlob GetPartContent(uint idx);
        [PreserveSig]
        int FindFirstPartKind(uint kind, out uint result);

        int GetPartReflection(uint idx, ref Guid iid, out IntPtr ppvObject);
    }

    /// <summary>
    /// Use this interface to represent a file (saved or in-memory).
    /// </summary>
    [ComImport]
    [Guid("bb2fca9e-1478-47ba-b08c-2c502ada4895")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcFile
    {
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetName();
    }

    /// <summary>
    /// Describes a location in a source file.
    /// </summary>
    [Guid("8e7ddf1c-d7d3-4d69-b286-85fccba1e0cf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcSourceLocation
    {
        bool IsEqualTo(IDxcSourceLocation other);
        void GetSpellingLocation(out IDxcFile file, out uint line, out uint col, out uint offset);
    }

    /// <summary>
    /// Describes a range of text in a source file.
    /// </summary>
    [ComImport]
    [Guid("f1359b36-a53f-4e81-b514-b6b84122a13f")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcSourceRange
    {
        bool IsNull();
        IDxcSourceLocation GetStart();
        IDxcSourceLocation GetEnd();
    }

    [ComImport]
    [Guid("2ec912fd-b144-4a15-ad0d-1c5439c81e46")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcType
    {
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetSpelling();
        bool IsEqualTo(IDxcType other);
    };


    [ComImport]
    [Guid("8ec00f98-07d0-4e60-9d7c-5a50b5b0017f")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcUnsavedFile
    {
        void GetFileName([MarshalAs(UnmanagedType.LPStr)] out string value);
        void GetContents([MarshalAs(UnmanagedType.LPStr)] out string value);
        void GetLength(out uint length);
    }

    /// <summary>
    /// A cursor representing some element in the abstract syntax tree for
    /// a translation unit.
    /// </summary>
    /// <remarks>
    /// The cursor abstraction unifies the different kinds of entities in a
    /// program--declaration, statements, expressions, references to declarations,
    /// etc.--under a single "cursor" abstraction with a common set of operations.
    /// Common operation for a cursor include: getting the physical location in
    /// a source file where the cursor points, getting the name associated with a
    /// cursor, and retrieving cursors for any child nodes of a particular cursor.
    /// </remarks>
    [ComImport]
    [Guid("1467b985-288d-4d2a-80c1-ef89c42c40bc")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcCursor
    {
        IDxcSourceRange GetExtent();
        IDxcSourceLocation GetLocation();
        // <summary>Describes what kind of construct this cursor refers to.</summary>
        DxcCursorKind GetCursorKind();
        DxcCursorKindFlags GetCursorKindFlags();
        IDxcCursor GetSemanticParent();
        IDxcCursor GetLexicalParent();
        IDxcType GetCursorType();
        int GetNumArguments();
        IDxcCursor GetArgumentAt(int index);
        IDxcCursor GetReferencedCursor();
        IDxcCursor GetDefinitionCursor();
        void FindReferencesInFile(IDxcFile file, uint skip, uint top, out uint resultLength,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 3)]
            out IDxcCursor[] cursors);
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetSpelling();
        bool IsEqualTo(IDxcCursor other);
        bool IsNull();
    }

    /// <summary>
    /// Use this interface to represent the context in which translation units are parsed.
    /// </summary>
    [ComImport]
    [Guid("937824a0-7f5a-4815-9ba7-7fc0424f4173")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcIndex
    {
        void SetGlobalOptions(DxcGlobalOptions options);
        DxcGlobalOptions GetGlobalOptions();
        IDxcTranslationUnit ParseTranslationUnit(
          [MarshalAs(UnmanagedType.LPStr)] string source_filename,
          [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] commandLineArgs,
          int num_command_line_args,
          [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface)] IDxcUnsavedFile[] unsavedFiles,
          uint num_unsaved_files,
          uint options);
    }

    /// <summary>
    /// Describes a single preprocessing token.
    /// </summary>
    [ComImport]
    [Guid("7f90b9ff-a275-4932-97d8-3cfd234482a2")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcToken
    {
        DxcTokenKind GetKind();
        IDxcSourceLocation GetLocation();
        IDxcSourceRange GetExtent();
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetSpelling();
    }

    [ComImport]
    [Guid("4f76b234-3659-4d33-99b0-3b0db994b564")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcDiagnostic
    {
        [return: MarshalAs(UnmanagedType.LPStr)]
        string FormatDiagnostic(DxcDiagnosticDisplayOptions options);
        DxcDiagnosticSeverity GetSeverity();
        IDxcSourceLocation GetLocation();
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetSpelling();
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetCategoryText();
        uint GetNumRanges();
        IDxcSourceRange GetRangeAt(uint index);
        uint GetNumFixIts();
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetFixItAt(uint index, out IDxcSourceRange range);
    }

    [ComImport]
    [Guid("9677dee0-c0e5-46a1-8b40-3db3168be63d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcTranslationUnit
    {
        IDxcCursor GetCursor();
        void Tokenize(IDxcSourceRange range,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex=2)]
            out IDxcToken[] tokens,
            out uint tokenCount);
        IDxcSourceLocation GetLocation(IDxcFile file, uint line, uint column);
        uint GetNumDiagnostics();
        IDxcDiagnostic GetDiagnosticAt(uint index);
        IDxcFile GetFile([MarshalAs(UnmanagedType.LPStr)]string name);
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetFileName();
        void Reparse(
          [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface)] IDxcUnsavedFile[] unsavedFiles,
          uint num_unsaved_files);
        IDxcCursor GetCursorForLocation(IDxcSourceLocation location);
        IDxcSourceLocation GetLocationForOffset(IDxcFile file, UInt32 offset);
        void GetSkippedRanges(IDxcFile file,
            out uint rangeCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex=1)]
            out IDxcSourceRange[] ranges);
        void GetDiagnosticDetails(UInt32 index, DxcDiagnosticDisplayOptions options,
            out UInt32 errorCode,
            out UInt32 errorLine,
            out UInt32 errorColumn,
            [MarshalAs(UnmanagedType.BStr)]
            out string errorFile,
            out UInt32 errorOffset,
            out UInt32 errorLength,
            [MarshalAs(UnmanagedType.BStr)]
            out string errorMessage);
    }


    [ComImport]
    [Guid("b1f99513-46d6-4112-8169-dd0d6053f17d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDxcIntelliSense
    {
        IDxcIndex CreateIndex();
        IDxcSourceLocation GetNullLocation();
        IDxcSourceRange GetNullRange();
        IDxcSourceRange GetRange(IDxcSourceLocation start, IDxcSourceLocation end);
        DxcDiagnosticDisplayOptions GetDefaultDiagnosticDisplayOptions();
        DxcTranslationUnitFlags GetDefaultEditingTUOptions();
    }

    public delegate int DxcCreateInstanceFn(ref Guid clsid, ref Guid iid, [MarshalAs(UnmanagedType.IUnknown)] out object instance);

    public static class Dxc
    {
        private static Guid CLSID_DxcAssembler = new Guid("D728DB68-F903-4F80-94CD-DCCF76EC7151");
        private static Guid CLSID_DxcDiaDataSource = new Guid("CD1F6B73-2AB0-484D-8EDC-EBE7A43CA09F");
        private static Guid CLSID_DxcIntelliSense = new Guid("3047833c-d1c0-4b8e-9d40-102878605985");
        private static Guid CLSID_DxcRewriter = new Guid("b489b951-e07f-40b3-968d-93e124734da4");
        private static Guid CLSID_DxcCompiler = new Guid("73e22d93-e6ce-47f3-b5bf-f0664f39c1b0");
        private static Guid CLSID_DxcLinker = new Guid("EF6A8087-B0EA-4D56-9E45-D07E1A8B7806");
        private static Guid CLSID_DxcContainerReflection = new Guid("b9f54489-55b8-400c-ba3a-1675e4728b91");
        private static Guid CLSID_DxcLibrary = new Guid("6245D6AF-66E0-48FD-80B4-4D271796748C");
        private static Guid CLSID_DxcOptimizer = new Guid("AE2CD79F-CC22-453F-9B6B-B124E7A5204C");
        private static Guid CLSID_DxcValidator = new Guid("8CA3E215-F728-4CF3-8CDD-88AF917587A1");

        public const uint DFCC_DXIL = 1279875140;
        public const uint DFCC_SHDR = 1380206675;
        public const uint DFCC_SHEX = 1480935507;
        public const uint DFCC_ILDB = 1111772233;
        public const uint DFCC_SPDB = 1111773267;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcCompiler CreateDxcCompiler()
        {
            DxcCreateInstance(CLSID_DxcCompiler, typeof(IDxcCompiler).GUID, out object result);
            return (IDxcCompiler)result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcLibrary CreateDxcLibrary()
        {
            DxcCreateInstance(CLSID_DxcLibrary, typeof(IDxcLibrary).GUID, out object result);
            return (IDxcLibrary)result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcAssembler CreateDxcAssembler()
        {
            DxcCreateInstance(CLSID_DxcAssembler, typeof(IDxcAssembler).GUID, out object result);
            return (IDxcAssembler)result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcLinker CreateDxcLinker()
        {
            DxcCreateInstance(CLSID_DxcLinker, typeof(IDxcLinker).GUID, out object result);
            return (IDxcLinker)result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcContainerReflection CreateDxcContainerReflection()
        {
            DxcCreateInstance(CLSID_DxcContainerReflection, typeof(IDxcContainerReflection).GUID, out object result);
            return (IDxcContainerReflection)result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IDxcIntelliSense CreateDxcIntelliSense()
        {
            DxcCreateInstance(CLSID_DxcIntelliSense, typeof(IDxcIntelliSense).GUID, out object result);
            return (IDxcIntelliSense)result;
        }

        public static IDxcBlobEncoding CreateBlobForText(IDxcLibrary library, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            const uint CP_UTF16 = 1200;
            return library.CreateBlobWithEncodingOnHeapCopy(text, (uint)(text.Length * 2), CP_UTF16);
        }

        public static string GetStringFromBlob(IDxcLibrary library, IDxcBlob blob)
        {
            unsafe
            {
                blob = library.GetBlobAstUf16(blob);
                return new string(blob.GetBufferPointer(), 0, (int)(blob.GetBufferSize() / 2));
            }
        }

        public static byte[] GetBytesFromBlob(IDxcBlob blob)
        {
            unsafe
            {
                byte* pMem = (byte*)blob.GetBufferPointer();
                uint size = blob.GetBufferSize();
                byte[] result = new byte[size];
                fixed (byte* pTarget = result)
                {
                    for (uint i = 0; i < size; ++i)
                        pTarget[i] = pMem[i];
                }
                return result;
            }
        }

        private static readonly NativeLibrary s_dxilLib;
        private static readonly NativeLibrary s_dxCompilerLib;

        static Dxc()
        {
            s_dxilLib = new NativeLibrary("dxil.dll");
            s_dxCompilerLib = new NativeLibrary("dxcompiler.dll");
            DxcCreateInstanceFn = s_dxCompilerLib.LoadFunction<DxcCreateInstanceFn>("DxcCreateInstance");
        }

        public static DxcCreateInstanceFn DxcCreateInstanceFn;

        private static int DxcCreateInstance(Guid clsid, Guid iid, out object instance)
        {
            return DxcCreateInstanceFn(ref clsid, ref iid, out instance);
        }
    }
}
