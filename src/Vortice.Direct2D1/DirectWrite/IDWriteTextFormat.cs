// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public unsafe partial class IDWriteTextFormat
{
    /// <summary>
    /// Gets a copy of the font family name.
    /// </summary>
    public string FontFamilyName
    {
        get
        {
            int fontFamilyNameLength = GetFontFamilyNameLength();
            char* fontFamilyName = stackalloc char[fontFamilyNameLength + 1];
            GetFontFamilyName(new IntPtr(fontFamilyName), fontFamilyNameLength + 1);
            return new string(fontFamilyName, 0, fontFamilyNameLength);
        }
    }

    /// <summary>
    /// Gets a copy of the locale name.
    /// </summary>
    public string LocaleName
    {
        get
        {
            int localNameLength = GetLocaleNameLength();
            char* localName = stackalloc char[localNameLength + 1];
            GetLocaleName(new IntPtr(localName), localNameLength + 1);
            return new string(localName, 0, localNameLength);
        }
    }
}
