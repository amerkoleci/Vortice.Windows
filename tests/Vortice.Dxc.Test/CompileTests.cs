// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.Dxc.Test;

[TestFixture(TestOf = typeof(IDxcUtils))]
public class IDxcUtilsTests
{

    [TestCase]
    public void BuildArgumentsTest()
    {
        using IDxcUtils utils = Vortice.Dxc.Dxc.CreateDxcUtils();
        using IDxcCompilerArgs args = utils.BuildArguments("test.hlsl", "test1", "lib_6_3", ["test2"], []);

        Assert.That(args.Count, Is.EqualTo(6));
        Assert.That(args.Arguments[0], Is.EqualTo("test.hlsl"));
        Assert.That(args.Arguments[1], Is.EqualTo("-E"));
        Assert.That(args.Arguments[2], Is.EqualTo("test1"));
        Assert.That(args.Arguments[3], Is.EqualTo("-T"));
        Assert.That(args.Arguments[4], Is.EqualTo("lib_6_3"));
        Assert.That(args.Arguments[5], Is.EqualTo("test2"));
    }
}
