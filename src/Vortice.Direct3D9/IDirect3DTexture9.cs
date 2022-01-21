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
        AddDirtyRect(IntPtr.Zero);
    }

    /// <summary>
    /// Adds a dirty region to a texture resource.
    /// </summary>
    /// <param name="dirtyRect">The dirty rectangle.</param>
    public void AddDirtyRect(in RectangleI dirtyRect)
    {
        RawRect rawRect = dirtyRect;
        AddDirtyRect(new IntPtr(&rawRect));
    }

    /// <summary>
    /// Locks a rectangle on a texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    public DataRectangle LockRect(int level, LockFlags flags)
    {
        var lockedRect = LockRect(level, IntPtr.Zero, flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }

    /// <summary>
    /// Locks a rectangle on a texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="rectangle">The rectangle.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    public DataRectangle LockRect(int level, in RectangleI rectangle, LockFlags flags)
    {
        RawRect rawRect = rectangle;
        var lockedRect = LockRect(level, new IntPtr(&rawRect), flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }
}
