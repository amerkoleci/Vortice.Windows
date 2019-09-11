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

            var messagePtr = stackalloc byte[(int)messageSize];
            GetMessage(messageIndex, new IntPtr(messagePtr), ref messageSize);

            var message = new Message();
            message.__MarshalFrom(ref *(Message.__Native*)messagePtr);
            return message;
        }


        public unsafe InfoQueueFilter GetStorageFilter()
        {
            var sizeFilter = PointerSize.Zero;
            GetStorageFilter(IntPtr.Zero, ref sizeFilter);

            if (sizeFilter == 0)
            {
                return null;
            }
            var filter = stackalloc byte[(int)sizeFilter];
            GetStorageFilter((IntPtr)filter, ref sizeFilter);

            var queueNative = new InfoQueueFilter();
            queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);

            return queueNative;
        }

        public unsafe InfoQueueFilter GetRetrievalFilter()
        {
            var sizeFilter = PointerSize.Zero;
            GetRetrievalFilter(IntPtr.Zero, ref sizeFilter);

            if (sizeFilter == 0)
            {
                return null;
            }
            var filter = stackalloc byte[(int)sizeFilter];
            GetRetrievalFilter((IntPtr)filter, ref sizeFilter);

            var queueNative = new InfoQueueFilter();
            queueNative.__MarshalFrom(ref *(InfoQueueFilter.__Native*)filter);

            return queueNative;
        }
    }
}
