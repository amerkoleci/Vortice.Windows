// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace SharpDirect2D
{
    /// <summary>
    /// This structure allows a <see cref="ID2D1Bitmap1"/> to be created with bitmap options and color context information available.
    /// </summary>
    public partial struct BitmapProperties1
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapProperties1"/> struct.
        /// </summary>
        /// <param name="pixelFormat">The pixel format.</param>
        public BitmapProperties1(PixelFormat pixelFormat)
            : this(pixelFormat, 96.0f, 96.0f, BitmapOptions.None, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapProperties1"/> struct.
        /// </summary>
        /// <param name="pixelFormat">The pixel format.</param>
        /// <param name="dpiX">The bitmap dpi in the x direction.</param>
        /// <param name="dpiY">The bitmap dpi in the y direction.</param>
        public BitmapProperties1(PixelFormat pixelFormat, float dpiX, float dpiY)
            : this(pixelFormat, dpiX, dpiY, BitmapOptions.None, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapProperties1"/> struct.
        /// </summary>
        /// <param name="pixelFormat">The pixel format.</param>
        /// <param name="dpiX">The bitmap dpi in the x direction.</param>
        /// <param name="dpiY">The bitmap dpi in the y direction.</param>
        /// <param name="bitmapOptions">The special creation options of the bitmap.</param>
        public BitmapProperties1(PixelFormat pixelFormat, float dpiX, float dpiY, BitmapOptions bitmapOptions)
            : this(pixelFormat, dpiX, dpiY, bitmapOptions, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapProperties1"/> struct.
        /// </summary>
        /// <param name="pixelFormat">The pixel format.</param>
        /// <param name="dpiX">The bitmap dpi in the x direction.</param>
        /// <param name="dpiY">The bitmap dpi in the y direction.</param>
        /// <param name="bitmapOptions">The special creation options of the bitmap.</param>
        /// <param name="colorContext">The optionally specified color context information.</param>
        public BitmapProperties1(PixelFormat pixelFormat, float dpiX, float dpiY, BitmapOptions bitmapOptions, ID2D1ColorContext colorContext)
        {
            PixelFormat = pixelFormat;
            DpiX = dpiX;
            DpiY = dpiY;
            BitmapOptions = bitmapOptions;
            ColorContext = colorContext;
        }
    }
}
