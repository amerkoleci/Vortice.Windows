// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice;

/// <summary>
/// Provides a set of methods to supplement or replace <see cref="Unsafe" /> and <see cref="MemoryMarshal" />.
/// </summary>
public static unsafe class UnsafeUtilities
{
    public static void Read<T>(void* source, T[] values) where T : unmanaged
    {
        int count = values.Length;
        fixed (void* destination = values)
        {
            NativeMemory.Copy(source, destination, (nuint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(void* source, T[] values, int count) where T : unmanaged
    {
        fixed (void* destination = values)
        {
            NativeMemory.Copy(source, destination, (nuint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(void* source, T[] values, uint count) where T : unmanaged
    {
        fixed (void* destination = values)
        {
            NativeMemory.Copy(source, destination, (nuint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(nint source, T[] values) where T : unmanaged
    {
        int count = values.Length;
        fixed (void* destination = values)
        {
            NativeMemory.Copy(source.ToPointer(), destination, (nuint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(nint source, T[] values, int count) where T : unmanaged
    {
        fixed (void* dstPtr = values)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, (void*)source, (uint)(count * sizeof(T)));
        }
    }

    public static void Write<T>(nint destination, ref T value) where T : unmanaged
    {
        fixed (void* valuePtr = &value)
        {
            Unsafe.CopyBlockUnaligned((void*)destination, valuePtr, (uint)(sizeof(T)));
        }
    }

    public static void Write<T>(void* destination, ref T value) where T : unmanaged
    {
        fixed (void* valuePtr = &value)
        {
            Unsafe.CopyBlockUnaligned(destination, valuePtr, (uint)(sizeof(T)));
        }
    }

    public static nint Write<T>(nint destination, T[] data) where T : unmanaged
    {
        int byteCount = data.Length * sizeof(T);
        fixed (void* dataPtr = data)
        {
            Unsafe.CopyBlockUnaligned((void*)destination, dataPtr, (uint)byteCount);
            return destination + byteCount;
        }
    }

    public static nint Write<T>(nint destination, T[] data, int offset, int count) where T : unmanaged
    {
        int byteCount = count * sizeof(T);
        fixed (void* dataPtr = &data[offset])
        {
            Unsafe.CopyBlockUnaligned((void*)destination, dataPtr, (uint)byteCount);
            return destination + byteCount;
        }
    }

    public static void Write<T>(void* destination, Span<T> data) where T : unmanaged
    {
        int byteCount = data.Length * sizeof(T);
        fixed (void* dataPtr = data)
        {
            Unsafe.CopyBlockUnaligned(destination, dataPtr, (uint)byteCount);
        }
    }

    public static void Write<T>(void* destination, Span<T> data, int offset, int count) where T : unmanaged
    {
        int byteCount = count * sizeof(T);
        fixed (void* dataPtr = &data[offset])
        {
            Unsafe.CopyBlockUnaligned(destination, dataPtr, (uint)byteCount);
        }
    }

    public static void* Alloc(int byteCount) 
    {
        return NativeMemory.Alloc((nuint)byteCount);
    }

    public static void* Alloc(int elementCount, int elementSize)
    {
        return NativeMemory.Alloc((nuint)elementCount, (nuint)elementSize);
    }

    public static T* Alloc<T>() where T : unmanaged
    {
        return (T*)NativeMemory.Alloc((nuint)sizeof(T));
    }

    public static T* Alloc<T>(int elementCount) where T : unmanaged
    {
        return (T*)Alloc(elementCount, sizeof(T));
    }

    public static T* AllocWithData<T>(T source) where T : unmanaged
    {
        int sizeInBytes = sizeof(T);
        T* dest = (T*)Alloc(sizeInBytes);
        Unsafe.CopyBlockUnaligned(dest, &source, (uint)sizeInBytes);

        return dest;
    }

    public static T* AllocWithData<T>(T[] source) where T : unmanaged
    {
        ReadOnlySpan<T> span = source;

        return AllocWithData(span);
    }

    public static T* AllocWithData<T>(ReadOnlySpan<T> source) where T : unmanaged
    {
        int sizeInBytes = source.Length * sizeof(T);
        T* dest = (T*)Alloc(sizeInBytes);
        fixed (T* sourcePtr = source)
        {
            Unsafe.CopyBlockUnaligned(dest, sourcePtr, (uint)sizeInBytes);
        }

        return dest;
    }

    public static void Free(void* ptr)
    {
        NativeMemory.Free(ptr);
    }

    public static void Free(IntPtr ptr)
    {
        NativeMemory.Free(ptr.ToPointer());
    }

    public static T* AllocToPointer<T>(T[]? values)
        where T : unmanaged
    {
        if (values == null || values.Length == 0)
        {
            return null;
        }

        int structSize = sizeof(T);
        int totalSize = values.Length * structSize;
        T* ptr = (T*)NativeMemory.Alloc((nuint)totalSize);

        byte* walk = (byte*)ptr;
        for (int i = 0; i < values.Length; i++)
        {
            Unsafe.Copy(walk, ref values[i]);
            walk += structSize;
        }

        return ptr;
    }
}
