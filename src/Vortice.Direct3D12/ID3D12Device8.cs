// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device8
    {
        public T? CreateCommittedResource2<T>(
            HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription1 description,
            ResourceStates initialResourceState,
            ClearValue? optimizedClearValue,
            ID3D12ProtectedResourceSession protectedSession) where T : ID3D12Resource
        {
            Result result = CreateCommittedResource2(
                ref heapProperties,
                heapFlags,
                ref description,
                initialResourceState,
                optimizedClearValue,
                protectedSession,
                typeof(T).GUID, out IntPtr nativePtr);

            if (result.Success)
            {
                return MarshallingHelpers.FromPointer<T>(nativePtr);
            }

            return default;
        }

        public T? CreatePlacedResource1<T>(ID3D12Heap heap, ulong heapOffset, ResourceDescription1 description, ResourceStates initialState, ClearValue? optimizedClearValue = null) where T : ID3D12Resource
        {
            Result result = CreatePlacedResource1(
                heap,
                heapOffset,
                ref description,
                initialState,
                optimizedClearValue,
                typeof(T).GUID, out IntPtr nativePtr);

            if (result.Success)
            {
                return MarshallingHelpers.FromPointer<T>(nativePtr);
            }

            return default;
        }
    }
}
