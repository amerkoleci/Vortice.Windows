// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D9;

public partial class IDirect3DSurface9
{
    /// <summary>
    /// Gets the parent cube texture or texture (mipmap) object, if this surface is a child level of a cube texture or a mipmap.
    /// This method can also provide access to the parent swap chain if the surface is a back-buffer child.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="guid">The GUID.</param>
    /// <returns>The parent container texture.</returns>
    public T? GetContainer<T>(Guid guid) where T : ComObject
    {
        var containerPtr = GetContainer(guid);
        return MarshallingHelpers.FromPointer<T>(containerPtr);
    }

    /// <summary>
    /// Locks a rectangle on a surface.
    /// </summary>
    /// <param name="flags">The type of lock to perform.</param>
    /// <returns>A pointer to the locked region</returns>
    public DataRectangle LockRect(LockFlags flags)
    {
        var lockedRect = LockRect(IntPtr.Zero, flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }

    /// <summary>
    /// Locks a rectangle on a surface.
    /// </summary>
    /// <param name="rect">The rectangle to lock.</param>
    /// <param name="flags">The type of lock to perform.</param>
    /// <returns>A pointer to the locked region</returns>
    public unsafe DataRectangle LockRect(in RectangleI rect, LockFlags flags)
    {
        RawRect rawRect = rect;
        var lockedRect = LockRect(new IntPtr(&rawRect), flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }
}
