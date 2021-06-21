// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.DXGI.Debug
{
    public partial class IDXGIInfoQueue
    {
        public unsafe InfoQueueMessage GetMessage(Guid producer, ulong messageIndex)
        {
            PointerSize messageSize = 0;
            GetMessage(producer, messageIndex, IntPtr.Zero, ref messageSize);

            if (messageSize == 0)
            {
                return new InfoQueueMessage();
            }

            var messagePtr = stackalloc byte[messageSize];
            GetMessage(producer, messageIndex, new IntPtr(messagePtr), ref messageSize);

            var message = new InfoQueueMessage();
            message.__MarshalFrom(ref *(InfoQueueMessage.__Native*)messagePtr);
            return message;
        }


        public unsafe InfoQueueFilter? GetStorageFilter(Guid producer)
        {
            PointerSize sizeFilter = PointerSize.Zero;
            GetStorageFilter(producer, IntPtr.Zero, ref sizeFilter);

            if (sizeFilter == 0)
            {
                return null;
            }

            byte* filter = stackalloc byte[(int)sizeFilter];
            GetStorageFilter(producer, (IntPtr)filter, ref sizeFilter);

            var queueNative = new InfoQueueFilter();
            queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);

            return queueNative;
        }

        public unsafe InfoQueueFilter? GetRetrievalFilter(Guid producer)
        {
            var sizeFilter = PointerSize.Zero;
            GetRetrievalFilter(producer, IntPtr.Zero, ref sizeFilter);

            if (sizeFilter == 0)
            {
                return null;
            }
            var filter = stackalloc byte[(int)sizeFilter];
            GetRetrievalFilter(producer, (IntPtr)filter, ref sizeFilter);

            var queueNative = new InfoQueueFilter();
            queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);

            return queueNative;
        }
    }
}
