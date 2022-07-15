// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using SharpGen.Runtime.Win32;

namespace Vortice.Dxc;

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

    public IDxcBlobEncoding GetBlobAsWide(IDxcBlob blob)
    {
        GetBlobAsWide(blob, out IDxcBlobEncoding result).CheckError();
        return result;
    }

    public Result GetBlobAsUtf16(IDxcBlob blob, out IDxcBlobEncoding blobEncoding)
    {
        return GetBlobAsWide(blob, out blobEncoding);
    }

    public IDxcBlobEncoding GetBlobAsUtf16(IDxcBlob blob)
    {
        GetBlobAsWide(blob, out IDxcBlobEncoding result).CheckError();
        return result;
    }
}
