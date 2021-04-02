// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DVolumeTexture9
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
            unsafe
            {
                AddDirtyBox(new IntPtr(&box));
            }
        }

        /// <summary>
        /// Locks a box on a volume texture resource.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>A <see cref="NativeMessage"/> describing the region locked.</returns>
        public NativeMessage LockBox(int level, LockFlags flags)
        {
            LockBox(level, out LockedBox lockedRect, IntPtr.Zero, flags);
            return new DataBox(lockedRect.PBits, lockedRect.RowPitch, lockedRect.SlicePitch);
        }

        /// <summary>
        /// Locks a box on a volume texture resource.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="box">The box.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>A <see cref="DataRectangle"/> describing the region locked.</returns>
        public NativeMessage LockBox(int level, Box box, LockFlags flags)
        {
            unsafe
            {
                LockBox(level, out LockedBox lockedRect, new IntPtr(&box), flags);
                return new DataBox(lockedRect.PBits, lockedRect.RowPitch, lockedRect.SlicePitch);
            }
        }
    }
}
