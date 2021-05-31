// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Numerics;

namespace Vortice.Direct2D1
{
    public partial class ID2D1Bitmap
    {
        public SizeF Dpi
        {
            get
            {
                GetDpi(out var dpiX, out var dpiY);
                return new SizeF(dpiX, dpiY);
            }
        }

        public void CopyFromBitmap(ID2D1Bitmap sourceBitmap)
        {
            CopyFromBitmap(null, sourceBitmap, null);
        }

        public void CopyFromBitmap(Point destinationPoint, ID2D1Bitmap sourceBitmap)
        {
            CopyFromBitmap(destinationPoint, sourceBitmap, null);
        }

        public void CopyFromBitmap(Point destinationPoint, ID2D1Bitmap sourceBitmap, Rectangle sourceArea)
        {
            RawRect rawSourceArea = sourceArea;
            CopyFromBitmap(destinationPoint, sourceBitmap, rawSourceArea);
        }

        public void CopyFromMemory(IntPtr pointer, int pitch)
        {
            CopyFromMemory(null, pointer, pitch);
        }

        public unsafe void CopyFromMemory(byte[] data, int pitch)
        {
            fixed (void* dataPtr = &data[0])
            {
                CopyFromMemory(null, new IntPtr(dataPtr), pitch);
            }
        }

        public unsafe void CopyFromMemory<T>(T[] data, int pitch) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                CopyFromMemory(null, (IntPtr)dataPtr, pitch);
            }
        }

        public unsafe void CopyFromMemory<T>(Span<T> data, int pitch) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                CopyFromMemory(null, new IntPtr(dataPtr), pitch);
            }
        }

        public unsafe void CopyFromMemory(Rectangle destinationArea, byte[] data, int pitch)
        {
            fixed (void* dataPtr = &data[0])
            {
                RawRect rawDestinationArea = destinationArea;
                CopyFromMemory(rawDestinationArea, new IntPtr(dataPtr), pitch);
            }
        }

        public unsafe void CopyFromMemory<T>(Rectangle destinationArea, T[] data, int pitch) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                RawRect rawDestinationArea = destinationArea;
                CopyFromMemory(destinationArea, (IntPtr)dataPtr, pitch);
            }
        }

        public unsafe void CopyFromMemory<T>(Rectangle destinationArea, Span<T> data, int pitch) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                RawRect rawDestinationArea = destinationArea;
                CopyFromMemory(rawDestinationArea, new IntPtr(dataPtr), pitch);
            }
        }

        public void CopyFromRenderTarget(ID2D1RenderTarget renderTarget)
        {
            CopyFromRenderTarget(null, renderTarget, null);
        }

        public void CopyFromRenderTarget(Point destinationPoint, ID2D1RenderTarget renderTarget)
        {
            CopyFromRenderTarget(destinationPoint, renderTarget, null);
        }

        public void CopyFromRenderTarget(Point destinationPoint, ID2D1RenderTarget renderTarget, Rectangle sourceArea)
        {
            RawRect rawSourceArea = sourceArea;
            CopyFromRenderTarget(destinationPoint, renderTarget, rawSourceArea);
        }
    }
}
