// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

namespace Vortice.Dxc
{
    public sealed class DxcCompilerOptions
    {
        public DxcShaderModel ShaderModel { get; set; } = DxcShaderModel.Model6_0;
        public bool IEEEStrictness { get; set; }
        public bool PackMatrixInRowMajor { get; set; }
        public bool PackMatrixInColumnMajor { get; set; }
        public bool Enable16bitTypes { get; set; }
        public bool EnableDebugInfo { get; set; }
        public bool DisableOptimizations { get; set; }
        public int OptimizationLevel { get; set; } = 3;
        public bool AllResourcesBound { get; set; }
        public bool WarningsAreErrors { get; set; }

        public bool StripReflectionIntoSeparateBlob { get; set; } = true;

        public int ShiftAllConstantBuffersBindings { get; set; }
        public int ShiftAllTexturesBindings { get; set; }
        public int ShiftAllSamplersBindings { get; set; }
        public int ShiftAllUAVBuffersBindings { get; set; }

        /// <summary>
        /// Generate SPIR-V code
        /// </summary>
        public bool GenerateSPIRV { get; set; } = false;

        public bool UseGlLayout { get; set; } = false;
        public bool UseDxLayout { get; set; } = false;
        public bool UseScalarLayout { get; set; } = false;
    }
}
