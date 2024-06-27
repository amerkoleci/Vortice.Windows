// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Dxc;

public unsafe readonly ref struct Utf16PinnedStringArray 
{
    private readonly char** _handle;

    public nint Handle => (nint)_handle;

    public readonly int Length;

    public Utf16PinnedStringArray(string[] strings, int length = 0)
    {
        Length = length == 0 ? strings.Length : length;

        _handle = (char**)NativeMemory.Alloc((nuint)(Length * sizeof(char*)));

        for (int i = 0; i < Length; i++)
        {
            char[] chars = strings[i].ToCharArray();

            uint size = (uint)(chars.Length + 1) * sizeof(char);
            _handle[i] = (char*)NativeMemory.Alloc(size);

            if (!string.IsNullOrEmpty(strings[i]))
            {
                fixed (char* pChars = chars)
                    Unsafe.CopyBlock(_handle[i], pChars, size);
            }
        }
    }

    public void Release()
    {
        for (int i = 0; i < Length; i++)
            NativeMemory.Free(_handle[i]);

        NativeMemory.Free(_handle);
    }

    public static implicit operator char**(Utf16PinnedStringArray pStringArray) => pStringArray._handle;
}
