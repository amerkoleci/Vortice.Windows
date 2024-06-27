// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Gdi;

namespace Vortice.DirectWrite;

public unsafe partial class IDWriteGdiInterop
{
    public IDWriteFont CreateFontFromLOGFONT(LogFont logFont)
    {
        int sizeOfLogFont = Marshal.SizeOf(logFont);
        byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
        Marshal.StructureToPtr(logFont, new IntPtr(nativeLogFont), false);
        return CreateFontFromLOGFONT(new IntPtr(nativeLogFont));
    }

    public bool ConvertFontToLOGFONT(IDWriteFont font, out LogFont logFont)
    {
        logFont = new LogFont();
        ConvertFontToLOGFONT(font, out var nativeLogFont, out var isSystemFont);
        Marshal.PtrToStructure(nativeLogFont, logFont);
        return isSystemFont;
    }

    public void ConvertFontFaceToLOGFONT(IDWriteFontFace font, out LogFont logFont)
    {
        logFont = new LogFont();
        ConvertFontFaceToLOGFONT(font, out IntPtr nativeLogFont);
        Marshal.PtrToStructure(nativeLogFont, logFont);
    }
}
