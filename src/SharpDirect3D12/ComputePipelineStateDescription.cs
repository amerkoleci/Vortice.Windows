// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpDXGI;
using SharpGen.Runtime;

namespace SharpDirect3D12
{
    public partial class ComputePipelineStateDescription
    {
        public ID3D12RootSignature RootSignature { get; set; }

        public ShaderBytecode ComputeShader { get; set; }

        public int NodeMask { get; set; }
        public CachedPipelineState CachedPSO { get; set; }
        public PipelineStateFlags Flags { get; set; } = PipelineStateFlags.None;

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal struct __Native
        {
            public IntPtr RootSignature;
            public ShaderBytecode.__Native CS;
            public int NodeMask;
            public CachedPipelineState.__Native CachedPSO;
            public PipelineStateFlags Flags;
        }

        internal void __MarshalFree(ref __Native @ref)
        {
            GC.KeepAlive(RootSignature);
            ComputeShader?.__MarshalFree(ref @ref.CS);
            CachedPSO.__MarshalFree(ref @ref.CachedPSO);
        }

        internal void __MarshalFrom(ref __Native @ref)
        {

        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.RootSignature = CppObject.ToCallbackPtr<ID3D12RootSignature>(RootSignature);
            ComputeShader?.__MarshalTo(ref @ref.CS);
            @ref.NodeMask = NodeMask;
            CachedPSO.__MarshalTo(ref @ref.CachedPSO);
            @ref.Flags = Flags;
        }
        #endregion
    }
}
