// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using SharpDXGI;

namespace SharpD2D1.WIC
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
            Guard.MustBeGreaterThan(size, 0, "Size must be greather than 0");
            Guard.MustBeGreaterThan(stride, 0, "Stride must be greather than 0");

            CopyPixels(IntPtr.Zero, stride, size, data);
        }

        public unsafe void CopyPixels(Rectangle rectangle, int stride, int size, IntPtr data)
        {
            Guard.MustBeGreaterThan(size, 0, "Size must be greather than 0");
            Guard.MustBeGreaterThan(stride, 0, "Stride must be greather than 0");

            CopyPixels(new IntPtr(&rectangle), stride, size, data);
        }

        public unsafe void CopyPixels(int stride, byte[] data)
        {
            Guard.MustBeGreaterThan(stride, 0, "Stride must be greather than 0");
            Guard.NotNullOrEmpty(data, nameof(data));

            CopyPixels(IntPtr.Zero, stride, data.Length, (IntPtr)Unsafe.AsPointer(ref data[0]));
        }

        public unsafe void CopyPixels(Rectangle rectangle, int stride, byte[] data)
        {
            Guard.MustBeGreaterThan(stride, 0, "Stride must be greather than 0");
            Guard.NotNullOrEmpty(data, nameof(data));

            CopyPixels(new IntPtr(&rectangle), stride, data.Length, (IntPtr)Unsafe.AsPointer(ref data[0]));
        }

        public unsafe void CopyPixels<T>(int stride, T[] data) where T : struct
        {
            Guard.MustBeGreaterThan(stride, 0, "Stride must be greather than 0");
            Guard.NotNullOrEmpty(data, nameof(data));

            CopyPixels(IntPtr.Zero, stride, data.Length, (IntPtr)Unsafe.AsPointer(ref data[0]));
        }

        public unsafe void CopyPixels<T>(Rectangle rectangle, int stride, T[] data) where T : struct
        {
            Guard.MustBeGreaterThan(stride, 0, "Stride must be greather than 0");
            Guard.NotNullOrEmpty(data, nameof(data));

            CopyPixels(new IntPtr(&rectangle), stride, data.Length, (IntPtr)Unsafe.AsPointer(ref data[0]));
        }
    }
}
