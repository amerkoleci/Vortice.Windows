// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;

namespace Vortice.DirectWrite;

public unsafe partial class IDWriteFontFace
{
    public uint FilesCount
    {
        get
        {
            uint numberOfFiles = 0;
            GetFiles(ref numberOfFiles, null);
            return numberOfFiles;
        }
    }

    public IDWriteFontFile[] GetFiles()
    {
        uint numberOfFiles = FilesCount;
        IDWriteFontFile[] files = new IDWriteFontFile[numberOfFiles];
        GetFiles(ref numberOfFiles, files).CheckError();
        return files;
    }

    public Result GetFiles(IDWriteFontFile[] files)
    {
        uint numberOfFiles = (uint)files.Length;
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

    public ushort[] GetGlyphIndices(uint[] codePoints)
    {
        ushort[] glyphIndices = new ushort[codePoints.Length];
        fixed (uint* codePointsPtr = codePoints)
        fixed (ushort* glyphIndicesPtr = glyphIndices)
            GetGlyphIndicesW(codePointsPtr, (uint)codePoints.Length, glyphIndicesPtr);
        return glyphIndices;
    }

    public Result GetGlyphIndices(uint[] codePoints, ushort[] glyphIndices)
    {
        fixed (uint* codePointsPtr = codePoints)
        fixed (ushort* glyphIndicesPtr = glyphIndices)
            return GetGlyphIndicesW(codePointsPtr, (uint)codePoints.Length, glyphIndicesPtr);
    }

    public Result GetGlyphIndices(ReadOnlySpan<uint> codePoints, Span<ushort> glyphIndices)
    {
        fixed (uint* codePointsPtr = codePoints)
        fixed (ushort* glyphIndicesPtr = glyphIndices)
            return GetGlyphIndicesW(codePointsPtr, (uint)codePoints.Length, glyphIndicesPtr);
    }

    public Result GetGlyphIndices(uint* codePoints, uint codePointCount, ushort* glyphIndices)
    {
        return GetGlyphIndicesW(codePoints, codePointCount, glyphIndices);
    }

    public unsafe bool TryGetFontTable(uint openTypeTableTag, out Span<byte> tableData, out IntPtr tableContext)
    {
        void* tableDataPtr = null;
        Result result = TryGetFontTable(openTypeTableTag, &tableDataPtr, out uint tableDataSize, out tableContext, out RawBool exists);

        if (result.Failure)
        {
            tableData = default;
            return false;
        }

        tableData = new Span<byte>(tableDataPtr, (int)tableDataSize);
        return exists;
    }
}
