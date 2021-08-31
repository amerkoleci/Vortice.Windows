// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.Multimedia;

namespace Vortice.XAudio2
{
    public partial class AudioBuffer : IDisposable
    {
        private readonly bool _ownsBuffer;

        private AudioBuffer(BufferFlags flags, IntPtr data, int sizeInBytes, bool ownsBuffer)
        {
            Flags = flags;
            AudioBytes = sizeInBytes;
            AudioDataPointer = data;
            _ownsBuffer = ownsBuffer;
        }

        /// <summary>
        /// Create new instance of <see cref="AudioBuffer"/> with externally owned memory.
        /// </summary>
        /// <param name="data">The data pointer.</param>
        /// <param name="sizeInBytes">Size in bytes of the buffer.</param>
        /// <param name="flags">The <see cref="BufferFlags"/> flags.</param>
        public AudioBuffer(IntPtr data, int sizeInBytes, BufferFlags flags = BufferFlags.None)
        {
            Flags = flags;
            AudioBytes = sizeInBytes;
            AudioDataPointer = data;
            _ownsBuffer = false;
        }

        public AudioBuffer(int sizeInBytes, BufferFlags flags = BufferFlags.EndOfStream)
        {
            Flags = flags;
            AudioBytes = sizeInBytes;
            AudioDataPointer = MemoryHelpers.AllocateMemory(sizeInBytes);
            _ownsBuffer = true;
        }

        public unsafe AudioBuffer(byte[] data, BufferFlags flags = BufferFlags.EndOfStream)
        {
            Flags = flags;
            AudioBytes = data.Length;
            AudioDataPointer = MemoryHelpers.AllocateMemory(data.Length);
            fixed (void* dataPtr = &data[0])
            {
                Unsafe.CopyBlockUnaligned(AudioDataPointer.ToPointer(), dataPtr, (uint)data.Length);
            }

            _ownsBuffer = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioBuffer" /> class.
        /// </summary>
        /// <param name="stream">The stream to get the audio buffer from.</param>
        public AudioBuffer(DataStream stream)
        {
            AudioDataPointer = stream.PositionPointer;
            Flags = BufferFlags.EndOfStream;
            AudioBytes = (int)stream.Length;
        }

        public static unsafe AudioBuffer Create<T>(ReadOnlySpan<T> data, BufferFlags flags = BufferFlags.EndOfStream) where T : unmanaged
        {
            int sizeInBytes = data.Length * sizeof(T);
            IntPtr dataPtr = MemoryHelpers.AllocateMemory(data.Length);
            MemoryHelpers.CopyMemory(dataPtr, data);
            return new AudioBuffer(flags, dataPtr, sizeInBytes, true);
        }

        public void Dispose()
        {
            if (_ownsBuffer
                && AudioDataPointer != IntPtr.Zero)
            {
                MemoryHelpers.FreeMemory(AudioDataPointer);
                AudioDataPointer = IntPtr.Zero;
            }
        }
    }
}
