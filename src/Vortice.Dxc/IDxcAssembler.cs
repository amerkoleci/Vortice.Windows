// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Dxc
{
    public partial class IDxcAssembler
    {
        public IDxcOperationResult AssembleToContainer(IDxcBlob shader)
        {
            AssembleToContainer(shader, out IDxcOperationResult result).CheckError();
            return result;
        }
    }
}
