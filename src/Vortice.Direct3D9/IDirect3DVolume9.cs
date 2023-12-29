// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DVolume9
{
    public T GetContainer<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] T>(Guid guid) where T : ComObject
    {
        IntPtr containerPtr = GetContainer(guid);
        return MarshallingHelpers.FromPointer<T>(containerPtr)!;
    }

    /// <summary>
    /// Locks a box on a volume resource.
    /// </summary>
    /// <param name="flags">The lock flags.</param>
    /// <returns>The locked region of this resource.</returns>
    public LockedBox LockBox(LockFlags flags)
    {
        return LockBox((void*)null, flags);
    }

    /// <summary>
    /// Locks a box on a volume resource.
    /// </summary>
    /// <param name="box">The box.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>The locked region of this resource</returns>
    public LockedBox LockBox(Box box, LockFlags flags)
    {
        return LockBox(&box, flags);
    }
}
