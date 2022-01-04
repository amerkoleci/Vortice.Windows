// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D;
using Xunit;

namespace Vortice.D3DCompiler.Test;

public class CompileTests
{
    private string AssetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");

    [Fact]
    public void SingleFileTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "TriangleSingleFile.hlsl");

        var result = Compiler.CompileFromFile(shaderSourceFile, null, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.True(result.Success);

        var shaderCode = blob.GetBytes();

        Assert.True(shaderCode.Length > 0);
    }

    [Fact]
    public void ErrorTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "TriangleError.hlsl");

        var result = Compiler.CompileFromFile(shaderSourceFile, null, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.True(result.Failure);

        var errorText = error?.ConvertToString();

        Assert.Contains("error X3018: invalid subscript 'ThisIsAnError'", errorText);
    }

    [Fact]
    public void ShaderIncludeTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "Triangle.hlsl");

        using (var includeHandler = new ShaderIncludeHandler(AssetsPath))
        {
            var result = Compiler.CompileFromFile(shaderSourceFile, null, includeHandler, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

            Assert.True(result.Success);

            var shaderCode = blob.GetBytes();

            Assert.True(shaderCode.Length > 0);
        }
    }

    [Fact]
    public void ShaderMacroTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "TriangleSingleFile.hlsl");

        var defines = new[] { new ShaderMacro { Name = "TEST", Definition = "1" } };

        var result = Compiler.CompileFromFile(shaderSourceFile, defines, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.True(result.Success);

        var shaderCode = blob.GetBytes();

        Assert.True(shaderCode.Length > 0);
    }
}
