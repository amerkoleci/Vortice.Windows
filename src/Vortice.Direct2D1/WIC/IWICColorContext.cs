// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.WIC;

public unsafe partial class IWICColorContext
{
    /// <summary>
    /// Gets the color context profile.
    /// </summary>
    public DataStream? Profile
    {
        get
        {
            GetProfileBytes(0, null, out int actualSize);
            if (actualSize == 0)
                return null;
            var buffer = new DataStream(actualSize, true, true);
            GetProfileBytes(actualSize, buffer.BaseUnsafePointer, out _);
            return buffer;
        }
    }

    public Result InitializeFromMemory(byte[] data, int dataLength = 0)
    {
        if (dataLength == 0)
            dataLength = data.Length;

        fixed (void* dataPtr = data)
        {
            return InitializeFromMemory(dataPtr, dataLength);
        }
    }

    public Result InitializeFromMemory(ReadOnlySpan<byte> data, int dataLength = 0)
    {
        if (dataLength == 0)
            dataLength = data.Length;

        fixed (void* dataPtr = data)
        {
            return InitializeFromMemory(dataPtr, dataLength);
        }
    }

    public Result InitializeFromMemory<T>(T[] data) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            return InitializeFromMemory(dataPtr, data.Length * sizeof(T));
        }
    }

    public Result InitializeFromMemory<T>(ReadOnlySpan<T> data) where T : unmanaged
    {
        fixed (void* dataPtr = data)
        {
            return InitializeFromMemory(dataPtr, data.Length * sizeof(T));
        }
    }

    public Result GetProfileBytes(byte[] buffer, out int actual)
    {
        fixed (byte* bufferPtr = buffer)
        {
            return GetProfileBytes(buffer.Length, bufferPtr, out actual);
        }
    }
}
