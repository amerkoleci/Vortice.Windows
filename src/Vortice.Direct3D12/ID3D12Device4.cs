// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

namespace Vortice.Direct3D12
{
    public partial class ID3D12Device4
    {
        public ID3D12GraphicsCommandList1 CreateCommandList1(CommandListType type, CommandListFlags commandListFlags = CommandListFlags.None)
        {
            return CreateCommandList1(0, type, commandListFlags, typeof(ID3D12GraphicsCommandList1).GUID);
        }

        public ID3D12GraphicsCommandList1 CreateCommandList1(int nodeMask, CommandListType type, CommandListFlags commandListFlags = CommandListFlags.None)
        {
            return CreateCommandList1(nodeMask, type, commandListFlags, typeof(ID3D12GraphicsCommandList1).GUID);
        }

        public ID3D12Resource1 CreateCommittedResource1(
            HeapProperties heapProperties,
            HeapFlags heapFlags,
            ResourceDescription description,
            ResourceStates initialResourceState,
            ID3D12ProtectedResourceSession protectedSession,
            ClearValue? optimizedClearValue = null)
        {
            return CreateCommittedResource1(
                ref heapProperties,
                heapFlags,
                ref description,
                initialResourceState,
                optimizedClearValue,
                protectedSession,
                typeof(ID3D12Resource1).GUID);
        }

        public ID3D12Heap1 CreateHeap1(HeapDescription description, ID3D12ProtectedResourceSession protectedSession)
        {
            return CreateHeap1(ref description, protectedSession, typeof(ID3D12Heap1).GUID);
        }

        public ID3D12ProtectedResourceSession CreateProtectedResourceSession(ProtectedResourceSessionDescription description)
        {
            return CreateProtectedResourceSession(description, typeof(ID3D12ProtectedResourceSession).GUID);
        }

        public ID3D12Resource1 CreateReservedResource1(ResourceDescription description, ResourceStates initialState, ClearValue clearValue, ID3D12ProtectedResourceSession protectedResourceSession)
        {
            return CreateReservedResource1(ref description, initialState, clearValue, protectedResourceSession, typeof(ID3D12Resource1).GUID);
        }

        public ID3D12Resource1 CreateReservedResource1(ResourceDescription description, ResourceStates initialState, ID3D12ProtectedResourceSession protectedResourceSession)
        {
            return CreateReservedResource1(ref description, initialState, null, protectedResourceSession, typeof(ID3D12Resource1).GUID);
        }
    }
}
