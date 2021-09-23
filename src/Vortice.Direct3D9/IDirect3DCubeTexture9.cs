// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DCubeTexture9
    {
        /// <summary>
        /// Adds a dirty region to a cube texture resource.
        /// </summary>
        /// <param name="faceType">Type of the face.</param>
        public void AddDirtyRect(CubeMapFace faceType)
        {
            AddDirtyRect(faceType, IntPtr.Zero);
        }

        /// <summary>
        /// Adds a dirty region to a cube texture resource.
        /// </summary>
        /// <param name="faceType">Type of the face.</param>
        /// <param name="dirtyRect">The dirty rectangle.</param>
        public void AddDirtyRect(CubeMapFace faceType, in Rectangle dirtyRect)
        {
            unsafe
            {
                RawRect rawRect = dirtyRect;
                AddDirtyRect(faceType, new IntPtr(&rawRect));
            }
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
            var lockedRect = LockRect(faceType, level, IntPtr.Zero, flags);
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
        public DataRectangle LockRect(CubeMapFace faceType, int level, in Rectangle rectangle, LockFlags flags)
        {
            unsafe
            {
                RawRect rawRect = rectangle;
                var lockedRect = LockRect(faceType, level, new IntPtr(&rawRect), flags);
                return new DataRectangle(lockedRect.Bits, lockedRect.Pitch);
            }
        }
    }
}
