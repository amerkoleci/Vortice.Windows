// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public unsafe partial class IDirect3DCubeTexture9
{
    /// <summary>
    /// Adds a dirty region to a cube texture resource.
    /// </summary>
    /// <param name="faceType">Type of the face.</param>
    public void AddDirtyRect(CubeMapFace faceType)
    {
        AddDirtyRect(faceType, null);
    }

    /// <summary>
    /// Adds a dirty region to a cube texture resource.
    /// </summary>
    /// <param name="faceType">Type of the face.</param>
    /// <param name="dirtyRect">The dirty rectangle.</param>
    public void AddDirtyRect(CubeMapFace faceType, Rect dirtyRect)
    {
        AddDirtyRect(faceType, &dirtyRect);
    }

    /// <summary>
    /// Locks a rectangle on a cube texture resource.
    /// </summary>
    /// <param name="faceType">Type of the face.</param>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="LockedRectangle"/> describing the region locked.</returns>
    /// <unmanaged>HREULT IDirect3DCubeTexture9::LockRect([In] D3DCUBEMAP_FACES FaceType,[In] unsigned int Level,[In] D3DLOCKED_RECT* pLockedRect,[In] const void* pRect,[In] D3DLOCK Flags)</unmanaged>
    public LockedRectangle LockRect(CubeMapFace faceType, int level, LockFlags flags)
    {
        return LockRect(faceType, level, null, flags);
    }

    /// <summary>
    /// Locks a rectangle on a cube texture resource.
    /// </summary>
    /// <param name="faceType">Type of the face.</param>
    /// <param name="level">The level.</param>
    /// <param name="rectangle">The rectangle.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="LockedRectangle"/> describing the region locked.</returns>
    public LockedRectangle LockRect(CubeMapFace faceType, int level, Rect rectangle, LockFlags flags)
    {
        return LockRect(faceType, level, &rectangle, flags);
    }
}
