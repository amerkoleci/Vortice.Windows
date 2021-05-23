// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DTexture9
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
        public void AddDirtyRect(in Rectangle dirtyRect)
        {
            unsafe
            {
                RawRect rawRect = dirtyRect;
                AddDirtyRect(new IntPtr(&rawRect));
            }
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
            return new DataRectangle(lockedRect.BitsPointer, lockedRect.Pitch);
        }

        /// <summary>
        /// Locks a rectangle on a texture resource.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
        public DataRectangle LockRect(int level, in Rectangle rectangle, LockFlags flags)
        {
            unsafe
            {
                RawRect rawRect = rectangle;
                var lockedRect = LockRect(level, new IntPtr(&rawRect), flags);
                return new DataRectangle(lockedRect.BitsPointer, lockedRect.Pitch);
            }
        }
    }
}
