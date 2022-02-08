// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12PipelineLibrary1
{
    public ID3D12PipelineState LoadPipeline(string name, PipelineStateStreamDescription description)
    {
        return LoadPipeline(name, ref description, typeof(ID3D12PipelineState).GUID);
    }

    public ID3D12PipelineState LoadPipeline(string name, ref PipelineStateStreamDescription description)
    {
        return LoadPipeline(name, ref description, typeof(ID3D12PipelineState).GUID);
    }
}
