// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Vortice.DirectX.DXGI;
using SharpGen.Runtime;

namespace Vortice.Direct3D12
{
    public partial class GraphicsPipelineStateDescription
    {
        private Format[] _RTVFormats;

        public ID3D12RootSignature RootSignature { get; set; }

        public ShaderBytecode VertexShader { get; set; }

        public ShaderBytecode PixelShader { get; set; }

        public ShaderBytecode DomainShader { get; set; }

        public ShaderBytecode HullShader { get; set; }

        public ShaderBytecode GeometryShader { get; set; }

        public StreamOutputDescription StreamOutput { get; set; }

        public BlendDescription BlendState { get; set; }

        public uint SampleMask { get; set; } = uint.MaxValue;

        public RasterizerDescription RasterizerState { get; set; }

        public DepthStencilDescription DepthStencilState { get; set; } = DepthStencilDescription.Default;

        public InputLayoutDescription InputLayout { get; set; }

        public IndexBufferStripCutValue IndexBufferStripCutValue { get; set; }

        public PrimitiveTopologyType PrimitiveTopologyType { get; set; }

        public Format[] RenderTargetFormats
        {
            get => _RTVFormats ?? (_RTVFormats = new Format[BlendDescription.SimultaneousRenderTargetCount]);
            set => _RTVFormats = value;
        }

        public Format DepthStencilFormat { get; set; }

        public SampleDescription SampleDescription { get; set; } = new SampleDescription(1, 0);

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

        internal void __MarshalFree(ref __Native @ref)
        {
            GC.KeepAlive(RootSignature);
            VertexShader?.__MarshalFree(ref @ref.VertexShader);
            PixelShader?.__MarshalFree(ref @ref.PixelShader);
            DomainShader?.__MarshalFree(ref @ref.DomainShader);
            HullShader?.__MarshalFree(ref @ref.HullShader);
            GeometryShader?.__MarshalFree(ref @ref.GeometryShader);
            StreamOutput?.__MarshalFree(ref @ref.StreamOutput);
            BlendState.__MarshalFree(ref @ref.BlendState);
            InputLayout?.__MarshalFree(ref @ref.InputLayout);
            CachedPSO.__MarshalFree(ref @ref.CachedPSO);
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.RootSignature = CppObject.ToCallbackPtr<ID3D12RootSignature>(RootSignature);
            VertexShader?.__MarshalTo(ref @ref.VertexShader);
            PixelShader?.__MarshalTo(ref @ref.PixelShader);
            DomainShader?.__MarshalTo(ref @ref.DomainShader);
            HullShader?.__MarshalTo(ref @ref.HullShader);
            GeometryShader?.__MarshalTo(ref @ref.GeometryShader);
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
                @ref.NumRenderTargets = Math.Min(RenderTargetFormats.Length, BlendDescription.SimultaneousRenderTargetCount);
                MemoryHelpers.CopyMemory(
                    (IntPtr)Unsafe.AsPointer(ref @ref.RenderTargetFormats),
                    (IntPtr)Unsafe.AsPointer(ref RenderTargetFormats[0]),
                    @ref.NumRenderTargets * sizeof(Format));
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
}
