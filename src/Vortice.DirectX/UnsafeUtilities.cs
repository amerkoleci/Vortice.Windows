// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice;

/// <summary>
/// Provides a set of methods to supplement or replace <see cref="Unsafe" /> and <see cref="MemoryMarshal" />.
/// </summary>
public static unsafe class UnsafeUtilities
{
    public static void Read<T>(void* source, T[] values) where T : unmanaged
    {
        int count = values.Length;
        fixed (void* dstPtr = values)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, source, (uint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(void* source, T[] values, int count) where T : unmanaged
    {
        fixed (void* dstPtr = values)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, source, (uint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(IntPtr source, T[] values) where T : unmanaged
    {
        int count = values.Length;
        fixed (void* dstPtr = values)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, (void*)source, (uint)(count * sizeof(T)));
        }
    }

    public static void Read<T>(IntPtr source, T[] values, int count) where T : unmanaged
    {
        fixed (void* dstPtr = values)
        {
            Unsafe.CopyBlockUnaligned(dstPtr, (void*)source, (uint)(count * sizeof(T)));
        }
    }

    public static IntPtr Write<T>(IntPtr destination, ref T value) where T : unmanaged
    {
        fixed (void* valuePtr = &value)
        {
            Unsafe.CopyBlockUnaligned((void*)destination, valuePtr, (uint)(sizeof(T)));
            return destination + sizeof(T);
        }
    }

    public static IntPtr Write<T>(IntPtr destination, T[] data) where T : unmanaged
    {
        int byteCount = data.Length * sizeof(T);
        fixed (void* dataPtr = data)
        {
            Unsafe.CopyBlockUnaligned((void*)destination, dataPtr, (uint)byteCount);
            return destination + byteCount;
        }
    }

    public static IntPtr Write<T>(IntPtr destination, T[] data, int offset, int count) where T : unmanaged
    {
        int byteCount = count * sizeof(T);
        fixed (void* dataPtr = &data[offset])
        {
            Unsafe.CopyBlockUnaligned((void*)destination, dataPtr, (uint)byteCount);
            return destination + byteCount;
        }
    }

    public static T* Alloc<T>(int count = 1) where T : unmanaged
    {
        return (T*)Marshal.AllocHGlobal(sizeof(T) * count);
    }

    public static T* AllocWithData<T>(T source) where T : unmanaged
    {
        int sizeInBytes = sizeof(T);
        T* dest = (T*)Marshal.AllocHGlobal(sizeInBytes);
        Unsafe.CopyBlockUnaligned(dest, &source, (uint)sizeInBytes);

        return dest;
    }

    public static T* AllocWithData<T>(T[] source) where T : unmanaged
    {
        int sizeInBytes = source.Length * sizeof(T);
        T* dest = (T*)Marshal.AllocHGlobal(sizeInBytes);
        fixed (T* sourcePtr = source)
        {
            Unsafe.CopyBlockUnaligned(dest, sourcePtr, (uint)sizeInBytes);
        }

        return dest;
    }
    
    public static void Free(void* ptr)
    {
        Marshal.FreeHGlobal((IntPtr)ptr);
    }

    public static void Free(IntPtr ptr)
    {
        Marshal.FreeHGlobal(ptr);
    }

    public static IntPtr AllocToPointer<T>(T[]? values) where T : unmanaged
    {
        if (values == null
            || values.Length == 0)
        {
            return IntPtr.Zero;
        }

        int structSize = sizeof(T);
        int totalSize = values.Length * structSize;
        IntPtr ptr = Marshal.AllocHGlobal(totalSize);

        byte* walk = (byte*)ptr;
        for (int i = 0; i < values.Length; i++)
        {
            Unsafe.Copy(walk, ref values[i]);
            walk += structSize;
        }

        return ptr;
    }

#if !NET6_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetReference<T>(Span<T> span)
    {
        return ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(span);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetReference<T>(ReadOnlySpan<T> span)
    {
        return ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(span);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T GetArrayDataReference<T>(T[] array)
    {
        return ref global::System.Runtime.InteropServices.MemoryMarshal.GetReference(array.AsSpan());
    }

    /// <summary>
    /// Creates a new <see cref="Span{T}"/> from a given reference.
    /// </summary>
    /// <typeparam name="T">The type of reference to wrap.</typeparam>
    /// <param name="value">The target reference.</param>
    /// <param name="length">The length of the <see cref="Span{T}"/> to create.</param>
    /// <returns>A new <see cref="Span{T}"/> wrapping <paramref name="value"/>.</returns>
    public static unsafe Span<T> CreateSpan<T>(ref T value, int length)
    {
        return new(Unsafe.AsPointer(ref value), length);
    }
#endif
}
