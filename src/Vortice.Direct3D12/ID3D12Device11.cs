// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class ID3D12Device11
{
    public void CreateSampler2(SamplerDescription2 desc, CpuDescriptorHandle destDescriptor)
    {
        CreateSampler2(ref desc, destDescriptor);
    }
}
