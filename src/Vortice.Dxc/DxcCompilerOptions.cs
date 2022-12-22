// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Dxc;

public sealed class DxcCompilerOptions
{
    public DxcShaderModel ShaderModel { get; set; } = DxcShaderModel.Model6_0;
    public bool EnableDebugInfo { get; set; }
    public bool EnableDebugInfoSlimFormat { get; set; }
    public bool SkipValidation { get; set; }
    public bool SkipOptimizations { get; set; }
    public bool PackMatrixRowMajor { get; set; }
    public bool PackMatrixColumnMajor { get; set; }
    public bool AvoidFlowControl { get; set; }
    public bool PreferFlowControl { get; set; }
    public bool EnableStrictness { get; set; }
    public bool EnableBackwardCompatibility { get; set; }
    public bool IEEEStrictness { get; set; }
    public bool Enable16bitTypes { get; set; }
    public int OptimizationLevel { get; set; } = 3;
    public bool WarningsAreErrors { get; set; }
    public bool ResourcesMayAlias { get; set; }
    public bool AllResourcesBound { get; set; }

    public int HLSLVersion { get; set; } = 2018;

    public bool StripReflectionIntoSeparateBlob { get; set; } = true;

    public int VkBufferShift { get; set; }
    public int VkBufferShiftSet { get; set; }
    public int VkTextureShift { get; set; }
    public int VkTextureShiftSet { get; set; }
    public int VkSamplerShift { get; set; }
    public int VkSamplerShiftSet { get; set; }
    public int VkUAVShift { get; set; }
    public int VkUAVShiftSet { get; set; }

    /// <summary>
    /// Generate SPIR-V code
    /// </summary>
    public bool GenerateSpirv { get; set; } = false;

    public bool VkUseGLLayout { get; set; } = false;
    public bool VkUseDXLayout { get; set; } = true;
    public bool VkUseScalarLayout { get; set; } = false;
    public bool VkUseDXPositionW { get; set; } = true;
    public bool SpvFlattenResourceArrays { get; set; } = false;
    public bool SpvReflect { get; set; } = false;
    public int SpvTargetEnvMajor { get; set; } = 1;
    public int SpirvTargetEnvMinor { get; set; } = 1;
}
