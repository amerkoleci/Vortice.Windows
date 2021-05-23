// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D9
{
    public partial class IDirect3DVolume9
    {
        protected override void NativePointerUpdated(IntPtr oldNativePointer)
        {
            ReleaseDevice();
            base.NativePointerUpdated(oldNativePointer);
        }

        protected override void DisposeCore(IntPtr nativePointer, bool disposing)
        {
            if (disposing)
                ReleaseDevice();

            base.DisposeCore(nativePointer, disposing);
        }

        private void ReleaseDevice()
        {
            if (Device__ != null)
            {
                // Don't use Dispose() in order to avoid circular references with DeviceContext
                ((IUnknown)Device__).Release();
                Device__ = null;
            }
        }

        public T? GetContainer<T>(Guid guid) where T : ComObject
        {
            var containerPtr = GetContainer(guid);
            return MarshallingHelpers.FromPointer<T>(containerPtr);
        }

        /// <summary>
        /// Locks a box on a volume resource.
        /// </summary>
        /// <param name="flags">The lock flags.</param>
        /// <returns>The locked region of this resource.</returns>
        public DataBox LockBox(LockFlags flags)
        {
            var lockedBox = LockBox(IntPtr.Zero, flags);
            return new DataBox(lockedBox.BitsPointer, lockedBox.RowPitch, lockedBox.SlicePitch);
        }

        /// <summary>
        /// Locks a box on a volume resource.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>The locked region of this resource</returns>
        public unsafe DataBox LockBox(Box box, LockFlags flags)
        {
            var lockedBox = LockBox(new IntPtr(&box), flags);
            return new DataBox(lockedBox.BitsPointer, lockedBox.RowPitch, lockedBox.SlicePitch);
        }
    }
}
