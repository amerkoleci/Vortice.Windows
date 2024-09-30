// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.XAudio2;

public unsafe partial class AudioBuffer : IDisposable
{
    private readonly bool _ownsBuffer;

    private AudioBuffer(BufferFlags flags, void* data, uint sizeInBytes, bool ownsBuffer)
    {
        Flags = flags;
        AudioBytes = sizeInBytes;
        AudioDataPointer = new IntPtr(data);
        _ownsBuffer = ownsBuffer;
    }

    /// <summary>
    /// Create new instance of <see cref="AudioBuffer"/> with externally owned memory.
    /// </summary>
    /// <param name="data">The data pointer.</param>
    /// <param name="sizeInBytes">Size in bytes of the buffer.</param>
    /// <param name="flags">The <see cref="BufferFlags"/> flags.</param>
    public AudioBuffer(IntPtr data, uint sizeInBytes, BufferFlags flags = BufferFlags.None)
    {
        Flags = flags;
        AudioBytes = sizeInBytes;
        AudioDataPointer = data;
        _ownsBuffer = false;
    }

    public AudioBuffer(uint sizeInBytes, BufferFlags flags = BufferFlags.EndOfStream)
    {
        Flags = flags;
        AudioBytes = sizeInBytes;
        AudioDataPointer = (IntPtr)MemoryHelpers.AllocateMemory((nuint)sizeInBytes);
        _ownsBuffer = true;
    }

    public AudioBuffer(byte[] data, BufferFlags flags = BufferFlags.EndOfStream)
    {
        Flags = flags;
        AudioBytes = (uint)data.Length;
        AudioDataPointer = (IntPtr)MemoryHelpers.AllocateMemory((nuint)data.Length);
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
    /// <param name="flags"></param>
    public AudioBuffer(DataStream stream, BufferFlags flags = BufferFlags.EndOfStream)
    {
        int length = (int)stream.Length - (int)stream.Position;

        AudioDataPointer = new(MemoryHelpers.AllocateMemory((nuint)length));
        MemoryHelpers.CopyMemory(AudioDataPointer, stream.PositionPointer, length);

        Flags = flags;
        AudioBytes = (uint)stream.Length;
        _ownsBuffer = true;
    }

    public static AudioBuffer Create<T>(T[] data, BufferFlags flags = BufferFlags.EndOfStream) where T : unmanaged
    {
        ReadOnlySpan<T> span = data.AsSpan();
        return Create(span, flags);
    }

    public static AudioBuffer Create<T>(ReadOnlySpan<T> data, BufferFlags flags = BufferFlags.EndOfStream) where T : unmanaged
    {
        uint sizeInBytes = (uint)(data.Length * sizeof(T));
        void* dataPtr = MemoryHelpers.AllocateMemory(sizeInBytes);
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

    public Span<byte> AsSpan() => new(AudioDataPointer.ToPointer(), (int)AudioBytes);

    public Span<T> AsSpan<T>() where T : unmanaged
    {
        return new(AudioDataPointer.ToPointer(), (int)(AudioBytes / sizeof(T)));
    }
}
