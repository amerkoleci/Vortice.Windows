// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using Vortice.Gdi;

namespace Vortice.DirectWrite
{
    public partial class IDWriteGdiInterop1
    {
        public unsafe IDWriteFont CreateFontFromLOGFONT(LogFont logFont, IDWriteFontCollection fontCollection)
        {
            int sizeOfLogFont = Marshal.SizeOf(logFont);
            byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
            Marshal.StructureToPtr(logFont, new IntPtr(nativeLogFont), false);
            return CreateFontFromLOGFONT(new IntPtr(nativeLogFont), fontCollection);
        }

        public unsafe IDWriteFontSet GetMatchingFontsByLOGFONT(LogFont logFont, IDWriteFontSet fontSet)
        {
            int sizeOfLogFont = Marshal.SizeOf(logFont);
            byte* nativeLogFont = stackalloc byte[sizeOfLogFont];
            GetMatchingFontsByLOGFONT(new IntPtr(nativeLogFont), fontSet, out IDWriteFontSet filteredSet);
            return filteredSet;
        }
    }
}
