// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Implementation based on https://github.com/tgjones/DotNetDxc

using System;
using System.Collections.Generic;
using SharpGen.Runtime;

namespace Vortice.Dxc
{
    public static class DxcCompiler
    {
        public static readonly IDxcUtils? Utils = Dxc.CreateDxcUtils();
        public static readonly IDxcCompiler3? Compiler = Dxc.CreateDxcCompiler3();

        public static IDxcResult? Compile(string shaderSource, string[] arguments, IDxcIncludeHandler? includeHandler = null)
        {
            if (includeHandler == null)
            {
                includeHandler = Utils!.CreateDefaultIncludeHandler();
                try
                {
                    return Compiler!.Compile(shaderSource, arguments, includeHandler);
                }
                finally
                {
                    MemoryHelpers.Dispose(ref includeHandler);
                }
            }
            else
            {
                return Compiler!.Compile(shaderSource, arguments, includeHandler);
            }
        }

        public static IDxcResult? Compile(DxcShaderStage shaderStage, string source, string entryPoint,
            DxcCompilerOptions? options = null,
            string? fileName = null,
            DxcDefine[]? defines = null,
            IDxcIncludeHandler? includeHandler = null,
            string[]? additionalArguments = null)
        {
            if (options == null)
            {
                options = new DxcCompilerOptions();
            }

            string profile = GetShaderProfile(shaderStage, options.ShaderModel);

            var arguments = new List<string>();
            if (!string.IsNullOrEmpty(fileName))
            {
                arguments.Add(fileName!);
            }

            arguments.Add("-E");
            arguments.Add(entryPoint);

            arguments.Add("-T");
            arguments.Add(profile);

            // Defines
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

            if (options.EnableDebugInfo)
            {
                arguments.Add("-Zi");
            }

            if (options.SkipValidation)
            {
                arguments.Add("-Vd");
            }

            if (options.SkipOptimizations)
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

            // HLSL matrices are translated into SPIR-V OpTypeMatrixs in a transposed manner,
            // See also https://antiagainst.github.io/post/hlsl-for-vulkan-matrices/
            if (options.PackMatrixRowMajor)
            {
                arguments.Add("-Zpr");
            }
            if (options.PackMatrixColumnMajor)
            {
                arguments.Add("-Zpc");
            }
            if (options.AvoidFlowControl)
            {
                arguments.Add("-Gfa");
            }
            if (options.PreferFlowControl)
            {
                arguments.Add("-Gfp");
            }

            if (options.EnableStrictness)
            {
                arguments.Add("-Ges");
            }

            if (options.EnableBackwardCompatibility)
            {
                arguments.Add("-Gec");
            }

            if (options.IEEEStrictness)
            {
                arguments.Add("-Gis");
            }

            if (options.WarningsAreErrors)
            {
                arguments.Add("-WX");
            }

            if (options.ResourcesMayAlias)
            {
                arguments.Add("-res_may_alias");
            }

            if (options.AllResourcesBound)
            {
                arguments.Add("-all_resources_bound");
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

            if (options.StripReflectionIntoSeparateBlob)
            {
                arguments.Add("-Qstrip_reflect");
            }

            if (options.GenerateSPIRV)
            {
                arguments.Add("-spirv");
            }

            // HLSL version, default 2018.
            arguments.Add("-HV");
            arguments.Add($"{options.HLSLVersion}");

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

            if (options.UseOpenGLLayout)
                arguments.Add("-fvk-use-gl-layout");
            if (options.UseDirectXLayout)
                arguments.Add("-fvk-use-dx-layout");
            if (options.UseScalarLayout)
                arguments.Add("-fvk-use-scalar-layout");

            if (options.SPIRVFlattenResourceArrays)
                arguments.Add("-fspv-flatten-resource-arrays");
            if (options.SPIRVReflect)
                arguments.Add("-fspv-reflect");

            arguments.Add("-fspv-target-env=vulkan1.1");

            if (additionalArguments != null && additionalArguments.Length > 0)
            {
                arguments.AddRange(additionalArguments);
            }

            return Compile(source, arguments.ToArray(), includeHandler);
        }

        private static string GetShaderProfile(DxcShaderStage shaderStage, DxcShaderModel shaderModel)
        {
            string shaderProfile;
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
