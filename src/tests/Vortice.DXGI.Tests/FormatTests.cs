// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using NUnit.Framework;

namespace Vortice.DXGI.Tests;

[TestFixture(TestOf = typeof(Format))]
public class FormatTests
{
    [TestCase]
    public void BitsPerPixelTest()
    {
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.R1_UNorm), 1);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.R8_Typeless), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.R8_UNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.R8_UInt), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.R8_SNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.R8_SInt), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.A8_UNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC2_Typeless), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC2_UNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC2_UNorm_SRgb), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC3_Typeless), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC3_UNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC3_UNorm_SRgb), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC5_Typeless), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC5_UNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC5_SNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC6H_Typeless), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC6H_Uf16), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC6H_Sf16), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC7_Typeless), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC7_UNorm), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.BC7_UNorm_SRgb), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.AI44), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.IA44), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(Format.P8), 8);
        Assert.AreEqual(FormatHelper.GetBitsPerPixel(FormatHelper.Xbox_R4G4_UNorm), 8);
    }

    [TestCase]
    public void GetSurfaceInfoTest()
    {
        FormatHelper.GetSurfaceInfo(Format.R8G8B8A8_UNorm, 256, 256, out int rowPitch, out int sliceCount, out int rowCount);
        Assert.AreEqual(256 * 4, rowPitch);
        Assert.AreEqual(256 * 4 * rowCount, sliceCount);
        Assert.AreEqual(256, rowCount);
    }
}
