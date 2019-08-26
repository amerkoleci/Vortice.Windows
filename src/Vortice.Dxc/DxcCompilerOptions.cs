// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Vortice.Dxc
{
    public sealed class DxcCompilerOptions
    {
        public DxcShaderModel ShaderModel { get; set; } = DxcShaderModel.Model6_2;
        public bool PackMatricesInRowMajor { get; set; } = true;
        public bool Enable16bitTypes { get; set; } = false;
        public bool EnableDebugInfo { get; set; } = false;
        public bool DisableOptimizations { get; set; } = false;
        public int OptimizationLevel { get; set; } = 3;
        public bool AllResourcesBound { get; set; } = false;

        /// <summary>
        /// Generate SPIR-V code
        /// </summary>
        public bool GenerateSPIRV { get; set; } = false;
    }
}
