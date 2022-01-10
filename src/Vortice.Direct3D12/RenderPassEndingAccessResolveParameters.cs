// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.DXGI;

namespace Vortice.Direct3D12;

public partial class RenderPassEndingAccessResolveParameters
{
    private RenderPassEndingAccessResolveSubresourceParameters _subresourceParameters;

    public ID3D12Resource? SrcResource { get; set; }

    public ID3D12Resource? DstResource { get; set; }

    public int SubresourceCount { get; set; }

    public RenderPassEndingAccessResolveSubresourceParameters SubresourceParameters
    {
        get => _subresourceParameters;
        set => _subresourceParameters = value;
    }

    public Format Format { get; set; }
    public ResolveMode ResolveMode { get; set; }
    public bool PreserveResolveSource { get; set; }

    #region Marshal
    internal unsafe struct __Native
    {
        public IntPtr pSrcResource;
        public IntPtr pDstResource;
        public int SubresourceCount;
        public RenderPassEndingAccessResolveSubresourceParameters* pSubresourceParameters;
        public Format Format;
        public ResolveMode ResolveMode;
        public RawBool PreserveResolveSource;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        GC.KeepAlive(SrcResource);
        GC.KeepAlive(DstResource);
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        if (@ref.pSrcResource != IntPtr.Zero)
        {
            SrcResource = new ID3D12Resource(@ref.pSrcResource);
        }

        if (@ref.pDstResource != IntPtr.Zero)
        {
            DstResource = new ID3D12Resource(@ref.pDstResource);
        }

        SubresourceCount = @ref.SubresourceCount;
        if (@ref.pSubresourceParameters != null)
        {
            fixed (void* dest = &_subresourceParameters)
            {
                MemoryHelpers.CopyMemory((IntPtr)dest, (IntPtr)@ref.pSubresourceParameters, sizeof(RenderPassEndingAccessResolveSubresourceParameters));
            }
        }
        Format = @ref.Format;
        ResolveMode = @ref.ResolveMode;
        PreserveResolveSource = @ref.PreserveResolveSource;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.pSrcResource = SrcResource != null ? SrcResource.NativePointer : IntPtr.Zero;
        @ref.pDstResource = DstResource != null ? DstResource.NativePointer : IntPtr.Zero;
        @ref.SubresourceCount = SubresourceCount;
        @ref.pSubresourceParameters = (RenderPassEndingAccessResolveSubresourceParameters*)Unsafe.AsPointer(ref _subresourceParameters);
        @ref.Format = Format;
        @ref.ResolveMode = ResolveMode;
        @ref.PreserveResolveSource = PreserveResolveSource;
    }
    #endregion
}
