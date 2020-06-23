// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.DXGI
{
    public partial class IDXGISwapChain4
    {
        public unsafe void SetHDRMetaData<T>(HdrMetadataType type, T data) where T : unmanaged
        {
            SetHDRMetaData(type, sizeof(T), new IntPtr(&data));
        }

        public unsafe void SetHDRMetaData<T>(HdrMetadataType type, ref T data) where T : unmanaged
        {
            fixed (void* dataPtr = &data)
            {
                SetHDRMetaData(type, sizeof(T), (IntPtr)dataPtr);
            }
        }

        public unsafe void SetHDRMetaData(HdrMetadataType type, HdrMetadataHdr10 data)
        {
            var native = new HdrMetadataHdr10.__Native();
            data.__MarshalTo(ref native);
            SetHDRMetaData(type, sizeof(HdrMetadataHdr10.__Native), new IntPtr(&native));
            data.__MarshalFree(ref native);
        }
    }
}
