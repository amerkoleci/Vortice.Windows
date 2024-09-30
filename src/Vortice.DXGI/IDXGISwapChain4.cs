// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public unsafe partial class IDXGISwapChain4
{
    public void SetHDRMetaData<T>(HdrMetadataType type, T data)
        where T : unmanaged
    {
        SetHDRMetaData(type, (uint)sizeof(T), new IntPtr(&data));
    }

    public void SetHDRMetaData<T>(HdrMetadataType type, ref T data)
        where T : unmanaged
    {
        fixed (void* dataPtr = &data)
        {
            SetHDRMetaData(type, (uint)sizeof(T), (IntPtr)dataPtr);
        }
    }

    public void SetHDRMetaData(HdrMetadataType type, in HdrMetadataHdr10 data)
    {
        var native = new HdrMetadataHdr10.__Native();
        data.__MarshalTo(ref native);
        SetHDRMetaData(type, (uint)sizeof(HdrMetadataHdr10.__Native), new IntPtr(&native));
        data.__MarshalFree(ref native);
    }
}
