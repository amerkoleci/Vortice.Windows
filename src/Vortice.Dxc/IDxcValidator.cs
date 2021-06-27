// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public partial class IDxcValidator
    {
        public IDxcOperationResult Validate(IDxcBlob shader, DxcValidatorFlags flags)
        {
            Validate(shader, flags, out IDxcOperationResult result).CheckError();
            return result;
        }
    }
}
