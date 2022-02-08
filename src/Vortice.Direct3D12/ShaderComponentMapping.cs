// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Shader component mapping.
/// </summary>
public static partial class ShaderComponentMapping
{
    public static readonly int Default = Encode(ShaderComponentMappingSource.FromMemoryComponent0,
                                                ShaderComponentMappingSource.FromMemoryComponent1,
                                                ShaderComponentMappingSource.FromMemoryComponent2,
                                                ShaderComponentMappingSource.FromMemoryComponent3);

    public static int Encode(ShaderComponentMappingSource source0, ShaderComponentMappingSource source1, ShaderComponentMappingSource source2, ShaderComponentMappingSource source3)
    {
        return (((((int)source0) & Mask) |
               ((((int)source1) & Mask) << Shift) |
               ((((int)source2) & Mask) << (Shift * 2)) |
               ((((int)source3) & Mask) << (Shift * 3)) |
               AlwaysSetBitAvoidingZeromemMistakes));
    }

    public static void Decode(int mapping, out ShaderComponentMappingSource source0, out ShaderComponentMappingSource source1, out ShaderComponentMappingSource source2, out ShaderComponentMappingSource source3)
    {
        source0 = Decode(mapping, 0);
        source1 = Decode(mapping, 1);
        source2 = Decode(mapping, 2);
        source3 = Decode(mapping, 3);
    }

    public static ShaderComponentMappingSource Decode(int mapping, int component)
    {
        return (ShaderComponentMappingSource)(mapping >> (Shift * component) & Mask);
    }
}
