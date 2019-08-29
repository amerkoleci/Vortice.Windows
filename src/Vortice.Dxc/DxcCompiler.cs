// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.Collections.Generic;

namespace Vortice.Dxc
{
    public static class DxcCompiler
    {
        public static readonly IDxcLibrary Library = Dxc.CreateDxcLibrary();

        public static IDxcOperationResult Compile(
            DxcShaderStage shaderStage,
            string source,
            string entryPoint,
            string sourceName,
            DxcCompilerOptions options,
            DXCDefine[] defines = null,
            IDxcIncludeHandler include = null)
        {
            var shaderProfile = GetShaderProfile(shaderStage, options.ShaderModel);
            if (options == null)
            {
                options = new DxcCompilerOptions();
            }

            var arguments = new List<string>();

            if (options.IEEEStrictness)
            {
                arguments.Add("-Gis");
            }

            // HLSL matrices are translated into SPIR-V OpTypeMatrixs in a transposed manner,
            // See also https://antiagainst.github.io/post/hlsl-for-vulkan-matrices/
            if (options.PackMatrixInColumnMajor)
            {
                arguments.Add("-Zpc");
            }
            else if (options.PackMatrixInRowMajor)
            {
                arguments.Add("-Zpr");
            }

            if (options.Enable16bitTypes)
            {
                if (options.ShaderModel.Major >= 6
                    && options.ShaderModel.Minor >= 2)
                {
                    arguments.Add("-enable-16bit-types");
                }
                else
                {
                    throw new InvalidOperationException("16-bit types requires shader model 6.2 or up.");
                }
            }

            if (options.EnableDebugInfo)
            {
                arguments.Add("-Zi");
            }

            if (options.DisableOptimizations)
            {
                arguments.Add("-Od");
            }
            else
            {
                if (options.OptimizationLevel < 4)
                {
                    arguments.Add($"-O{options.OptimizationLevel}");
                }
                else
                {
                    throw new InvalidOperationException("Invalid optimization level.");
                }
            }

            if (options.AllResourcesBound)
            {
                arguments.Add("-all_resources_bound");
            }

            if (options.GenerateSPIRV)
            {
                arguments.Add("-spirv");
            }

            if (options.ShiftAllConstantBuffersBindings > 0)
            {
                arguments.Add("-fvk-b-shift");
                arguments.Add($"{options.ShiftAllConstantBuffersBindings}");
                arguments.Add($"all");
            }

            if (options.ShiftAllTexturesBindings > 0)
            {
                arguments.Add("-fvk-t-shift");
                arguments.Add($"{options.ShiftAllTexturesBindings}");
                arguments.Add($"all");
            }

            if (options.ShiftAllSamplersBindings > 0)
            {
                arguments.Add("-fvk-s-shift");
                arguments.Add($"{options.ShiftAllSamplersBindings}");
                arguments.Add($"all");
            }

            if (options.ShiftAllUAVBuffersBindings > 0)
            {
                arguments.Add("-fvk-u-shift");
                arguments.Add($"{options.ShiftAllUAVBuffersBindings}");
                arguments.Add($"all");
            }

            if (include == null)
            {
                include = Library.CreateIncludeHandler();
            }

            var compiler = Dxc.CreateDxcCompiler();
            return compiler.Compile(
                Dxc.CreateBlobForText(Library, source),
                sourceName,
                entryPoint,
                shaderProfile,
                arguments.ToArray(),
                arguments.Count,
                defines,
                defines != null ? defines.Length : 0,
                include
                );
        }

        private static string GetShaderProfile(DxcShaderStage shaderStage, DxcShaderModel shaderModel)
        {
            var shaderProfile = "";
            switch (shaderStage)
            {
                case DxcShaderStage.VertexShader:
                    shaderProfile = "vs";
                    break;
                case DxcShaderStage.PixelShader:
                    shaderProfile = "ps";
                    break;
                case DxcShaderStage.GeometryShader:
                    shaderProfile = "gs";
                    break;
                case DxcShaderStage.HullShader:
                    shaderProfile = "hs";
                    break;
                case DxcShaderStage.DomainShader:
                    shaderProfile = "ds";
                    break;
                case DxcShaderStage.ComputeShader:
                    shaderProfile = "cs";
                    break;
                case DxcShaderStage.Library:
                    shaderProfile = "lib";
                    break;
                default:
                    return string.Empty;
            }

            shaderProfile += $"_{shaderModel.Major}_{shaderModel.Minor}";
            return shaderProfile;
        }
    }
}
