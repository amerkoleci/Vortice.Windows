// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using SharpDXGI;

namespace SharpD3D12
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
            Guard.NotNull(protectedSession, nameof(protectedSession));

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
            Guard.NotNull(protectedSession, nameof(protectedSession));

            return CreateHeap1(ref description, protectedSession, typeof(ID3D12Heap1).GUID);
        }
    }
}
