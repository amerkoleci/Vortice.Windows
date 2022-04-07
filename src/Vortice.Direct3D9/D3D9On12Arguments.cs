// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

/// <unmanaged>D3D9ON12_ARGS</unmanaged>
/// <unmanaged-short>D3D9ON12_ARGS</unmanaged-short>
public partial struct D3D9On12Arguments
{
    public bool Enable9On12 { get; set; }
    public ComObject? D3D12Device { get; set; }
    public ComObject? D3D12Queue1 { get; set; }
    public ComObject? D3D12Queue2 { get; set; }
    public uint NodeMask { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal partial struct __Native
    {
        public RawBool Enable9On12;
        public IntPtr pD3D12Device;
        public IntPtr pD3D12Queue1;
        public IntPtr pD3D12Queue2;
        public uint NumQueues;
        public uint NodeMask;
    }
    #endregion

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.Enable9On12 = Enable9On12;
        @ref.pD3D12Device = MarshallingHelpers.ToCallbackPtr(D3D12Device);
        @ref.NumQueues = 0;
        if (D3D12Queue1 != null)
        {
            @ref.pD3D12Queue1 = MarshallingHelpers.ToCallbackPtr(D3D12Queue1);
            @ref.NumQueues++;
        }
        if (D3D12Queue2 != null)
        {
            @ref.pD3D12Queue2 = MarshallingHelpers.ToCallbackPtr(D3D12Queue2);
            @ref.NumQueues++;
        }

        @ref.NodeMask = NodeMask;
    }
}
