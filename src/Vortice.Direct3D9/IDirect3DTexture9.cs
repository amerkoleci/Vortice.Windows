// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DTexture9
{
    /// <summary>
    /// Adds a dirty region to a texture resource.
    /// </summary>
    public void AddDirtyRect()
    {
        AddDirtyRect((void*)null);
    }

    /// <summary>
    /// Adds a dirty region to a texture resource.
    /// </summary>
    /// <param name="dirtyRect">The dirty rectangle.</param>
    public void AddDirtyRect(in RectI dirtyRect)
    {
        RawRect rawRect = dirtyRect;
        AddDirtyRect(&rawRect);
    }

    /// <summary>
    /// Locks a rectangle on a texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    public DataRectangle LockRect(int level, LockFlags flags)
    {
        LockedRectangle lockedRect = LockRect(level, null, flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }

    /// <summary>
    /// Locks a rectangle on a texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="rectangle">The rectangle.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    public DataRectangle LockRect(int level, in RectI rectangle, LockFlags flags)
    {
        RawRect rawRect = rectangle;
        LockedRectangle lockedRect = LockRect(level, &rawRect, flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }
}
