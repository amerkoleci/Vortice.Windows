// Copyright © Amer Koleci and Contributors.
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
        AddDirtyBox(IntPtr.Zero);
    }

    /// <summary>
    /// Adds a dirty region to a texture resource.
    /// </summary>
    /// <param name="box">The dirty box.</param>
    public void AddDirtyBox(Box box)
    {
        AddDirtyBox(new IntPtr(&box));
    }

    /// <summary>
    /// Locks a box on a volume texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataBox"/> describing the region locked.</returns>
    public DataBox LockBox(int level, LockFlags flags)
    {
        var lockedRect = LockBox(level, IntPtr.Zero, flags);
        return new DataBox(lockedRect.Bits, lockedRect.RowPitch, lockedRect.SlicePitch);
    }

    /// <summary>
    /// Locks a box on a volume texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="box">The box.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    public DataBox LockBox(int level, Box box, LockFlags flags)
    {
        var lockedRect = LockBox(level, new IntPtr(&box), flags);
        return new DataBox(lockedRect.Bits, lockedRect.RowPitch, lockedRect.SlicePitch);
    }
}
