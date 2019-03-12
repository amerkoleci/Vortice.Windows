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
    }
}
