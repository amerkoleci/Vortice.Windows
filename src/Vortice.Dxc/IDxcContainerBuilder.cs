// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Dxc
{
    public partial class IDxcContainerBuilder
    {
        public IDxcOperationResult SerializeContainer(IDxcBlob source)
        {
            SerializeContainer(out IDxcOperationResult result).CheckError();
            return result;
        }
    }
}
