// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.Direct3D12;
using System.Runtime.InteropServices;

namespace HelloDirect3D12;

public sealed partial class D3D12GraphicsDevice
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PipelineStateStream
    {
        public PipelineStateSubObjectTypeRootSignature RootSignature;
        public PipelineStateSubObjectTypeVertexShader VertexShader;
        public PipelineStateSubObjectTypePixelShader PixelShader;
        public PipelineStateSubObjectTypeInputLayout InputLayout;
        public PipelineStateSubObjectTypeSampleMask SampleMask;
        public PipelineStateSubObjectTypePrimitiveTopology PrimitiveTopology;
        public PipelineStateSubObjectTypeRasterizer RasterizerState;
        public PipelineStateSubObjectTypeBlend BlendState;
        public PipelineStateSubObjectTypeDepthStencil DepthStencilState;
        public PipelineStateSubObjectTypeRenderTargetFormats RenderTargetFormats;
        public PipelineStateSubObjectTypeDepthStencilFormat DepthStencilFormat;
        public PipelineStateSubObjectTypeSampleDescription SampleDescription;
    }
}
