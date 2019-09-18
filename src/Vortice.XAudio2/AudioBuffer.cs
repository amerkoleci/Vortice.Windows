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

        public AudioBuffer(int sizeInBytes, BufferFlags flags = BufferFlags.EndOfStream)
        {
            Flags = flags;
            AudioBytes = sizeInBytes;
            AudioDataPointer = MemoryHelpers.AllocateMemory(sizeInBytes);
            _ownsBuffer = true;
        }

        public AudioBuffer(byte[] data, BufferFlags flags = BufferFlags.EndOfStream)
        {
            Flags = flags;
            AudioBytes = data.Length;
            AudioDataPointer = MemoryHelpers.AllocateMemory(data.Length);
            unsafe
            {
                Unsafe.CopyBlockUnaligned(
                    AudioDataPointer.ToPointer(),
                    Unsafe.AsPointer(ref data[0]),
                    (uint)data.Length);
            }

            _ownsBuffer = true;
        }

        public static unsafe AudioBuffer Create<T>(ReadOnlySpan<T> data, BufferFlags flags = BufferFlags.EndOfStream) where T : struct
        {
            var sizeInBytes = data.Length * Unsafe.SizeOf<T>();
            var dataPtr = MemoryHelpers.AllocateMemory(data.Length);
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
