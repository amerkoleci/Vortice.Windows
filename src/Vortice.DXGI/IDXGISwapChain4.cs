// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.DXGI
{
    public partial class IDXGISwapChain4
    {
        public unsafe void SetHDRMetaData<T>(HdrMetadataType type, T data) where T : struct
        {
            SetHDRMetaData(type, Unsafe.SizeOf<T>(), new IntPtr(Unsafe.AsPointer(ref data)));
        }

        public unsafe void SetHDRMetaData<T>(HdrMetadataType type, ref T data) where T : struct
        {
            SetHDRMetaData(type, Unsafe.SizeOf<T>(), new IntPtr(Unsafe.AsPointer(ref data)));
        }

        public unsafe void SetHDRMetaData(HdrMetadataType type, HdrMetadataHdr10 data)
        {
            SetHDRMetaData(type, Unsafe.SizeOf<HdrMetadataHdr10>(), new IntPtr(Unsafe.AsPointer(ref data)));
        }
    }
}
