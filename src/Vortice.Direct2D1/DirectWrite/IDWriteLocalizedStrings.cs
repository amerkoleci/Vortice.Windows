// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.DirectWrite
{
    public partial class IDWriteLocalizedStrings
    {
        public unsafe string GetString(int index)
        {
            var length = GetStringLength(index);
            char* chars = stackalloc char[length + 1];
            GetString(index, new IntPtr(chars), length + 1);
            return new string(chars, 0, length);
        }

        public unsafe string GetLocaleName(int index)
        {
            var length = GetLocaleNameLength(index);
            char* chars = stackalloc char[length + 1];
            GetLocaleName(index, new IntPtr(chars), length + 1);
            return new string(chars, 0, length);
        }
    }
}
