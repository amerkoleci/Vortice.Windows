// Copyright � Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Dxc.Test;

[TestFixture(TestOf = typeof(DxcCompiler))]
public class CompileTests
{
    private const string ShaderCodeNotSignedMessage = "Shader code is not signed.";

    private string AssetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");

    [TestCase]
    public void SingleFileTest()
    {
        string shaderSource = File.ReadAllText(Path.Combine(AssetsPath, "TriangleSingleFile.hlsl"));

        using IDxcResult results = DxcCompiler.Compile(DxcShaderStage.Vertex, shaderSource, "VSMain");

        Assert.True(results.GetStatus().Success);

        var shaderCode = results.GetObjectBytecodeArray();

        Assert.True(shaderCode.Length > 0);

        Assert.True(ShaderCodeHelper.IsCodeSigned(shaderCode), ShaderCodeNotSignedMessage);
    }

    [TestCase]
    public void ErrorTest()
    {
        string shaderSource = File.ReadAllText(Path.Combine(AssetsPath, "TriangleError.hlsl"));

        using IDxcResult results = DxcCompiler.Compile(DxcShaderStage.Vertex, shaderSource, "VSMain");

        Assert.True(results.GetStatus().Failure);

        var errorTest = results.GetErrors();

        Assert.True(errorTest!.Contains("error: no member named 'ThisIsAnError' in 'PSInput'"));
    }

    [TestCase]
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

    [TestCase]
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
