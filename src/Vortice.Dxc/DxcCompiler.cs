// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.Collections.Generic;

namespace Vortice.Dxc
{
    public static class DxcCompiler
    {
        public static readonly IDxcUtils Utils = Dxc.CreateDxcUtils();
        public static readonly IDxcCompiler3 Compiler = Dxc.CreateDxcCompiler3();

        public static IDxcResult Compile(string shaderSource, string[] arguments, IDxcIncludeHandler includeHandler = null)
        {
            if (includeHandler == null)
            {
                using (includeHandler = Utils.CreateDefaultIncludeHandler())
                {
                    return Compiler.Compile(shaderSource, arguments, includeHandler);
                }
            }
            else
            {
                return Compiler.Compile(shaderSource, arguments, includeHandler);
            }
        }

        public static IDxcResult Compile(DxcShaderStage shaderStage, string source, string entryPoint,
            DxcCompilerOptions options = null,
            string fileName = null,
            DxcDefine[] defines = null,
            IDxcIncludeHandler includeHandler = null)
        {
            if (options == null)
                options = new DxcCompilerOptions();

            string profile = GetShaderProfile(shaderStage, options.ShaderModel);

            var arguments = new List<string>();
            if (!string.IsNullOrEmpty(fileName))
            {
                arguments.Add(fileName);
            }

            arguments.Add("-E");
            arguments.Add(entryPoint);

            arguments.Add("-T");
            arguments.Add(profile);

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

            if (options.WarningsAreErrors)
            {
                arguments.Add("-WX");
            }

            if (options.AllResourcesBound)
            {
                arguments.Add("-all_resources_bound");
            }

            if (options.StripReflectionIntoSeparateBlob)
            {
                arguments.Add("-Qstrip_reflect");
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

            if (defines != null && defines.Length > 0)
            {
                foreach (DxcDefine define in defines)
                {
                    string defineValue = define.Value;
                    if (string.IsNullOrEmpty(defineValue))
                        defineValue = "1";

                    arguments.Add("-D");
                    arguments.Add($"{define.Name}={defineValue}");
                }
            }

            if (includeHandler == null)
            {
                using (includeHandler = Utils.CreateDefaultIncludeHandler())
                {
                    return Compiler.Compile(source, arguments.ToArray(), includeHandler);
                }
            }
            else
            {
                return Compiler.Compile(source, arguments.ToArray(), includeHandler);
            }
        }

        private static string GetShaderProfile(DxcShaderStage shaderStage, DxcShaderModel shaderModel)
        {
            string shaderProfile = "";
            switch (shaderStage)
            {
                case DxcShaderStage.Vertex:
                    shaderProfile = "vs";
                    break;
                case DxcShaderStage.Pixel:
                    shaderProfile = "ps";
                    break;
                case DxcShaderStage.Geometry:
                    shaderProfile = "gs";
                    break;
                case DxcShaderStage.Hull:
                    shaderProfile = "hs";
                    break;
                case DxcShaderStage.Domain:
                    shaderProfile = "ds";
                    break;
                case DxcShaderStage.Compute:
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
