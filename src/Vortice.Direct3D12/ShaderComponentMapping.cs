// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Shader component mapping.
/// </summary>
public static partial class ShaderComponentMapping
{
    /// <unmanaged>D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING </unmanaged>
    /// <unmanaged-short>D3D12_DEFAULT_SHADER_4_COMPONENT_MAPPING </unmanaged-short>
    public static readonly int Default = Encode(ShaderComponentMappingSource.FromMemoryComponent0,
                                                ShaderComponentMappingSource.FromMemoryComponent1,
                                                ShaderComponentMappingSource.FromMemoryComponent2,
                                                ShaderComponentMappingSource.FromMemoryComponent3);


    /// <unmanaged>D3D12_ENCODE_SHADER_4_COMPONENT_MAPPING </unmanaged>
    /// <unmanaged-short>D3D12_ENCODE_SHADER_4_COMPONENT_MAPPING </unmanaged-short>
    public static int Encode(ShaderComponentMappingSource source0, ShaderComponentMappingSource source1, ShaderComponentMappingSource source2, ShaderComponentMappingSource source3)
    {
        return (((((int)source0) & Mask) |
               ((((int)source1) & Mask) << Shift) |
               ((((int)source2) & Mask) << (Shift * 2)) |
               ((((int)source3) & Mask) << (Shift * 3)) |
               AlwaysSetBitAvoidingZeromemMistakes));
    }

    /// <unmanaged>D3D12_DECODE_SHADER_4_COMPONENT_MAPPING </unmanaged>
    /// <unmanaged-short>D3D12_DECODE_SHADER_4_COMPONENT_MAPPING </unmanaged-short>
    public static void Decode(int mapping, out ShaderComponentMappingSource source0, out ShaderComponentMappingSource source1, out ShaderComponentMappingSource source2, out ShaderComponentMappingSource source3)
    {
        source0 = Decode(0, mapping);
        source1 = Decode(1, mapping);
        source2 = Decode(2, mapping);
        source3 = Decode(3, mapping);
    }

    /// <unmanaged>D3D12_DECODE_SHADER_4_COMPONENT_MAPPING </unmanaged>
    /// <unmanaged-short>D3D12_DECODE_SHADER_4_COMPONENT_MAPPING </unmanaged-short>
    public static ShaderComponentMappingSource Decode(int componentToExtract, int mapping)
    {
        return (ShaderComponentMappingSource)(mapping >> (Shift * componentToExtract) & Mask);
    }
}
