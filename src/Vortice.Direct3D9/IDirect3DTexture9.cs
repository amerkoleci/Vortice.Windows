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
        public void AddDirtyRect(Rect dirtyRect)
        {
            unsafe
            {
                AddDirtyRect(new IntPtr(&dirtyRect));
            }
        }

        /// <summary>
        /// Adds a dirty region to a texture resource.
        /// </summary>
        /// <param name="dirtyRect">The dirty rectangle.</param>
        public void AddDirtyRect(ref Rect dirtyRect)
        {
            unsafe
            {
                AddDirtyRect((IntPtr)Unsafe.AsPointer(ref dirtyRect));
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
            LockRect(level, out LockedRectangle lockedRect, IntPtr.Zero, flags);
            return new DataRectangle(lockedRect.PBits, lockedRect.Pitch);
        }

        /// <summary>
        /// Locks a rectangle on a texture resource.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
        public DataRectangle LockRect(int level, Rect rectangle, LockFlags flags)
        {
            unsafe
            {
                LockRect(level, out LockedRectangle lockedRect, new IntPtr(&rectangle), flags);
                return new DataRectangle(lockedRect.PBits, lockedRect.Pitch);
            }
        }
    }
}
