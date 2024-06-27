// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12PipelineLibrary
{
    public ID3D12PipelineState LoadComputePipeline(string name, ComputePipelineStateDescription description)
    {
        return LoadComputePipeline(name, description, typeof(ID3D12PipelineState).GUID);
    }

    public unsafe ID3D12PipelineState LoadGraphicsPipeline(string name, GraphicsPipelineStateDescription description)
    {
        return LoadGraphicsPipeline(name, description, typeof(ID3D12PipelineState).GUID);
    }
}
