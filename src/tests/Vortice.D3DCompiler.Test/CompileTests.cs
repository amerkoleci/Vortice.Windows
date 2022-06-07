// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;
using Vortice.Direct3D;

namespace Vortice.D3DCompiler.Test;

[TestFixture(TestOf = typeof(Compiler))]
public class CompileTests
{
    private string AssetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");

    [TestCase]
    public void SingleFileTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "TriangleSingleFile.hlsl");

        var result = Compiler.CompileFromFile(shaderSourceFile, null, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.True(result.Success);

        var shaderCode = blob.AsSpan();

        Assert.True(shaderCode.Length > 0);
    }

    [TestCase]
    public void ErrorTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "TriangleError.hlsl");

        var result = Compiler.CompileFromFile(shaderSourceFile, null, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.True(result.Failure);

        var errorText = error?.AsString();

        Assert.True(errorText!.Contains("error X3018: invalid subscript 'ThisIsAnError'"));
    }

    [TestCase]
    public void ShaderIncludeTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "Triangle.hlsl");

        using (var includeHandler = new ShaderIncludeHandler(AssetsPath))
        {
            var result = Compiler.CompileFromFile(shaderSourceFile, null, includeHandler, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

            Assert.True(result.Success);

            var shaderCode = blob.AsSpan();

            Assert.True(shaderCode.Length > 0);
        }
    }

    [TestCase]
    public void ShaderMacroTest()
    {
        string shaderSourceFile = Path.Combine(AssetsPath, "TriangleSingleFile.hlsl");

        var defines = new[] { new ShaderMacro { Name = "TEST", Definition = "1" } };

        var result = Compiler.CompileFromFile(shaderSourceFile, defines, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.True(result.Success);

        var shaderCode = blob.AsSpan();

        Assert.True(shaderCode.Length > 0);
    }
}
