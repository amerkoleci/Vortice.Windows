// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D11.Debug
{
    public partial class ID3D11InfoQueue
    {
        public unsafe Message GetMessage(ulong messageIndex)
        {
            PointerSize messageSize = 0;
            GetMessage(messageIndex, IntPtr.Zero, ref messageSize);

            if (messageSize == 0)
            {
                return new Message();
            }

            byte* messagePtr = stackalloc byte[messageSize];
            GetMessage(messageIndex, new IntPtr(messagePtr), ref messageSize);

            Message message = new Message();
            message.__MarshalFrom(ref *(Message.__Native*)messagePtr);
            return message;
        }


        public unsafe InfoQueueFilter GetStorageFilter()
        {
            PointerSize sizeFilter = PointerSize.Zero;
            GetStorageFilter(IntPtr.Zero, ref sizeFilter);

            if (sizeFilter == 0)
            {
                return null;
            }

            byte* filter = stackalloc byte[sizeFilter];
            GetStorageFilter((IntPtr)filter, ref sizeFilter);

            InfoQueueFilter queueNative = new InfoQueueFilter();
            queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);
            return queueNative;
        }

        public unsafe InfoQueueFilter GetRetrievalFilter()
        {
            PointerSize sizeFilter = PointerSize.Zero;
            GetRetrievalFilter(IntPtr.Zero, ref sizeFilter);

            if (sizeFilter == 0)
            {
                return null;
            }

            byte* filter = stackalloc byte[sizeFilter];
            GetRetrievalFilter((IntPtr)filter, ref sizeFilter);

            InfoQueueFilter queueNative = new InfoQueueFilter();
            queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);
            return queueNative;
        }
    }
}
