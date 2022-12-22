// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Drawing;
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
    public void AddDirtyRect(Rectangle dirtyRect)
    {
        AddDirtyRect(&dirtyRect);
    }

    /// <summary>
    /// Locks a rectangle on a texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="LockedRectangle"/> describing the region locked.</returns>
    public LockedRectangle LockRect(int level, LockFlags flags)
    {
        return LockRect(level, null, flags);
    }

    /// <summary>
    /// Locks a rectangle on a texture resource.
    /// </summary>
    /// <param name="level">The level.</param>
    /// <param name="rectangle">The rectangle.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="LockedRectangle"/> describing the region locked.</returns>
    public LockedRectangle LockRect(int level, Rectangle rectangle, LockFlags flags)
    {
        return LockRect(level, &rectangle, flags);
    }
}
