// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes bindings (fixed for the duration of the render pass) to one or more render target views (RTVs), as well as their beginning and ending access characteristics.
/// </summary>
public partial struct RenderPassRenderTargetDescription
{
    /// <summary>
    /// Initialize new instance of <see cref="RenderPassRenderTargetDescription"/> struct.
    /// </summary>
    /// <param name="cpuDescriptor">The CPU <see cref="CpuDescriptorHandle"/> handle corresponding to the render target view(s) (RTVs).</param>
    /// <param name="beginningAccess">The access to the RTV(s) requested at the transition into a render pass.</param>
    /// <param name="endingAccess">The access to the RTV(s) requested at the transition out of a render pass.</param>
    public RenderPassRenderTargetDescription(
        CpuDescriptorHandle cpuDescriptor,
        RenderPassBeginningAccess beginningAccess,
        RenderPassEndingAccess endingAccess)
    {
        CpuDescriptor = cpuDescriptor;
        BeginningAccess = beginningAccess;
        EndingAccess = endingAccess;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct __Native
    {
        public CpuDescriptorHandle CpuDescriptor;

        public RenderPassBeginningAccess BeginningAccess;

        public RenderPassEndingAccess.__Native EndingAccess;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.CpuDescriptor = CpuDescriptor;
        @ref.BeginningAccess = BeginningAccess;
        EndingAccess.__MarshalTo(ref @ref.EndingAccess);
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        EndingAccess.__MarshalFree(ref @ref.EndingAccess);
    }

    //internal void __MarshalFrom(ref __Native @ref)
    //{
    //    CpuDescriptor = @ref.CpuDescriptor;
    //    BeginningAccess = @ref.BeginningAccess;
    //    EndingAccess.__MarshalFrom(ref @ref.EndingAccess);
    //}
    #endregion
}
