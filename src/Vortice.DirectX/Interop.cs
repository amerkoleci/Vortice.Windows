// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;

namespace Vortice
{
    public static unsafe class Interop
    {
        public static IntPtr Read<T>(IntPtr source, ref T value) where T : unmanaged
        {
            fixed (void* valuePtr = &value)
            {
                Unsafe.CopyBlockUnaligned(valuePtr, (void*)source, (uint)(sizeof(T)));
                return source + sizeof(T);
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

        public static IntPtr Write<T>(IntPtr destination, ref T value) where T : unmanaged
        {
            unsafe
            {
                fixed (void* valuePtr = &value)
                {
                    Unsafe.CopyBlockUnaligned((void*)destination, valuePtr, (uint)(sizeof(T)));
                    return destination + sizeof(T);
                }
            }
        }

        public static void Write<T>(IntPtr destination, T[] values) where T : unmanaged
        {
            MemoryHelpers.Write(destination, new Span<T>(values), values.Length);
        }

        public static IntPtr Write<T>(IntPtr destination, T[] values, int offset, int count) where T : unmanaged
        {
            return MemoryHelpers.Write(destination, new Span<T>(values).Slice(offset), count);
        }

        public static IntPtr Alloc<T>(int count = 1) where T : unmanaged
        {
            return Marshal.AllocHGlobal(sizeof(T) * count);
        }

        public static IntPtr AllocToPointer<T>(T[] values) where T : unmanaged
        {
            if (values == null
                || values.Length == 0)
            {
                return IntPtr.Zero;
            }

            int structSize = sizeof(T);
            int totalSize = values.Length * structSize;
            IntPtr ptr = Marshal.AllocHGlobal(totalSize);

            var walk = (byte*)ptr;
            for (int i = 0; i < values.Length; i++)
            {
                Unsafe.Copy(walk, ref values[i]);
                walk += structSize;
            }

            return ptr;
        }
    }
}
