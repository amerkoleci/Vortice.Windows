// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11.Debug;

public unsafe partial class ID3D11InfoQueue
{
    public Message GetMessage(ulong messageIndex)
    {
        PointerUSize messageSize = 0;
        GetMessage(messageIndex, IntPtr.Zero, ref messageSize);

        if (messageSize == 0)
        {
            return new Message();
        }

        byte* messagePtr = stackalloc byte[(int)((uint)messageSize)];
        GetMessage(messageIndex, new IntPtr(messagePtr), ref messageSize);

        Message message = new Message();
        message.__MarshalFrom(ref *(Message.__Native*)messagePtr);
        return message;
    }

    public  InfoQueueFilter? GetStorageFilter()
    {
        PointerUSize sizeFilter = PointerUSize.Zero;
        GetStorageFilter(IntPtr.Zero, ref sizeFilter);

        if (sizeFilter == 0)
        {
            return default;
        }

        byte* filter = stackalloc byte[(int)((uint)sizeFilter)];
        GetStorageFilter((IntPtr)filter, ref sizeFilter);

        InfoQueueFilter queueNative = new InfoQueueFilter();
        queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);
        return queueNative;
    }

    public InfoQueueFilter? GetRetrievalFilter()
    {
        PointerUSize sizeFilter = PointerUSize.Zero;
        GetRetrievalFilter(IntPtr.Zero, ref sizeFilter);

        if (sizeFilter == 0)
        {
            return default;
        }

        byte* filter = stackalloc byte[(int)((uint)sizeFilter)];
        GetRetrievalFilter((IntPtr)filter, ref sizeFilter);

        InfoQueueFilter queueNative = new InfoQueueFilter();
        queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);
        return queueNative;
    }
}
