// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using Xunit;

namespace Vortice.Dxc.Test
{
    public class CompileTests
    {
        private const string ShaderCodeNotSignedMessage = "Shader code is not signed.";

        private string AssetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");

        [Fact]
        public void SingleFileTest()
        {
            string shaderSource = File.ReadAllText(Path.Combine(AssetsPath, "TriangleSingleFile.hlsl"));

            using IDxcResult results = DxcCompiler.Compile(DxcShaderStage.Vertex, shaderSource, "VSMain");

            Assert.True(results.GetStatus().Success);

            var shaderCode = results.GetObjectBytecodeArray();

            Assert.True(shaderCode.Length > 0);

            Assert.True(ShaderCodeHelper.IsCodeSigned(shaderCode), ShaderCodeNotSignedMessage);
        }

        [Fact]
        public void ErrorTest()
        {
            string shaderSource = File.ReadAllText(Path.Combine(AssetsPath, "TriangleError.hlsl"));

            using IDxcResult results = DxcCompiler.Compile(DxcShaderStage.Vertex, shaderSource, "VSMain");

            Assert.True(results.GetStatus().Failure);

            var error = results.GetErrors();

            Assert.Contains("error: no member named 'ThisIsAnError' in 'PSInput'", error);
        }

        [Fact]
        public void ShaderIncludeTest()
        {
            string shaderSource = File.ReadAllText(Path.Combine(AssetsPath, "Triangle.hlsl"));

            using (var includeHandler = new ShaderIncludeHandler(AssetsPath))
            {
                using IDxcResult results = DxcCompiler.Compile(DxcShaderStage.Vertex, shaderSource, "VSMain", includeHandler: includeHandler);

                Assert.True(results.GetStatus().Success);

                var shaderCode = results.GetObjectBytecodeArray();

                Assert.True(shaderCode.Length > 0);

                Assert.True(ShaderCodeHelper.IsCodeSigned(shaderCode), ShaderCodeNotSignedMessage);
            }
        }

        [Fact]
        public void DxcDefineTest()
        {
            string shaderSource = File.ReadAllText(Path.Combine(AssetsPath, "TriangleSingleFile.hlsl"));

            var defines = new DxcDefine[] { new DxcDefine { Name = "TEST", Value = "1" } };

            using IDxcResult results = DxcCompiler.Compile(DxcShaderStage.Vertex, shaderSource, "VSMain", defines: defines);

            Assert.True(results.GetStatus().Success);

            var shaderCode = results.GetObjectBytecodeArray();

            Assert.True(shaderCode.Length > 0);

            Assert.True(ShaderCodeHelper.IsCodeSigned(shaderCode), ShaderCodeNotSignedMessage);
        }
    }
}
