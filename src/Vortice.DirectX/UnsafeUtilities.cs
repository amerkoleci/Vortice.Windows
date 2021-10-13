// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.
// Copyright © Tanner Gooding and Contributors. Licensed under the MIT License (MIT). See License.md in the repository root for more information.
// https://github.com/terrafx/terrafx/blob/main/sources/Core/Utilities/UnsafeUtilities.cs

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice
{
    /// <summary>
    /// Provides a set of methods to supplement or replace <see cref="Unsafe" /> and <see cref="MemoryMarshal" />.
    /// </summary>
    public static unsafe class UnsafeUtilities
    {
        /// <inheritdoc cref="Unsafe.As{TFrom, TTo}(ref TFrom)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TFrom, TTo>(ref TFrom source) => ref Unsafe.As<TFrom, TTo>(ref source);

        /// <inheritdoc cref="Unsafe.SizeOf{T}" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SizeOf<T>() => Unsafe.SizeOf<T>();

        /// <inheritdoc cref="Unsafe.SizeOf{T}" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint USizeOf<T>() => unchecked((uint)Unsafe.SizeOf<T>());

        /// <inheritdoc cref="Unsafe.AsPointer{T}(ref T)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* AsPointer<T>(ref T source) where T : unmanaged => (T*)Unsafe.AsPointer(ref source);

        /// <inheritdoc cref="Unsafe.As{TFrom, TTo}(ref TFrom)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly TTo AsReadonly<TFrom, TTo>(in TFrom source) => ref Unsafe.As<TFrom, TTo>(ref AsRef(in source));

        /// <summary>Reinterprets the given native integer as a reference.</summary>
        /// <typeparam name="T">The type of the reference.</typeparam>
        /// <param name="source">The native integer to reinterpret.</param>
        /// <returns>A reference to a value of type <typeparamref name="T" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(nint source) => ref Unsafe.AsRef<T>((void*)source);

        /// <summary>Reinterprets the given native unsigned integer as a reference.</summary>
        /// <typeparam name="T">The type of the reference.</typeparam>
        /// <param name="source">The native unsigned integer to reinterpret.</param>
        /// <returns>A reference to a value of type <typeparamref name="T" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(nuint source) => ref Unsafe.AsRef<T>((void*)source);

        /// <inheritdoc cref="Unsafe.AsRef{T}(in T)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(in T source) => ref Unsafe.AsRef(in source);

        /// <inheritdoc cref="Unsafe.AsRef{T}(void*)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(void* source) => ref Unsafe.AsRef<T>(source);

        /// <inheritdoc cref="MemoryMarshal.Cast{TFrom, TTo}(Span{TFrom})" />
        public static Span<TTo> Cast<TFrom, TTo>(this Span<TFrom> span)
            where TFrom : struct
            where TTo : struct
        {
            return MemoryMarshal.Cast<TFrom, TTo>(span);
        }

        /// <inheritdoc cref="MemoryMarshal.Cast{TFrom, TTo}(ReadOnlySpan{TFrom})" />
        public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(this ReadOnlySpan<TFrom> span)
            where TFrom : struct
            where TTo : struct
        {
            return MemoryMarshal.Cast<TFrom, TTo>(span);
        }

        /// <summary>Returns a pointer to the element of the span at index zero.</summary>
        /// <typeparam name="T">The type of items in <paramref name="span" />.</typeparam>
        /// <param name="span">The span from which the pointer is retrieved.</param>
        /// <returns>A pointer to the item at index zero of <paramref name="span" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* GetPointer<T>(this Span<T> span) where T : unmanaged
        {
            return AsPointer(ref span.GetReference());
        }

        /// <summary>Returns a pointer to the element of the span at index zero.</summary>
        /// <typeparam name="T">The type of items in <paramref name="span" />.</typeparam>
        /// <param name="span">The span from which the pointer is retrieved.</param>
        /// <returns>A pointer to the item at index zero of <paramref name="span" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* GetPointer<T>(this ReadOnlySpan<T> span) where T : unmanaged => AsPointer(ref AsRef(in span.GetReference()));

        /// <inheritdoc cref="MemoryMarshal.GetReference{T}(Span{T})" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetReference<T>(this Span<T> span) => ref MemoryMarshal.GetReference(span);

        /// <inheritdoc cref="MemoryMarshal.GetReference{T}(ReadOnlySpan{T})" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly T GetReference<T>(this ReadOnlySpan<T> span) => ref MemoryMarshal.GetReference(span);

        /// <inheritdoc cref="Unsafe.IsNullRef{T}(ref T)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullRef<T>(in T source) => Unsafe.IsNullRef(ref AsRef(in source));

        /// <inheritdoc cref="Unsafe.NullRef{T}" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T NullRef<T>() => ref Unsafe.NullRef<T>();

        /// <inheritdoc cref="Unsafe.ReadUnaligned{T}(void*)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadUnaligned<T>(void* source)
            where T : unmanaged => Unsafe.ReadUnaligned<T>(source);

        /// <inheritdoc cref="Unsafe.ReadUnaligned{T}(void*)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadUnaligned<T>(void* source, nuint offset) where T : unmanaged
        {
            return Unsafe.ReadUnaligned<T>((void*)((nuint)source + offset));
        }

        /// <inheritdoc cref="Unsafe.WriteUnaligned{T}(void*, T)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUnaligned<T>(void* source, T value) where T : unmanaged
        {
            Unsafe.WriteUnaligned<T>(source, value);
        }

        /// <inheritdoc cref="Unsafe.WriteUnaligned{T}(void*, T)" />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUnaligned<T>(void* source, nuint offset, T value) where T : unmanaged
        {
            Unsafe.WriteUnaligned((void*)((nuint)source + offset), value);
        }

#if NET5_0_OR_GREATER
        /// <summary>Reinterprets the readonly span as a writeable span.</summary>
        /// <typeparam name="T">The type of items in <paramref name="span" /></typeparam>
        /// <param name="span">The readonly span to reinterpret.</param>
        /// <returns>A writeable span that points to the same items as <paramref name="span" />.</returns>
        public static Span<T> AsSpan<T>(this ReadOnlySpan<T> span) => MemoryMarshal.CreateSpan(ref AsRef(in span.GetReference()), span.Length);

        /// <inheritdoc cref="MemoryMarshal.CreateSpan{T}(ref T, int)" />
        public static Span<T> CreateSpan<T>(ref T reference, int length) => MemoryMarshal.CreateSpan(ref reference, length);

        /// <inheritdoc cref="MemoryMarshal.CreateReadOnlySpan{T}(ref T, int)" />
        public static ReadOnlySpan<T> CreateReadOnlySpan<T>(in T reference, int length) => MemoryMarshal.CreateReadOnlySpan(ref AsRef(in reference), length);
#endif

            public static IntPtr Read<T>(IntPtr source, ref T value) where T : unmanaged
        {
            fixed (void* valuePtr = &value)
            {
                Unsafe.CopyBlockUnaligned(valuePtr, (void*)source, (uint)(sizeof(T)));
                return source + sizeof(T);
            }
        }

        public static IntPtr Read<T>(IntPtr source, T[] values) where T : unmanaged
        {
            int count = values.Length;
            fixed (void* dstPtr = values)
            {
                Unsafe.CopyBlockUnaligned(dstPtr, (void*)source, (uint)(count * sizeof(T)));
                return source + sizeof(T) * count;
            }
        }

        public static IntPtr Read<T>(IntPtr source, T[] values, int count) where T : unmanaged
        {
            fixed (void* dstPtr = values)
            {
                Unsafe.CopyBlockUnaligned(dstPtr, (void*)source, (uint)(count * sizeof(T)));
                return source + sizeof(T) * count;
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

        public static IntPtr Alloc<T>(int count = 1) where T : unmanaged
        {
            return Marshal.AllocHGlobal(sizeof(T) * count);
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
    }
}
