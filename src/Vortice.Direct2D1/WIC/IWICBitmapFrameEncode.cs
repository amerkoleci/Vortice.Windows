// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.Mathematics;

namespace Vortice.WIC
{
    public partial class IWICBitmapFrameEncode
    {
        /// <summary>
        /// Sets the <see cref="IWICColorContext"/> objects for this frame encoder.
        /// </summary>
        /// <param name="colorContexts">The color contexts to set for the encoder.</param>
        public void SetColorContexts(IWICColorContext[] colorContexts)
        {
            SetColorContexts(colorContexts != null ? colorContexts.Length : 0, colorContexts);
        }

        /// <summary>
        /// Requests that the encoder use the specified pixel format.
        /// </summary>
        /// <param name="pixelFormat">
        /// On input, the requested pixel format GUID. On output, the closest pixel format GUID supported by the encoder; this may be different than the requested format.
        /// For a list of pixel format GUIDs, see <see cref="PixelFormat"/> class.
        /// </param>
        public void SetPixelFormat(Guid pixelFormat)
        {
            SetPixelFormat(ref pixelFormat);
        }

        /// <summary>
        /// Sets the output image dimensions for the frame.
        /// </summary>
        /// <param name="size">The width and height of the output image.</param>
        public void SetSize(Size size)
        {
            SetSize(size.Width, size.Height);
        }

        public void WritePixels(int lineCount, DataRectangle buffer, int totalSizeInBytes = 0)
        {
            WritePixels(lineCount, buffer.DataPointer, buffer.Pitch, totalSizeInBytes);
        }

        public void WritePixels(int lineCount, IntPtr buffer, int rowStride, int totalSizeInBytes = 0)
        {
            if (totalSizeInBytes == 0)
            {
                totalSizeInBytes = lineCount * rowStride;
            }

            WritePixels(lineCount, rowStride, totalSizeInBytes, buffer);
        }

        public unsafe void WritePixels<T>(int lineCount, int stride, Span<T> data) where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                WritePixels(lineCount, stride, data.Length * sizeof(T), (IntPtr)dataPtr);
            }
        }

        public unsafe void WritePixels<T>(int lineCount, int stride, T[] pixelBuffer) where T : unmanaged
        {
            if ((lineCount * stride) > (sizeof(T) * pixelBuffer.Length))
            {
                throw new ArgumentException("lineCount * stride must be <= to sizeof(pixelBuffer)");
            }

            fixed (void* pixelBufferPtr = &pixelBuffer[0])
            {
                WritePixels(lineCount, stride, lineCount * stride, (IntPtr)pixelBufferPtr);
            }
        }

        /// <summary>
        /// Encodes a bitmap source.
        /// </summary>
        /// <param name="bitmapSource">The bitmap source to encode.</param>
        public void WriteSource(IWICBitmapSource bitmapSource)
        {
            WriteSource(bitmapSource, IntPtr.Zero);
        }

        /// <summary>
        /// Encodes a bitmap source.
        /// </summary>
        /// <param name="bitmapSource">The bitmap source to encode.</param>
        /// <param name="rectangle">The size rectangle of the bitmap source.</param>
        public unsafe void WriteSource(IWICBitmapSource bitmapSource, Rectangle rectangle)
        {
            WriteSource(bitmapSource, new IntPtr(&rectangle));
        }
    }
}
