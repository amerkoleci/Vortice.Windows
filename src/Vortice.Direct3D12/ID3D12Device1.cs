// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using Vortice.Direct3D;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device1
    {
        public ID3D12PipelineLibrary CreatePipelineLibrary(Blob blob)
        {
            Guard.NotNull(blob, nameof(blob));
            return CreatePipelineLibrary(blob.BufferPointer, blob.BufferSize, typeof(ID3D12PipelineLibrary).GUID);
        }
    }
}
