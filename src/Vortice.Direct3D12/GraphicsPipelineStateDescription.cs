// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.DXGI;
using static Vortice.UnsafeUtilities;

namespace Vortice.Direct3D12;

public partial class GraphicsPipelineStateDescription
{
    private Format[]? _RTVFormats;

    public ID3D12RootSignature? RootSignature { get; set; }

    public ReadOnlyMemory<byte> VertexShader { get; set; }

    public ReadOnlyMemory<byte> PixelShader { get; set; }

    public ReadOnlyMemory<byte> DomainShader { get; set; }

    public ReadOnlyMemory<byte> HullShader { get; set; }

    public ReadOnlyMemory<byte> GeometryShader { get; set; }

    public StreamOutputDescription? StreamOutput { get; set; }

    public BlendDescription BlendState { get; set; }

    public uint SampleMask { get; set; } = uint.MaxValue;

    public RasterizerDescription RasterizerState { get; set; }

    public DepthStencilDescription DepthStencilState { get; set; } = DepthStencilDescription.Default;

    public InputLayoutDescription? InputLayout { get; set; }

    public IndexBufferStripCutValue IndexBufferStripCutValue { get; set; }

    public PrimitiveTopologyType PrimitiveTopologyType { get; set; }

    public Format[] RenderTargetFormats
    {
        get => _RTVFormats ??= new Format[D3D12.SimultaneousRenderTargetCount];
        set => _RTVFormats = value;
    }

    public Format DepthStencilFormat { get; set; }

    public SampleDescription SampleDescription { get; set; } = SampleDescription.Default;

    public int NodeMask { get; set; }

    public CachedPipelineState CachedPSO { get; set; }

    public PipelineStateFlags Flags { get; set; }

    #region Marshal
    internal struct __Native
    {
        public IntPtr RootSignature;
        public ShaderBytecode.__Native VertexShader;
        public ShaderBytecode.__Native PixelShader;
        public ShaderBytecode.__Native DomainShader;
        public ShaderBytecode.__Native HullShader;
        public ShaderBytecode.__Native GeometryShader;
        public StreamOutputDescription.__Native StreamOutput;
        public BlendDescription.__Native BlendState;
        public uint SampleMask;
        public RasterizerDescription RasterizerState;
        public DepthStencilDescription DepthStencilState;
        public InputLayoutDescription.__Native InputLayout;
        public IndexBufferStripCutValue IBStripCutValue;
        public PrimitiveTopologyType PrimitiveTopologyType;
        public int NumRenderTargets;
        public Format RenderTargetFormats;
        private Format __RenderTargetFormats1;
        private Format __RenderTargetFormats2;
        private Format __RenderTargetFormats3;
        private Format __RenderTargetFormats4;
        private Format __RenderTargetFormats5;
        private Format __RenderTargetFormats6;
        private Format __RenderTargetFormats7;
        public Format DepthStencilFormat;
        public SampleDescription SampleDescription;
        public int NodeMask;
        public CachedPipelineState.__Native CachedPSO;
        public PipelineStateFlags Flags;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        GC.KeepAlive(RootSignature);
        if (@ref.VertexShader.BytecodeLength > 0)
            Free(@ref.VertexShader.pShaderBytecode);
        if (@ref.PixelShader.BytecodeLength > 0)
            Free(@ref.PixelShader.pShaderBytecode);
        if (@ref.DomainShader.BytecodeLength > 0)
            Free(@ref.DomainShader.pShaderBytecode);
        if (@ref.HullShader.BytecodeLength > 0)
            Free(@ref.HullShader.pShaderBytecode);
        if (@ref.GeometryShader.BytecodeLength > 0)
            Free(@ref.GeometryShader.pShaderBytecode);
        StreamOutput?.__MarshalFree(ref @ref.StreamOutput);
        BlendState.__MarshalFree(ref @ref.BlendState);
        InputLayout?.__MarshalFree(ref @ref.InputLayout);
        CachedPSO.__MarshalFree(ref @ref.CachedPSO);
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.RootSignature = MarshallingHelpers.ToCallbackPtr<ID3D12RootSignature>(RootSignature);
        if (VertexShader.Length > 0)
        {
            @ref.VertexShader.pShaderBytecode = AllocWithData(VertexShader.Span);
            @ref.VertexShader.BytecodeLength = (nuint)VertexShader.Length;
        }
        if (PixelShader.Length > 0)
        {
            @ref.PixelShader.pShaderBytecode = AllocWithData(PixelShader.Span);
            @ref.PixelShader.BytecodeLength = (nuint)PixelShader.Length;
        }
        if (DomainShader.Length > 0)
        {
            @ref.DomainShader.pShaderBytecode = AllocWithData(DomainShader.Span);
            @ref.DomainShader.BytecodeLength = (nuint)DomainShader.Length;
        }
        if (HullShader.Length > 0)
        {
            @ref.HullShader.pShaderBytecode = AllocWithData(HullShader.Span);
            @ref.HullShader.BytecodeLength = (nuint)HullShader.Length;
        }
        if (GeometryShader.Length > 0)
        {
            @ref.GeometryShader.pShaderBytecode = AllocWithData(GeometryShader.Span);
            @ref.GeometryShader.BytecodeLength = (nuint)GeometryShader.Length;
        }
        StreamOutput?.__MarshalTo(ref @ref.StreamOutput);
        BlendState.__MarshalTo(ref @ref.BlendState);
        @ref.SampleMask = SampleMask;
        @ref.RasterizerState = RasterizerState;
        @ref.DepthStencilState = DepthStencilState;
        InputLayout?.__MarshalTo(ref @ref.InputLayout);
        @ref.IBStripCutValue = IndexBufferStripCutValue;
        @ref.PrimitiveTopologyType = PrimitiveTopologyType;
        if (RenderTargetFormats.Length > 0)
        {
            @ref.NumRenderTargets = Math.Min(RenderTargetFormats.Length, D3D12.SimultaneousRenderTargetCount);
            fixed (void* renderTargetFormatsPtr = &RenderTargetFormats[0])
            {
                MemoryHelpers.CopyMemory(
                    (IntPtr)Unsafe.AsPointer(ref @ref.RenderTargetFormats),
                    (IntPtr)renderTargetFormatsPtr,
                    @ref.NumRenderTargets * sizeof(Format));
            }
        }
        else
        {
            @ref.NumRenderTargets = 0;
        }

        @ref.DepthStencilFormat = DepthStencilFormat;
        @ref.SampleDescription = SampleDescription;
        @ref.NodeMask = NodeMask;
        CachedPSO.__MarshalTo(ref @ref.CachedPSO);
        @ref.Flags = Flags;
    }
    #endregion
}
