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
    public void AddDirtyRect(CubeMapFace faceType, in RectI dirtyRect)
    {
        RawRect rawRect = dirtyRect;
        AddDirtyRect(faceType, &rawRect);
    }

    /// <summary>
    /// Locks a rectangle on a cube texture resource.
    /// </summary>
    /// <param name="faceType">Type of the face.</param>
    /// <param name="level">The level.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    /// <unmanaged>HREULT IDirect3DCubeTexture9::LockRect([In] D3DCUBEMAP_FACES FaceType,[In] unsigned int Level,[In] D3DLOCKED_RECT* pLockedRect,[In] const void* pRect,[In] D3DLOCK Flags)</unmanaged>
    public DataRectangle LockRect(CubeMapFace faceType, int level, LockFlags flags)
    {
        LockedRectangle lockedRect = LockRect(faceType, level, null, flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }

    /// <summary>
    /// Locks a rectangle on a cube texture resource.
    /// </summary>
    /// <param name="faceType">Type of the face.</param>
    /// <param name="level">The level.</param>
    /// <param name="rectangle">The rectangle.</param>
    /// <param name="flags">The flags.</param>
    /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
    public DataRectangle LockRect(CubeMapFace faceType, int level, in RectI rectangle, LockFlags flags)
    {
        RawRect rawRect = rectangle;
        LockedRectangle lockedRect = LockRect(faceType, level, &rawRect, flags);
        return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
    }
}
