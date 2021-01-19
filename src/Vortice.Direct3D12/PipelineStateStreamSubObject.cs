// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice.DXGI;

namespace Vortice.Direct3D12
{
    public interface IPipelineStateStreamSubObject
    {
        PipelineStateSubObjectType Type { get; }
    }

    internal struct AlignedSubobjectType<T> where T : unmanaged
    {
        internal PipelineStateSubObjectType _type;
        internal T _inner;
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct PipelineStateSubObjectTypeRootSignature : IPipelineStateStreamSubObject
    {
        public readonly PipelineStateSubObjectType Type;
        public readonly IntPtr RootSignature;

        public PipelineStateSubObjectTypeRootSignature(ID3D12RootSignature rootSignature)
        {
            Type = PipelineStateSubObjectType.RootSignature;
            RootSignature = rootSignature.NativePointer;
        }

        public static implicit operator PipelineStateSubObjectTypeRootSignature(ID3D12RootSignature rootSignature)
        {
            return new PipelineStateSubObjectTypeRootSignature(rootSignature);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => Type;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeVertexShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeVertexShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.VertexShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeVertexShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeVertexShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.VertexShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypePixelShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypePixelShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.PixelShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypePixelShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypePixelShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.PixelShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeGeometryShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeGeometryShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.GeometryShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeGeometryShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeGeometryShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.GeometryShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeStreamOutput : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<StreamOutputDescription.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeStreamOutput(in StreamOutputDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.StreamOutput;
            _type._inner = new StreamOutputDescription.__Native();
            description.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeStreamOutput(in StreamOutputDescription description)
        {
            return new PipelineStateSubObjectTypeStreamOutput(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.StreamOutput;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeHullShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeHullShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.HullShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeHullShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeHullShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.HullShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeDomainShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeDomainShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.DomainShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeDomainShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeDomainShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.DomainShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeAmplificationShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeAmplificationShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.AmplificationShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeAmplificationShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeAmplificationShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.AmplificationShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeMeshShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeMeshShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.MeshShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeMeshShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeMeshShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.MeshShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeComputeShader : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ShaderBytecode.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeComputeShader(ShaderBytecode bytecode)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.ComputeShader;
            _type._inner = new ShaderBytecode.__Native();
            bytecode.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeComputeShader(ShaderBytecode bytecode)
        {
            return new PipelineStateSubObjectTypeComputeShader(bytecode);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.ComputeShader;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeIndexBufferStripCutValue : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<IndexBufferStripCutValue> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeIndexBufferStripCutValue(IndexBufferStripCutValue value)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.IndexBufferStripCutValue;
            _type._inner = value;
        }

        public static implicit operator PipelineStateSubObjectTypeIndexBufferStripCutValue(IndexBufferStripCutValue value)
        {
            return new PipelineStateSubObjectTypeIndexBufferStripCutValue(value);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.IndexBufferStripCutValue;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypePrimitiveTopology : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<PrimitiveTopologyType> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypePrimitiveTopology(PrimitiveTopologyType value)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.PrimitiveTopology;
            _type._inner = value;
        }

        public static implicit operator PipelineStateSubObjectTypePrimitiveTopology(PrimitiveTopologyType value)
        {
            return new PipelineStateSubObjectTypePrimitiveTopology(value);
        }
        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.PrimitiveTopology;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeInputLayout : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<InputLayoutDescription.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeInputLayout(in InputLayoutDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.InputLayout;
            _type._inner = new InputLayoutDescription.__Native();
            description.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeInputLayout(in InputLayoutDescription description)
        {
            return new PipelineStateSubObjectTypeInputLayout(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.InputLayout;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeBlend : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<BlendDescription.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeBlend(in BlendDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.Blend;
            _type._inner = new BlendDescription.__Native();
            description.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeBlend(in BlendDescription description)
        {
            return new PipelineStateSubObjectTypeBlend(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.Blend;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeDepthStencil : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<DepthStencilDescription> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeDepthStencil(in DepthStencilDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.DepthStencil;
            _type._inner = description;
        }

        public static implicit operator PipelineStateSubObjectTypeDepthStencil(in DepthStencilDescription description)
        {
            return new PipelineStateSubObjectTypeDepthStencil(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.DepthStencil;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeDepthStencil1 : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<DepthStencilDescription1> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeDepthStencil1(in DepthStencilDescription1 description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.DepthStencil1;
            _type._inner = description;
        }

        public static implicit operator PipelineStateSubObjectTypeDepthStencil1(in DepthStencilDescription1 description)
        {
            return new PipelineStateSubObjectTypeDepthStencil1(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.DepthStencil1;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeDepthStencilFormat : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<Format> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeDepthStencilFormat(Format format)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.DepthStencilFormat;
            _type._inner = format;
        }

        public static implicit operator PipelineStateSubObjectTypeDepthStencilFormat(Format format)
        {
            return new PipelineStateSubObjectTypeDepthStencilFormat(format);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.DepthStencilFormat;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeRasterizer : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<RasterizerDescription> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeRasterizer(in RasterizerDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.Rasterizer;
            _type._inner = description;
        }

        public static implicit operator PipelineStateSubObjectTypeRasterizer(in RasterizerDescription description)
        {
            return new PipelineStateSubObjectTypeRasterizer(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.Rasterizer;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeRenderTargetFormats : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<RtFormatArray.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeRenderTargetFormats(Format[] formats)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.RenderTargetFormats;

            RtFormatArray description = new RtFormatArray(formats);
            _type._inner = new RtFormatArray.__Native();
            description.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeRenderTargetFormats(Format[] formats)
        {
            return new PipelineStateSubObjectTypeRenderTargetFormats(formats);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.RenderTargetFormats;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeSampleDescription : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<SampleDescription> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeSampleDescription(in SampleDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.SampleDescription;
            _type._inner = description;
        }

        public static implicit operator PipelineStateSubObjectTypeSampleDescription(in SampleDescription description)
        {
            return new PipelineStateSubObjectTypeSampleDescription(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.SampleDescription;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeSampleMask : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<uint> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeSampleMask(uint value)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.SampleMask;
            _type._inner = value;
        }

        public static implicit operator PipelineStateSubObjectTypeSampleMask(uint value)
        {
            return new PipelineStateSubObjectTypeSampleMask(value);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.SampleMask;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeNodeMask : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<uint> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeNodeMask(uint value)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.NodeMask;
            _type._inner = value;
        }

        public static implicit operator PipelineStateSubObjectTypeNodeMask(uint value)
        {
            return new PipelineStateSubObjectTypeNodeMask(value);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.NodeMask;
    }


    [StructLayout(LayoutKind.Explicit)]
    public struct PipelineStateStreamFlags : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<PipelineStateFlags> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateStreamFlags(PipelineStateFlags flags) : this()
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.Flags;
            _type._inner = flags;
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.Flags;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeCachedPipelineState : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<CachedPipelineState.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeCachedPipelineState(in CachedPipelineState description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.CachedPipelineStateObject;
            _type._inner = new CachedPipelineState.__Native();
            description.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeCachedPipelineState(in CachedPipelineState description)
        {
            return new PipelineStateSubObjectTypeCachedPipelineState(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.CachedPipelineStateObject;
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct PipelineStateSubObjectTypeViewInstancing : IPipelineStateStreamSubObject
    {
        [FieldOffset(0)]
        internal readonly AlignedSubobjectType<ViewInstancingDescription.__Native> _type;

        [FieldOffset(0)]
        internal readonly PointerSize _pad;

        public PipelineStateSubObjectTypeViewInstancing(in ViewInstancingDescription description)
        {
            _pad = default;
            _type._type = PipelineStateSubObjectType.ViewInstancing;
            _type._inner = new ViewInstancingDescription.__Native();
            description.__MarshalTo(ref _type._inner);
        }

        public static implicit operator PipelineStateSubObjectTypeViewInstancing(in ViewInstancingDescription description)
        {
            return new PipelineStateSubObjectTypeViewInstancing(description);
        }

        PipelineStateSubObjectType IPipelineStateStreamSubObject.Type => PipelineStateSubObjectType.CachedPipelineStateObject;
    }
}
