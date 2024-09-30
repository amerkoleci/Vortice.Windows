// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DVolumeTexture9
{
    /// <summary>
    /// Adds a dirty region to a texture resource.
    /// </summary>
    public void AddDirtyBox()
    {
        AddDirtyBox((void*)null);
    }

    /// <summary>
    /// Adds a dirty region to a texture resource.
    /// </summary>
    /// <param name="box">The dirty box.</param>
    public void AddDirtyBox(Box box)
    {
        AddDirtyBox(&box);
    }

    /// <summary>
    /// Locks a box on a volume texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="LockedBox"/> describing the region locked.</returns>
    public LockedBox LockBox(uint level, LockFlags flags)
    {
        return LockBox(level, (void*)null, flags);
    }

    /// <summary>
    /// Locks a box on a volume texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="box">The box.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="LockedBox"/> describing the region locked.</returns>
    public LockedBox LockBox(uint level, Box box, LockFlags flags)
    {
        return LockBox(level, &box, flags);
    }
}
