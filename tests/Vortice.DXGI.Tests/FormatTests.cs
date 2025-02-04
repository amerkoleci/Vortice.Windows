// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.DXGI.Tests;

[TestFixture(TestOf = typeof(Format))]
public class FormatTests
{
    [TestCase]
    public void BitsPerPixelTest()
    {
        Assert.That(FormatHelper.GetBitsPerPixel(Format.R1_UNorm), Is.EqualTo(1));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.R8_Typeless), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.R8_UNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.R8_UInt), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.R8_SNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.R8_SInt), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.A8_UNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC2_Typeless), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC2_UNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC2_UNorm_SRgb), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC3_Typeless), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC3_UNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC3_UNorm_SRgb), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC5_Typeless), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC5_UNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC5_SNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC6H_Typeless), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC6H_Uf16), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC6H_Sf16), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC7_Typeless), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC7_UNorm), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.BC7_UNorm_SRgb), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.AI44), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.IA44), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(Format.P8), Is.EqualTo(8));
        Assert.That(FormatHelper.GetBitsPerPixel(FormatHelper.Xbox_R4G4_UNorm), Is.EqualTo(8));
    }

    [TestCase]
    public void GetSurfaceInfoTest()
    {
        FormatHelper.GetSurfaceInfo(Format.R8G8B8A8_UNorm, 256, 256, out uint rowPitch, out uint sliceCount, out uint rowCount);
        Assert.That(256u * 4, Is.EqualTo(rowPitch));
        Assert.That(256u * 4 * rowCount, Is.EqualTo(sliceCount));
        Assert.That(256u, Is.EqualTo(rowCount));
    }
}
