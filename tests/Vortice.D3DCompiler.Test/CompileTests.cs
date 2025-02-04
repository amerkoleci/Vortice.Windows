// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;
using Vortice.Direct3D;

namespace Vortice.D3DCompiler.Test;

[TestFixture(TestOf = typeof(Compiler))]
public class CompileTests
{
    private readonly string _assetsPath = Path.Combine(AppContext.BaseDirectory, "Assets");

    [TestCase]
    public void SingleFileTest()
    {
        string shaderSourceFile = Path.Combine(_assetsPath, "TriangleSingleFile.hlsl");

        var result = Compiler.CompileFromFile(shaderSourceFile, null, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.That(result.Success, Is.True);

        var shaderCode = blob.AsSpan();

        Assert.That(shaderCode.Length > 0, Is.True);
    }

    [TestCase]
    public void ErrorTest()
    {
        string shaderSourceFile = Path.Combine(_assetsPath, "TriangleError.hlsl");

        var result = Compiler.CompileFromFile(shaderSourceFile, null, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.That(result.Failure, Is.True);

        var errorText = error?.AsString();

        Assert.That(errorText!.Contains("error X3018: invalid subscript 'ThisIsAnError'"), Is.True);
    }

    [TestCase]
    public void ShaderIncludeTest()
    {
        string shaderSourceFile = Path.Combine(_assetsPath, "Triangle.hlsl");

        using (var includeHandler = new ShaderIncludeHandler(_assetsPath))
        {
            var result = Compiler.CompileFromFile(shaderSourceFile, null, includeHandler, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

            Assert.That(result.Success, Is.True);

            var shaderCode = blob.AsSpan();

            Assert.That(shaderCode.Length > 0, Is.True);
        }
    }

    [TestCase]
    public void ShaderMacroTest()
    {
        string shaderSourceFile = Path.Combine(_assetsPath, "TriangleSingleFile.hlsl");

        var defines = new[] { new ShaderMacro { Name = "TEST", Definition = "1" } };

        var result = Compiler.CompileFromFile(shaderSourceFile, defines, null, "VSMain", "vs_4_0", ShaderFlags.None, EffectFlags.None, out var blob, out var error);

        Assert.That(result.Success, Is.True);

        var shaderCode = blob.AsSpan();

        Assert.That(shaderCode.Length > 0, Is.True);
    }
}
