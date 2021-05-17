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
// -----------------------------------------------------------------------------
// Original code from SlimDX project. The difference in the implem is that 
// this class doesn't test limit, allowing slightly better performance.
// Greetings to SlimDX Group. Original code published with the following license:
// -----------------------------------------------------------------------------
/*
* Copyright (c) 2007-2011 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Direct3D;

namespace Vortice
{
    /// <summary>
    /// Provides a stream interface to a buffer located in unmanaged memory.
    /// </summary>
    public unsafe class DataStream : Stream
    {
        private byte* _buffer;
        private GCHandle _handle;
        private Blob? _blob;
        private readonly bool _ownsBuffer;
        private long _position;
        private readonly long _size;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStream"/> class from a <see cref="Blob"/> buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public DataStream(Blob buffer)
        {
            Debug.Assert(buffer.GetBufferSize() > 0);

            _buffer = (byte*)buffer.GetBufferPointer();
            _size = buffer.GetBufferSize();
            CanRead = true;
            CanWrite = true;
            _blob = buffer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStream"/> class and allocates a new buffer to use as a backing store.
        /// </summary>
        /// <param name="sizeInBytes">The size of the buffer to be allocated, in bytes.</param>
        /// <param name="canRead"><c>true</c> if reading from the buffer should be allowed; otherwise, <c>false</c>.</param>
        /// <param name = "canWrite"><c>true</c> if writing to the buffer should be allowed; otherwise, <c>false</c>.</param>
        public DataStream(int sizeInBytes, bool canRead, bool canWrite)
        {
            Debug.Assert(sizeInBytes > 0);

            _buffer = (byte*)MemoryHelpers.AllocateMemory(sizeInBytes);
            _size = sizeInBytes;
            _ownsBuffer = true;
            CanRead = canRead;
            CanWrite = canWrite;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStream"/> class using an unmanaged buffer as a backing store.
        /// </summary>
        /// <param name="userBuffer">A pointer to the buffer to be used as a backing store.</param>
        /// <param name="sizeInBytes">The size of the buffer provided, in bytes.</param>
        /// <param name="canRead"><c>true</c> if reading from the buffer should be allowed; otherwise, <c>false</c>.</param>
        /// <param name = "canWrite"><c>true</c> if writing to the buffer should be allowed; otherwise, <c>false</c>.</param>
        public DataStream(IntPtr userBuffer, long sizeInBytes, bool canRead, bool canWrite)
        {
            Debug.Assert(userBuffer != IntPtr.Zero);
            Debug.Assert(sizeInBytes > 0);

            _buffer = (byte*)userBuffer.ToPointer();
            _size = sizeInBytes;
            CanRead = canRead;
            CanWrite = canWrite;
        }

        internal DataStream(void* dataPointer, int sizeInBytes, bool canRead, bool canWrite, GCHandle handle)
        {
            Debug.Assert(sizeInBytes > 0);
            _handle = handle;
            _buffer = (byte*)dataPointer;
            _size = sizeInBytes;
            CanRead = canRead;
            CanWrite = canWrite;
            _ownsBuffer = false;
        }

        internal DataStream(void* buffer, int sizeInBytes, bool canRead, bool canWrite, bool makeCopy)
        {
            Debug.Assert(sizeInBytes > 0);

            if (makeCopy)
            {
                _buffer = (byte*)MemoryHelpers.AllocateMemory(sizeInBytes);
                Unsafe.CopyBlockUnaligned(_buffer, buffer, (uint)sizeInBytes);
            }
            else
            {
                _buffer = (byte*)buffer;
            }
            _size = sizeInBytes;
            CanRead = canRead;
            CanWrite = canWrite;
            _ownsBuffer = makeCopy;
        }

        ~DataStream()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataStream"/> class using a managed buffer as a backing store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userBuffer">A managed array to be used as a backing store.</param>
        /// <param name="canRead"><c>true</c> if reading from the buffer should be allowed; otherwise, <c>false</c>.</param>
        /// <param name="canWrite"><c>true</c> if writing to the buffer should be allowed; otherwise, <c>false</c>.</param>
        /// <param name="index">Index inside the buffer in terms of element count (not size in bytes).</param>
        /// <param name="pinBuffer">True to keep the managed buffer and pin it, false will allocate unmanaged memory and make a copy of it. Default is true.</param>
        /// <returns></returns>
        public static DataStream Create<T>(T[] userBuffer, bool canRead, bool canWrite, int index = 0, bool pinBuffer = true) where T : unmanaged
        {
            if (userBuffer == null)
                throw new ArgumentNullException(nameof(userBuffer));

            if (index < 0 || index > userBuffer.Length)
                throw new ArgumentException("Index is out of range [0, userBuffer.Length-1]", "index");

            int sizeOfBuffer = userBuffer.Length * sizeof(T);
            int indexOffset = index * sizeof(T);

            if (pinBuffer)
            {
                var handle = GCHandle.Alloc(userBuffer, GCHandleType.Pinned);
                return new DataStream(indexOffset + (byte*)handle.AddrOfPinnedObject(), sizeOfBuffer - indexOffset, canRead, canWrite, handle);
            }

            return new DataStream(indexOffset + (byte*)Unsafe.AsPointer(ref userBuffer[0]), sizeOfBuffer - indexOffset, canRead, canWrite, true);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_blob != null)
                {
                    _blob.Dispose();
                    _blob = null;
                }
            }

            if (_handle.IsAllocated)
            {
                _handle.Free();
            }

            if (_ownsBuffer && _buffer != (byte*)0)
            {
                MemoryHelpers.FreeMemory((IntPtr)_buffer);
                _buffer = (byte*)0;
            }
        }

        /// <summary>
        /// Reads a single value from the current stream and advances the current position within this stream by the number of bytes read.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <typeparam name = "T">The type of the value to be read from the stream.</typeparam>
        /// <returns>The value that was read.</returns>
        /// <exception cref="NotSupportedException">This stream does not support reading.</exception>
        public T Read<T>() where T : unmanaged
        {
            if (!CanRead)
                throw new NotSupportedException();

            T result = Unsafe.ReadUnaligned<T>(_buffer + _position);
            _position += sizeof(T);
            return result;
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public void Read(IntPtr buffer, int offset, int count)
        {
            Unsafe.CopyBlockUnaligned((byte*)buffer + offset, _buffer + _position, (uint)count);
            _position += count;
        }

        /// <summary>
        /// Reads an array of values from the current stream, and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <typeparam name="T">The type of the values to be read from the stream.</typeparam>
        /// <returns>An array of values that was read from the current stream.</returns>
        public T[] ReadRange<T>(int count) where T : unmanaged
        {
            if (!CanRead)
                throw new NotSupportedException();

            byte* from = _buffer + _position;
            T[] result = new T[count];
            _position = (byte*)MemoryHelpers.Read((IntPtr)from, result, 0, count) - _buffer;
            return result;
        }

        /// <summary>
        ///   Reads a sequence of elements from the current stream into a target buffer and
        ///   advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <param name = "buffer">An array of values to be read from the stream.</param>
        /// <param name = "offset">The zero-based byte offset in buffer at which to begin storing
        ///   the data read from the current stream.</param>
        /// <param name = "count">The number of values to be read from the current stream.</param>
        /// <returns>The number of bytes read from the stream.</returns>
        /// <exception cref="NotSupportedException">This stream does not support reading.</exception>
        public int ReadRange<T>(T[] buffer, int offset, int count) where T : unmanaged
        {
            if (!CanRead)
                throw new NotSupportedException();

            long oldPosition = _position;
            _position = (byte*)MemoryHelpers.Read((IntPtr)(_buffer + _position), buffer, offset, count) - _buffer;
            return (int)(_position - oldPosition);
        }

        /// <summary>
        /// Writes a single value to the stream, and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <typeparam name = "T">The type of the value to be written to the stream.</typeparam>
        /// <param name = "value">The value to write to the stream.</param>
        /// <exception cref = "T:System.NotSupportedException">The stream does not support writing.</exception>
        public void Write<T>(T value) where T : unmanaged
        {
            if (!CanWrite)
                throw new NotSupportedException();

            Unsafe.WriteUnaligned(_buffer + _position, value);
            _position += sizeof(T);
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public void Write(IntPtr buffer, int offset, int count)
        {
            Unsafe.CopyBlockUnaligned(_buffer + _position, (byte*)buffer + offset, (uint)count);
            _position += count;
        }

        /// <summary>
        /// Writes an array of values to the current stream, and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <param name = "data">An array of values to be written to the current stream.</param>
        /// <exception cref="NotSupportedException">This stream does not support writing.</exception>
        public void WriteRange<T>(T[] data) where T : unmanaged
        {
            WriteRange(data, 0, data.Length);
        }

        /// <summary>
        /// Writes a range of bytes to the current stream, and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <param name = "source">A pointer to the location to start copying from.</param>
        /// <param name = "count">The number of bytes to copy from source to the current stream.</param>
        /// <exception cref="NotSupportedException">This stream does not support writing.</exception>
        public void WriteRange(IntPtr source, long count)
        {
            if (!CanWrite)
                throw new NotSupportedException();

            Debug.Assert(source != IntPtr.Zero);
            Debug.Assert(count > 0);
            Debug.Assert((_position + count) <= _size);

            Unsafe.CopyBlockUnaligned(_buffer + _position, source.ToPointer(), (uint)count);
            _position += count;
        }

        /// <summary>
        /// Writes an array of values to the current stream, and advances the current position within this stream by the number of bytes written.
        /// </summary>
        /// <remarks>
        /// In order to provide faster read/write, this operation doesn't check stream bound. 
        /// A client must carefully not read/write above the size of this datastream.
        /// </remarks>
        /// <typeparam name = "T">The type of the values to be written to the stream.</typeparam>
        /// <param name = "data">An array of values to be written to the stream.</param>
        /// <param name = "offset">The zero-based offset in data at which to begin copying values to the current stream.</param>
        /// <param name = "count">
        /// The number of values to be written to the current stream. If this is zero, all of the contents <paramref name="data" /> will be written.
        /// </param>
        /// <exception cref="NotSupportedException">This stream does not support writing.</exception>
        public void WriteRange<T>(T[] data, int offset, int count) where T : unmanaged
        {
            if (!CanWrite)
                throw new NotSupportedException();

            _position = (byte*)UnsafeUtilities.Write((IntPtr)(_buffer + _position), data, offset, count) - _buffer;
        }

        /// <summary>
        /// Gets the undisturbed buffer pointer.
        /// </summary>
        /// <value>The undisturbed buffer pointer.</value>
        public IntPtr BasePointer => (IntPtr)(_buffer);

        /// <summary>
        /// Gets the position pointer.
        /// </summary>
        /// <value>The position pointer.</value>
        public IntPtr PositionPointer => (IntPtr)(_buffer + _position);

        /// <summary>
        /// Gets the length of the remaining.
        /// </summary>
        /// <value>The length of the remaining.</value>
        public long RemainingLength => (_size - _position);

        #region Stream Members Override
        /// <inheritdoc/>
        public override bool CanRead { get; }

        /// <inheritdoc/>
        public override bool CanSeek => true;

        /// <inheritdoc/>
        public override bool CanWrite { get; }

        /// <inheritdoc/>
        public override long Length => _size;

        /// <inheritdoc/>
        public override long Position
        {
            get => _position;
            set => Seek(value, SeekOrigin.Begin);
        }

        /// <inheritdoc/>
        public override void Flush() => throw new NotSupportedException($"{nameof(DataStream)} objects cannot be flushed.");

        /// <inheritdoc/>
        public override int ReadByte()
        {
            if (_position >= Length)
                return -1;

            return _buffer[_position++];
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            int minCount = (int)Math.Min(RemainingLength, count);
            return ReadRange(buffer, offset, minCount);
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            long targetPosition = 0;

            switch (origin)
            {
                case SeekOrigin.Begin:
                    targetPosition = offset;
                    break;

                case SeekOrigin.Current:
                    targetPosition = _position + offset;
                    break;

                case SeekOrigin.End:
                    targetPosition = _size - offset;
                    break;
            }

            if (targetPosition < 0)
                throw new InvalidOperationException("Cannot seek beyond the beginning of the stream.");
            if (targetPosition > _size)
                throw new InvalidOperationException("Cannot seek beyond the end of the stream.");

            _position = targetPosition;
            return _position;
        }

        /// <inheritdoc/>
        public override void SetLength(long value) => throw new NotSupportedException($"{nameof(DataStream)} objects cannot be resized.");

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            WriteRange(buffer, offset, count);
        }
        #endregion
    }
}
