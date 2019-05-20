// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.DirectX.Direct3D12
{
    public partial class ID3D12PipelineLibrary1
    {
        public ID3D12PipelineState LoadPipeline(string name, PipelineStateStreamDescription description)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            return LoadPipeline(name, ref description, typeof(ID3D12PipelineState).GUID);
        }

        public ID3D12PipelineState LoadPipeline(string name, ref PipelineStateStreamDescription description)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            return LoadPipeline(name, ref description, typeof(ID3D12PipelineState).GUID);
        }
    }
}
