// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DIndexBuffer9
{
    public Span<T> Lock<T>(uint offsetToLock, uint sizeToLock, LockFlags lockFlags = LockFlags.None) where T : unmanaged
    {
        if (sizeToLock == 0)
            sizeToLock = Description.SizeInBytes;

        var pOut = Lock(offsetToLock, sizeToLock, lockFlags);
        return new Span<T>(pOut.ToPointer(), (int)sizeToLock);
    }

    public ReadOnlySpan<T> LockAsReadOnly<T>(uint offsetToLock, uint sizeToLock, LockFlags lockFlags = LockFlags.ReadOnly) where T : unmanaged
    {
        if (sizeToLock == 0)
            sizeToLock = Description.SizeInBytes;

        nint pOut = Lock(offsetToLock, sizeToLock, lockFlags);
        return new ReadOnlySpan<T>(pOut.ToPointer(), (int)sizeToLock);
    }

    /// <summary>
    /// Locks the specified index buffer.
    /// </summary>
    /// <param name="offsetToLock">The offset in the buffer.</param>
    /// <param name="sizeToLock">The size of the buffer to lock.</param>
    /// <param name="lockFlags">The lock flags.</param>
    /// <returns>A <see cref="System.IntPtr" /> containing the locked index buffer.</returns>
    public IntPtr LockToPointer(uint offsetToLock, uint sizeToLock, LockFlags lockFlags = LockFlags.None)
    {
        if (sizeToLock == 0)
            sizeToLock = Description.SizeInBytes;

        return Lock(offsetToLock, sizeToLock, lockFlags);
    }
}
