// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using SharpDXGI;

namespace SharpDirect3D12
{
    /// <summary>
    /// Describes the layout of a root signature version 1.1.
    /// </summary>
    public partial class RootSignatureDescription1
    {
        public RootParameter1[] Parameters { get; set; }
        public StaticSamplerDescription[] StaticSamplers { get; set; }
        public RootSignatureFlags Flags { get; set; }

        #region Marshal
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        internal struct __Native
        {
            public int NumParameters;
            public IntPtr PParameters;
            public int NumStaticSamplers;
            public IntPtr PStaticSamplers;
            public RootSignatureFlags Flags;

            internal void __MarshalFree()
            {
                if (PParameters != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(PParameters);
                }

                if (PStaticSamplers != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(PStaticSamplers);
                }
            }
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            @ref.__MarshalFree();
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            Parameters = new RootParameter1[@ref.NumParameters];
            if (@ref.NumParameters > 0)
            {
                Interop.Read(@ref.PParameters, Parameters);
            }

            StaticSamplers = new StaticSamplerDescription[@ref.NumStaticSamplers];
            if (@ref.NumStaticSamplers > 0)
            {
                Interop.Read(@ref.PStaticSamplers, StaticSamplers);
            }

            Flags = @ref.Flags;
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.NumParameters = Parameters?.Length ?? 0;
            @ref.PParameters = Interop.AllocToPointer(Parameters);
            @ref.NumStaticSamplers = StaticSamplers?.Length ?? 0;
            @ref.PStaticSamplers = Interop.AllocToPointer(StaticSamplers);
            @ref.Flags = Flags;
        }
        #endregion
    }
}
