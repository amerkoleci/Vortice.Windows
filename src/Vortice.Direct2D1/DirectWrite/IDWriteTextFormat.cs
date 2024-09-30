// Copyright (c) Amer Koleci and Contributors.
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
            uint fontFamilyNameLength = GetFontFamilyNameLength();
            char* fontFamilyName = stackalloc char[(int)fontFamilyNameLength + 1];
            GetFontFamilyName(new IntPtr(fontFamilyName), fontFamilyNameLength + 1);
            return new string(fontFamilyName, 0, (int)fontFamilyNameLength);
        }
    }

    /// <summary>
    /// Gets a copy of the locale name.
    /// </summary>
    public string LocaleName
    {
        get
        {
            uint localNameLength = GetLocaleNameLength();
            char* localName = stackalloc char[(int)localNameLength + 1];
            GetLocaleName(new IntPtr(localName), localNameLength + 1);
            return new string(localName, 0, (int)localNameLength);
        }
    }
}
