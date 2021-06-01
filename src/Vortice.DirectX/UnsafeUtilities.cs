// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

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
