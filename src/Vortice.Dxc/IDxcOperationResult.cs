// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcOperationResult
    {
        public IDxcBlobEncoding GetErrorBuffer()
        {
            GetErrorBuffer(out IDxcBlobEncoding result).CheckError();
            return result;
        }

        public IDxcBlob GetResult()
        {
            GetResult(out IDxcBlob result).CheckError();
            return result;
        }

        public Result GetStatus()
        {
            GetStatus(out Result result).CheckError();
            return result;
        }
    }
}
