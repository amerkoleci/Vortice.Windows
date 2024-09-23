// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public partial struct ShaderNode
{
    public string? Shader;
    public NodeOverridesType OverridesType;
    public BroadcastingLaunchOverrides? BroadcastingLaunchOverrides;
    public CoalescingLaunchOverrides? CoalescingLaunchOverrides;
    public ThreadLaunchOverrides? ThreadLaunchOverrides;
    public CommonComputeNodeOverrides? CommonComputeNodeOverrides;

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal unsafe partial struct __Native
    {
        public IntPtr Shader;
        public NodeOverridesType OverridesType;
        public Union Inner;

        [StructLayout(LayoutKind.Explicit, Pack = 0)]
        internal struct Union
        {
            [FieldOffset(0)]
            public BroadcastingLaunchOverrides* pBroadcastingLaunchOverrides;

            [FieldOffset(0)]
            public CoalescingLaunchOverrides* pCoalescingLaunchOverrides;

            [FieldOffset(0)]
            public ThreadLaunchOverrides* pThreadLaunchOverrides;

            [FieldOffset(0)]
            public CommonComputeNodeOverrides* pCommonComputeNodeOverrides;
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        Marshal.FreeHGlobal(@ref.Shader);
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        Shader = Marshal.PtrToStringAnsi(@ref.Shader);
        OverridesType = @ref.OverridesType;

        if (@ref.Inner.pBroadcastingLaunchOverrides != null)
            BroadcastingLaunchOverrides = *@ref.Inner.pBroadcastingLaunchOverrides;

        if (@ref.Inner.pCoalescingLaunchOverrides != null)
            CoalescingLaunchOverrides = *@ref.Inner.pCoalescingLaunchOverrides;

        if (@ref.Inner.pThreadLaunchOverrides != null)
            ThreadLaunchOverrides = *@ref.Inner.pThreadLaunchOverrides;

        if (@ref.Inner.pCommonComputeNodeOverrides != null)
            CommonComputeNodeOverrides = *@ref.Inner.pCommonComputeNodeOverrides;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Shader = Marshal.StringToHGlobalAnsi(Shader);
        @ref.OverridesType = OverridesType;

        //if (BroadcastingLaunchOverrides.HasValue)
        //    @ref.Inner.pBroadcastingLaunchOverrides = Unsafe.AsPointer(ref BroadcastingLaunchOverrides.Value);

        //if (@ref.Inner.pCoalescingLaunchOverrides != null)
        //    CoalescingLaunchOverrides = *@ref.Inner.pCoalescingLaunchOverrides;

        //if (@ref.Inner.pThreadLaunchOverrides != null)
        //    ThreadLaunchOverrides = *@ref.Inner.pThreadLaunchOverrides;

        //if (@ref.Inner.pCommonComputeNodeOverrides != null)
        //    CommonComputeNodeOverrides = *@ref.Inner.pCommonComputeNodeOverrides;
    }
    #endregion
}
