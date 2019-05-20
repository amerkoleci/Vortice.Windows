// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Vortice.DirectX
{
    public static unsafe class Interop
    {
        public static int SizeOf<T>() => Unsafe.SizeOf<T>();

        public static void Read<T>(IntPtr srcPointer, ref T value)
        {
            Unsafe.Copy(ref value, srcPointer.ToPointer());
        }

        public static void Read<T>(IntPtr srcPointer, T[] values)
        {
            int stride = SizeOf<T>();
            long size = stride * values.Length;
            void* dstPtr = Unsafe.AsPointer(ref values[0]);
            Buffer.MemoryCopy(srcPointer.ToPointer(), dstPtr, size, size);
        }

        public static void Write<T>(IntPtr dstPointer, ref T value)
        {
            Unsafe.Copy(dstPointer.ToPointer(), ref value);
        }

        public static void Write<T>(IntPtr dstPointer, T[] values)
        {
            if (values == null || values.Length == 0)
                return;

            int stride = SizeOf<T>();
            uint size = (uint)(stride * values.Length);
            void* srcPtr = Unsafe.AsPointer(ref values[0]);
            Unsafe.CopyBlock(dstPointer.ToPointer(), srcPtr, size);
        }

        public static IntPtr Alloc(int byteCount)
        {
            if (byteCount == 0)
            {
                return IntPtr.Zero;
            }

            return Marshal.AllocHGlobal(byteCount);
        }

        public static IntPtr Alloc<T>(int count = 1) => Alloc(Unsafe.SizeOf<T>() * count);

        public static IntPtr AllocToPointer<T>(T[] values) where T : struct
        {
            if (values == null
                || values.Length == 0)
            {
                return IntPtr.Zero;
            }

            int structSize = SizeOf<T>();
            int totalSize = values.Length * structSize;
            var ptr = Marshal.AllocHGlobal(totalSize);

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
