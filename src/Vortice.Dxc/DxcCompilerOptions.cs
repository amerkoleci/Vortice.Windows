// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

namespace Vortice.Dxc
{
    public sealed class DxcCompilerOptions
    {
        public DxcShaderModel ShaderModel { get; set; } = DxcShaderModel.Model6_0;
        public bool EnableDebugInfo { get; set; }
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

        public int ShiftAllConstantBuffersBindings { get; set; }
        public int ShiftAllTexturesBindings { get; set; }
        public int ShiftAllSamplersBindings { get; set; }
        public int ShiftAllUAVBuffersBindings { get; set; }

        /// <summary>
        /// Generate SPIR-V code
        /// </summary>
        public bool GenerateSPIRV { get; set; } = false;

        public bool UseOpenGLLayout { get; set; } = false;
        public bool UseDirectXLayout { get; set; } = false;
        public bool UseScalarLayout { get; set; } = false;

        public bool SPIRVFlattenResourceArrays { get; set; } = false;
        public bool SPIRVReflect { get; set; } = false;
    }
}
