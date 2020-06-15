// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using Vortice.Mathematics;

namespace Vortice.WIC
{
    public partial class IWICBitmapLock
    {
        public Size Size
        {
            get
            {
                GetSize(out var width, out var height);
                return new Size(width, height);
            }
        }

        /// <summary>
        /// Gets a <see cref="DataRectangle"/> to the data.
        /// </summary>
        public DataRectangle Data
        {
            get
            {
                var pointer = GetDataPointer(out var size);
                return new DataRectangle(pointer, Stride);
            }
        }

        public unsafe Span<byte> GetDataPointer() 
        {
            var pointer = GetDataPointer(out var size);
            return new Span<byte>(pointer.ToPointer(), size);
        }

        public unsafe Span<T> GetDataPointer<T>() where T : unmanaged
        {
            var pointer = GetDataPointer(out var size);
            return new Span<T>(pointer.ToPointer(), size / sizeof(T));
        }
    }
}
