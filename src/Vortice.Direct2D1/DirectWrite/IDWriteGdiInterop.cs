// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.Gdi;

namespace Vortice.DirectWrite
{
    public partial class IDWriteGdiInterop
    {
        public unsafe IDWriteFont CreateFontFromLOGFONT(LogFont logFont)
        {
            int sizeOfLogFont = Marshal.SizeOf(logFont);
            byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
            Marshal.StructureToPtr(logFont, new IntPtr(nativeLogFont), false);
            CreateFontFromLOGFONT(new IntPtr(nativeLogFont), out var font);
            return font;
        }

        public unsafe bool ConvertFontToLOGFONT(IDWriteFont font, out LogFont logFont)
        {
            logFont = new LogFont();
            int sizeOfLogFont = Marshal.SizeOf(logFont);
            byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
            ConvertFontToLOGFONT(font, new IntPtr(nativeLogFont), out var isSystemFont);
            Marshal.PtrToStructure(new IntPtr(nativeLogFont), logFont);
            return isSystemFont;
        }
    }
}
