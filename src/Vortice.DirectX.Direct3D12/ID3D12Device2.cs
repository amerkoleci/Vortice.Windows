// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12Device2
    {
        public ID3D12PipelineState CreatePipelineState(PipelineStateStreamDescription description)
        {
            return CreatePipelineState(ref description, typeof(ID3D12PipelineState).GUID);
        }
    }
}
