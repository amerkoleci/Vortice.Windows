// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI.Debug;

public unsafe partial class IDXGIInfoQueue
{
    public InfoQueueMessage GetMessage(Guid producer, ulong messageIndex)
    {
        PointerUSize messageSize = 0;
        GetMessage(producer, messageIndex, IntPtr.Zero, ref messageSize);

        if (messageSize == 0)
        {
            return new InfoQueueMessage();
        }

        byte* messagePtr = stackalloc byte[(int)((uint)messageSize)];
        GetMessage(producer, messageIndex, new IntPtr(messagePtr), ref messageSize);

        var message = new InfoQueueMessage();
        message.__MarshalFrom(ref *(InfoQueueMessage.__Native*)messagePtr);
        return message;
    }

    public InfoQueueFilter? GetStorageFilter(Guid producer)
    {
        PointerUSize sizeFilter = PointerUSize.Zero;
        GetStorageFilter(producer, IntPtr.Zero, ref sizeFilter);

        if (sizeFilter == 0)
        {
            return null;
        }

        byte* filter = stackalloc byte[(int)((uint)sizeFilter)];
        GetStorageFilter(producer, (IntPtr)filter, ref sizeFilter);

        var queueNative = new InfoQueueFilter();
        queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);

        return queueNative;
    }

    public InfoQueueFilter? GetRetrievalFilter(Guid producer)
    {
        PointerUSize sizeFilter = PointerUSize.Zero;
        GetRetrievalFilter(producer, IntPtr.Zero, ref sizeFilter);

        if (sizeFilter == 0)
        {
            return null;
        }

        byte* filter = stackalloc byte[(int)((uint)sizeFilter)];
        GetRetrievalFilter(producer, (IntPtr)filter, ref sizeFilter);

        var queueNative = new InfoQueueFilter();
        queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);

        return queueNative;
    }
}
