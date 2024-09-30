// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Mathematics;

namespace Vortice.Direct3D11;

public unsafe partial class ID3D11Device3
{
    public ID3D11DeviceContext3 CreateDeferredContext3()
    {
        return CreateDeferredContext3(0);
    }

    public void WriteToSubresource<T>(
        ID3D11Resource destinationResource, uint destinationSubresource,
        Span<T> sourceData, uint sourceRowPitch, uint srcDepthPitch) where T : unmanaged
    {
        fixed (void* dataPtr = sourceData)
        {
            WriteToSubresource(destinationResource, destinationSubresource, null,
                (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                );
        }
    }

    public void WriteToSubresource<T>(
        ID3D11Resource destinationResource, uint destinationSubresource, Box destinationBox,
        Span<T> sourceData, uint sourceRowPitch, uint srcDepthPitch) where T : unmanaged
    {
        fixed (void* dataPtr = sourceData)
        {
            WriteToSubresource(destinationResource, destinationSubresource, destinationBox,
                (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                );
        }
    }

    public void WriteToSubresource<T>(
        ID3D11Resource destinationResource, uint destinationSubresource,
        T[] sourceData, uint sourceRowPitch, uint srcDepthPitch) where T : unmanaged
    {
        fixed (void* sourceDataPtr = &sourceData[0])
        {
            WriteToSubresource(
                destinationResource, destinationSubresource, null,
                (IntPtr)sourceDataPtr, sourceRowPitch, srcDepthPitch
                );
        }
    }

    public void WriteToSubresource<T>(
        ID3D11Resource destinationResource, uint destinationSubresource, Box destinationBox,
        T[] sourceData, uint sourceRowPitch, uint srcDepthPitch) where T : unmanaged
    {
        fixed (void* sourceDataPtr = &sourceData[0])
        {
            WriteToSubresource(
                destinationResource, destinationSubresource, destinationBox,
                (IntPtr)sourceDataPtr, sourceRowPitch, srcDepthPitch
                );
        }
    }

    public void ReadFromSubresource<T>(
        T[] destination, uint destinationRowPitch, uint destinationDepthPitch,
        ID3D11Resource sourceResource, uint sourceSubresource, Box? sourceBox = null) where T : unmanaged
    {
        fixed (void* destinationPtr = &destination[0])
        {
            ReadFromSubresource(
                (IntPtr)destinationPtr, destinationRowPitch, destinationDepthPitch,
                sourceResource, sourceSubresource, sourceBox);
        }
    }
}
