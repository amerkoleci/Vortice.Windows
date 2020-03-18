// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using SharpGen.Runtime;
using Vortice.Mathematics;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DSurface9
    {
        /// <summary>
        /// Gets the parent cube texture or texture (mipmap) object, if this surface is a child level of a cube texture or a mipmap. 
        /// This method can also provide access to the parent swap chain if the surface is a back-buffer child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guid">The GUID.</param>
        /// <returns>The parent container texture.</returns>
        public T GetContainer<T>(Guid guid) where T : ComObject
        {
            GetContainer(guid, out var containerPtr);
            return FromPointer<T>(containerPtr);
        }

        /// <summary>
        /// Locks a rectangle on a surface.
        /// </summary>
        /// <param name="flags">The type of lock to perform.</param>
        /// <returns>A pointer to the locked region</returns>
        public DataRectangle LockRect(LockFlags flags)
        {
            LockRect(out var lockedRect, IntPtr.Zero, flags);
            return new DataRectangle(lockedRect.PBits, lockedRect.Pitch);
        }

        /// <summary>
        /// Locks a rectangle on a surface.
        /// </summary>
        /// <param name="rect">The rectangle to lock.</param>
        /// <param name="flags">The type of lock to perform.</param>
        /// <returns>A pointer to the locked region</returns>
        public DataRectangle LockRect(in Rectangle rect, LockFlags flags)
        {
            unsafe
            {
                RawRect rawRect = rect;
                LockRect(out var lockedRect, new IntPtr(&rawRect), flags);
                return new DataRectangle(lockedRect.PBits, lockedRect.Pitch);
            }
        }
    }
}
