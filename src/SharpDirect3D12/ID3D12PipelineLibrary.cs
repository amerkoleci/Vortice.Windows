// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpDXGI;

namespace SharpDirect3D12
{
    public partial class ID3D12PipelineLibrary
    {
        public ID3D12PipelineState LoadComputePipeline(string name, ComputePipelineStateDescription description)
        {
            Guard.NotNullOrEmpty(name, nameof(name));
            Guard.NotNull(description, nameof(description));

            return LoadComputePipeline(name, description, typeof(ID3D12PipelineState).GUID);
        }

        public unsafe ID3D12PipelineState LoadGraphicsPipeline(string name, GraphicsPipelineStateDescription description)
        {
            Guard.NotNullOrEmpty(name, nameof(name));
            Guard.NotNull(description, nameof(description));

            return LoadGraphicsPipeline(name, description, typeof(ID3D12PipelineState).GUID);
        }
    }
}
