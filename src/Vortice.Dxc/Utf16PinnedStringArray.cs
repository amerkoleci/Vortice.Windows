// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Dxc;

public unsafe readonly ref struct Utf16PinnedStringArray 
{
    private readonly void** _handle;

    public nint Handle => (nint)_handle;

    public readonly int Length;

    public Utf16PinnedStringArray(string[] strings, int length = 0)
    {
        Length = length == 0 ? strings.Length : length;

        uint charSize = sizeof(char);

        _handle = (void**)NativeMemory.Alloc((nuint)(Length * sizeof(void*)));

        for (int i = 0; i < Length; i++)
        {
            char[] chars = strings[i].ToCharArray();

            uint size = (uint)(chars.Length + 1) * charSize;
            _handle[i] = NativeMemory.Alloc(size);

            fixed (char* pChars = chars)
                Unsafe.CopyBlock(_handle[i], pChars, size);
        }
    }

    public void Release()
    {
        for (int i = 0; i < Length; i++)
            NativeMemory.Free(_handle[i]);

        NativeMemory.Free(_handle);
    }

    public static implicit operator char**(Utf16PinnedStringArray pStringArray)
        => (char**)pStringArray.Handle;
}
