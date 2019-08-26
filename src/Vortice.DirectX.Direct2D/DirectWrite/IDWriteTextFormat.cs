// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using Vortice.DirectX.Direct2D;

namespace Vortice.DirectX.DirectWrite
{
    public partial class IDWriteTextFormat
    {
        /// <summary>	
        /// Gets a copy of the font family name. 	
        /// </summary>	
        public string FontFamilyName
        {
            get
            {
                unsafe
                {
                    int fontFamilyNameLength = GetFontFamilyNameLength();
                    char* fontFamilyName = stackalloc char[fontFamilyNameLength + 1];
                    GetFontFamilyName(new IntPtr(fontFamilyName), fontFamilyNameLength + 1);
                    return new string(fontFamilyName, 0, fontFamilyNameLength);
                }
            }
        }

        /// <summary>	
        /// Gets a copy of the locale name. 	
        /// </summary>	
        public string LocaleName
        {
            get
            {
                unsafe
                {
                    int localNameLength = GetLocaleNameLength();
                    char* localName = stackalloc char[localNameLength + 1];
                    GetLocaleName(new IntPtr(localName), localNameLength + 1);
                    return new string(localName, 0, localNameLength);
                }
            }
        }
    }
}
