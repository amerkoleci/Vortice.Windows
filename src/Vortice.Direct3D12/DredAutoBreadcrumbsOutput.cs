// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

public partial class DredAutoBreadcrumbsOutput
{
    public AutoBreadcrumbNode? HeadAutoBreadcrumbNode { get; set; }

    #region Marshal
    internal unsafe struct __Native
    {
        public AutoBreadcrumbNode.__Native* pHeadAutoBreadcrumbNode;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        HeadAutoBreadcrumbNode?.__MarshalFree(@ref.pHeadAutoBreadcrumbNode);
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        HeadAutoBreadcrumbNode?.__MarshalFrom(@ref.pHeadAutoBreadcrumbNode);
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        HeadAutoBreadcrumbNode?.__MarshalTo(@ref.pHeadAutoBreadcrumbNode);
    }
    #endregion
}
