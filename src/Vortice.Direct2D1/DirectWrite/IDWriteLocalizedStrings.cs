// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectWrite;

public unsafe partial class IDWriteLocalizedStrings
{
    public string GetString(uint index)
    {
        uint length = GetStringLength(index);
        char* chars = stackalloc char[(int)length + 1];
        GetString(index, new IntPtr(chars), length + 1);
        return new string(chars, 0, (int)length);
    }

    public string GetLocaleName(uint index)
    {
        uint length = GetLocaleNameLength(index);
        char* chars = stackalloc char[(int)length + 1];
        GetLocaleName(index, new IntPtr(chars), length + 1);
        return new string(chars, 0, (int)length);
    }
}
