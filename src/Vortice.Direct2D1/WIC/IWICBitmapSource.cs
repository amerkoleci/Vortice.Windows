// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;

namespace Vortice.WIC
{
    public partial class IWICBitmapSource
    {
        public Size Size
        {
            get
            {
                GetSize(out var width, out var height);
                return new Size(width, height);
            }
        }

        public void CopyPixels(int stride, int size, IntPtr data)
        {
            CopyPixels(IntPtr.Zero, stride, size, data);
        }

        public unsafe void CopyPixels(Rectangle rectangle, int stride, int size, IntPtr data)
        {
            CopyPixels(new IntPtr(&rectangle), stride, size, data);
        }

        public unsafe void CopyPixels(int stride, byte[] data)
        {
            fixed (void* dataPtr = &data[0])
            {
                CopyPixels(IntPtr.Zero, stride, data.Length, (IntPtr)dataPtr);
            }
        }

        public unsafe void CopyPixels(Rectangle rectangle, int stride, byte[] data)
        {
            fixed (void* dataPtr = &data[0])
            {
                CopyPixels(new IntPtr(&rectangle), stride, data.Length, (IntPtr)dataPtr);
            }
        }

        public unsafe void CopyPixels<T>(int stride, T[] data) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                CopyPixels(IntPtr.Zero, stride, data.Length, (IntPtr)dataPtr);
            }
        }

        public unsafe void CopyPixels<T>(Rectangle rectangle, int stride, T[] data) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                CopyPixels(new IntPtr(&rectangle), stride, data.Length, (IntPtr)dataPtr);
            }
        }
    }
}
