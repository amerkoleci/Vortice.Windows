// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public unsafe partial class IDWriteLocalizedStrings
{
    public string GetString(int index)
    {
        var length = GetStringLength(index);
        char* chars = stackalloc char[length + 1];
        GetString(index, new IntPtr(chars), length + 1);
        return new string(chars, 0, length);
    }

    public string GetLocaleName(int index)
    {
        var length = GetLocaleNameLength(index);
        char* chars = stackalloc char[length + 1];
        GetLocaleName(index, new IntPtr(chars), length + 1);
        return new string(chars, 0, length);
    }
}
