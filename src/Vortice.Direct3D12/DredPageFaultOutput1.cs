// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class DredPageFaultOutput1
{
    public ulong PageFaultVA { get; set; }

    public DredAllocationNode1? HeadExistingAllocationNode { get; set; }
    public DredAllocationNode1? HeadRecentFreedAllocationNode { get; set; }

    #region Marshal
    internal unsafe struct __Native
    {
        public ulong PageFaultVA;
        public DredAllocationNode1.__Native* pHeadExistingAllocationNode;
        public DredAllocationNode1.__Native* pHeadRecentFreedAllocationNode;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        HeadExistingAllocationNode?.__MarshalFree(@ref.pHeadExistingAllocationNode);
        HeadRecentFreedAllocationNode?.__MarshalFree(@ref.pHeadRecentFreedAllocationNode);
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        HeadExistingAllocationNode?.__MarshalFrom(@ref.pHeadExistingAllocationNode);
        HeadRecentFreedAllocationNode?.__MarshalFrom(@ref.pHeadRecentFreedAllocationNode);
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        HeadExistingAllocationNode?.__MarshalTo(@ref.pHeadExistingAllocationNode);
        HeadRecentFreedAllocationNode?.__MarshalTo(@ref.pHeadRecentFreedAllocationNode);
    }
    #endregion
}
