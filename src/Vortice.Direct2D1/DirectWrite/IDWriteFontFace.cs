// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.DirectWrite;

public partial class IDWriteFontFace
{
    public int FilesCount
    {
        get
        {
            int numberOfFiles = 0;
            GetFiles(ref numberOfFiles, null);
            return numberOfFiles;
        }
    }

    public IDWriteFontFile[] GetFiles()
    {
        int numberOfFiles = FilesCount;
        IDWriteFontFile[] files = new IDWriteFontFile[numberOfFiles];
        GetFiles(ref numberOfFiles, files).CheckError();
        return files;
    }

    public Result GetFiles(IDWriteFontFile[] files)
    {
        int numberOfFiles = files.Length;
        return GetFiles(ref numberOfFiles, files);
    }

    public GlyphMetrics[] GetDesignGlyphMetrics(ushort[] glyphIndices, bool isSideways)
    {
        var glyphMetrics = new GlyphMetrics[glyphIndices.Length];
        GetDesignGlyphMetrics(glyphIndices, glyphMetrics, isSideways);
        return glyphMetrics;
    }

    public Result GetDesignGlyphMetrics(ushort[] glyphIndices, GlyphMetrics[] glyphMetrics, bool isSideways)
    {
        return GetDesignGlyphMetrics(glyphIndices, glyphMetrics, (RawBool)isSideways);
    }

    public GlyphMetrics[] GetGdiCompatibleGlyphMetrics(float fontSize, float pixelsPerDip, Matrix3x2? transform, bool useGdiNatural, ushort[] glyphIndices, bool isSideways)
    {
        var glyphMetrics = new GlyphMetrics[glyphIndices.Length];
        GetGdiCompatibleGlyphMetrics(fontSize, pixelsPerDip, transform, useGdiNatural, glyphIndices, glyphMetrics, isSideways);
        return glyphMetrics;
    }

    public ushort[] GetGlyphIndices(int[] codePoints)
    {
        ushort[] glyphIndices = new ushort[codePoints.Length];
        GetGlyphIndicesW(codePoints, glyphIndices);
        return glyphIndices;
    }

    public Result GetGlyphIndices(int[] codePoints, ushort[] glyphIndices)
    {
        return GetGlyphIndicesW(codePoints, glyphIndices);
    }

    public unsafe bool TryGetFontTable(int openTypeTableTag, out Span<byte> tableData, out IntPtr tableContext)
    {
        void* tableDataPtr = null;
        Result result = TryGetFontTable(openTypeTableTag, &tableDataPtr, out int tableDataSize, out tableContext, out RawBool exists);

        if (result.Failure)
        {
            tableData = default;
            return false;
        }

        tableData = new Span<byte>(tableDataPtr, tableDataSize);
        return exists;
    }
}
