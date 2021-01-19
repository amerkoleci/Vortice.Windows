// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

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
    }
}
