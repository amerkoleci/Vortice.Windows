// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;
using SharpGen.Runtime.Win32;

namespace Vortice.Dxc
{
    public partial class IDxcLibrary
    {
        public IDxcBlob CreateBlobFromBlob(IDxcBlob blob, int offset, int length)
        {
            CreateBlobFromBlob(blob, offset, length, out IDxcBlob result).CheckError();
            return result;
        }

        public IDxcBlobEncoding CreateBlobFromFile(string fileName, int? codePage)
        {
            CreateBlobFromFile(fileName, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlobEncoding CreateBlobWithEncodingFromPinned(IntPtr text, int size, int codePage)
        {
            CreateBlobWithEncodingFromPinned(text, size, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlobEncoding CreateBlobWithEncodingOnHeapCopy(IntPtr text, int size, int codePage)
        {
            CreateBlobWithEncodingOnHeapCopy(text, size, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlobEncoding CreateBlobWithEncodingOnMalloc(IntPtr text, ComObject malloc, int size, int codePage)
        {
            CreateBlobWithEncodingOnMalloc(text, malloc, size, codePage, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IStream CreateStreamFromBlobReadOnly(IDxcBlob blob)
        {
            CreateStreamFromBlobReadOnly(blob, out IStream result).CheckError();
            return result;
        }

        public IDxcIncludeHandler CreateIncludeHandler()
        {
            CreateIncludeHandler(out IDxcIncludeHandler result).CheckError();
            return result;
        }

        public IDxcBlobEncoding GetBlobAsUtf8(IDxcBlob blob)
        {
            GetBlobAsUtf8(blob, out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlobEncoding GetBlobAsUtf16(IDxcBlob blob)
        {
            GetBlobAsUtf16(blob, out IDxcBlobEncoding result).CheckError();
            return result;
        }
    }
}
