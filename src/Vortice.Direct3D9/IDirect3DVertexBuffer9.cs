// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DVertexBuffer9
    {
        public unsafe Span<T> Lock<T>(int offsetToLock, int sizeToLock, LockFlags lockFlags = LockFlags.None) where T : unmanaged
        {
            if (sizeToLock == 0)
                sizeToLock = Description.SizeInBytes;

            Lock(offsetToLock, sizeToLock, out var pOut, lockFlags);
            return new Span<T>(pOut.ToPointer(), sizeToLock);
        }

        public unsafe ReadOnlySpan<T> LockAsReadOnly<T>(int offsetToLock, int sizeToLock, LockFlags lockFlags = LockFlags.ReadOnly) where T : unmanaged
        {
            if (sizeToLock == 0)
                sizeToLock = Description.SizeInBytes;

            Lock(offsetToLock, sizeToLock, out var pOut, lockFlags);
            return new ReadOnlySpan<T>(pOut.ToPointer(), sizeToLock);
        }

        /// <summary>
        /// Locks the specified index buffer.
        /// </summary>
        /// <param name="offsetToLock">The offset in the buffer.</param>
        /// <param name="sizeToLock">The size of the buffer to lock.</param>
        /// <param name="lockFlags">The lock flags.</param>
        /// <returns>A <see cref="System.IntPtr" /> containing the locked index buffer.</returns>
        public IntPtr LockToPointer(int offsetToLock, int sizeToLock, LockFlags lockFlags = LockFlags.None)
        {
            if (sizeToLock == 0)
            {
                sizeToLock = Description.SizeInBytes;
            }

            Lock(offsetToLock, sizeToLock, out var pOut, lockFlags);
            return pOut;
        }
    }
}
