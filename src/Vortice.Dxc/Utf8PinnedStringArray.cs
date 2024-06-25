// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Text;
using SharpGen.Runtime.Win32;

namespace Vortice.Dxc;

public unsafe readonly ref struct Utf8PinnedStringArray
{
    private readonly byte** _handle;

    public nint Handle => (nint)_handle;

    public readonly int Length;

    public Utf8PinnedStringArray(string[] strings, int length = 0)
    {
        Length = length == 0 ? strings.Length : length;
        _handle = (byte**)NativeMemory.Alloc((nuint)(Length * sizeof(byte*)));

        for (int i = 0; i < Length; i++)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strings[i]);
            if (bytes.Length == 0)
                continue;

            uint size = (uint)(bytes.Length + 1) * sizeof(byte);
            _handle[i] = (byte*)NativeMemory.Alloc(size);
            fixed (byte* pBytes = bytes)
                Unsafe.CopyBlock(_handle[i], pBytes, size);
        }
    }

    public void Release()
    {
        for (int i = 0; i < Length; i++)
            NativeMemory.Free(_handle[i]);

        NativeMemory.Free(_handle);
    }

    public static implicit operator byte**(Utf8PinnedStringArray pStringArray)
        => pStringArray._handle;
}
