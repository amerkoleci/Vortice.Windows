// Copyright (c) 2010-2014 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.IO;
using SharpGen.Runtime;

namespace Vortice.Multimedia
{
    /// <summary>
    /// A chunk of a Riff stream.
    /// </summary>
    public class RiffChunk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RiffChunk"/> class.
        /// </summary>
        /// <param name="stream">The stream holding this chunk</param>
        /// <param name="type">The type.</param>
        /// <param name="size">The size.</param>
        /// <param name="dataPosition">The data offset.</param>
        /// <param name="isList">if set to <c>true</c> [is list].</param>
        /// <param name="isHeader">if set to <c>true</c> [is header].</param>
        public RiffChunk(Stream stream, FourCC type, uint size, uint dataPosition, bool isList = false, bool isHeader = false)
        {
            Stream = stream;
            Type = type;
            Size = size;
            DataPosition = dataPosition;
            IsList = isList;
            IsHeader = isHeader;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public Stream Stream { get; }

        /// <summary>
        /// Gets the <see cref="FourCC"/> of this chunk.
        /// </summary>
        public FourCC Type { get; }

        /// <summary>
        /// Gets the size of the data embedded by this chunk.
        /// </summary>
        public uint Size { get; }

        /// <summary>
        /// Gets the position of the data embedded by this chunk relative to the stream.
        /// </summary>
        public uint DataPosition { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a list chunk.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is list; otherwise, <c>false</c>.
        /// </value>
        public bool IsList { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a header chunk.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is a header; otherwise, <c>false</c>.
        /// </value>
        public bool IsHeader { get; }

        /// <summary>
        /// Gets the raw data contained in this chunk.
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            byte[] data = new byte[Size];
            Stream.Position = DataPosition;
            Stream.Read(data, 0, (int)Size);
            return data;
        }

        /// <summary>
        /// Gets structured data contained in this chunk.
        /// </summary>
        /// <typeparam name="T">The type of the data to return</typeparam>
        /// <returns>
        /// A structure filled with the chunk data
        /// </returns>
        public unsafe T GetDataAs<T>() where T : unmanaged
        {
            T value = new();
            byte[] data = GetData();
            fixed (void* ptr = data)
            {
                MemoryHelpers.Read((IntPtr)ptr, ref value);
            }
            return value;
        }

        /// <summary>
        /// Gets structured data contained in this chunk.
        /// </summary>
        /// <typeparam name="T">The type of the data to return</typeparam>
        /// <returns>A structure filled with the chunk data</returns>
        public unsafe T[] GetDataAsArray<T>() where T : unmanaged
        {
            int sizeOfT = sizeof(T);
            if ((Size % sizeOfT) != 0)
                throw new ArgumentException("Size of T is incompatible with size of chunk");

            T[] values = new T[Size / sizeOfT];
            byte[] data = GetData();
            fixed (void* ptr = data)
            {
                MemoryHelpers.Read((IntPtr)ptr, values, 0, values.Length);
            }

            return values;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Type: {Type}, Size: {Size}, Position: {DataPosition}, IsList: {IsList}, IsHeader: {IsHeader}";
        }
    }
}
