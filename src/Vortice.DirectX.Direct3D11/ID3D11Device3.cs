// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;

namespace Vortice.DirectX.Direct3D11
{
    public partial class ID3D11Device3
    {
        public unsafe ID3D11DeviceContext3 CreateDeferredContext3()
        {
            return CreateDeferredContext3(0);
        }

        public unsafe void WriteToSubresource<T>(
            ID3D11Resource destinationResource, int destinationSubresource,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                WriteToSubresource(destinationResource, destinationSubresource, null,
                    (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                    );
            }
        }

        public unsafe void WriteToSubresource<T>(
            ID3D11Resource destinationResource, int destinationSubresource, Box destinationBox,
            Span<T> sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            fixed (void* dataPtr = sourceData)
            {
                WriteToSubresource(destinationResource, destinationSubresource, destinationBox,
                    (IntPtr)dataPtr, sourceRowPitch, srcDepthPitch
                    );
            }
        }

        public unsafe void WriteToSubresource<T>(
            ID3D11Resource destinationResource, int destinationSubresource,
            T[] sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            WriteToSubresource(
                destinationResource, destinationSubresource, null,
                (IntPtr)Unsafe.AsPointer(ref sourceData[0]), sourceRowPitch, srcDepthPitch
                );
        }

        public unsafe void WriteToSubresource<T>(
            ID3D11Resource destinationResource, int destinationSubresource, Box destinationBox,
            T[] sourceData, int sourceRowPitch, int srcDepthPitch) where T : unmanaged
        {
            WriteToSubresource(
                destinationResource, destinationSubresource, destinationBox,
                (IntPtr)Unsafe.AsPointer(ref sourceData[0]), sourceRowPitch, srcDepthPitch
                );
        }

        public unsafe void ReadFromSubresource<T>(
            T[] destination, int destinationRowPitch, int destinationDepthPitch,
            ID3D11Resource sourceResource, int sourceSubresource, Box? sourceBox = null) where T : unmanaged
        {
            ReadFromSubresource(
                (IntPtr)Unsafe.AsPointer(ref destination[0]), destinationRowPitch, destinationDepthPitch,
                sourceResource, sourceSubresource, sourceBox);
        }
    }
}
