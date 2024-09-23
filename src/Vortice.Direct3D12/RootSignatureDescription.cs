// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the layout of a root signature version 1.0.
/// </summary>
public partial class RootSignatureDescription
{
    public RootParameter[]? Parameters { get; set; }
    public StaticSamplerDescription[]? StaticSamplers { get; set; }
    public RootSignatureFlags Flags { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootSignatureDescription"/> class.
    /// </summary>
    public RootSignatureDescription()
        : this(RootSignatureFlags.None)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootSignatureDescription"/> class.
    /// </summary>
    /// <param name="flags">The flags.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="samplers">The samplers.</param>
    public RootSignatureDescription(RootSignatureFlags flags, RootParameter[]? parameters = default, StaticSamplerDescription[]? samplers = default)
    {
        Parameters = parameters;
        StaticSamplers = samplers;
        Flags = flags;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public int NumParameters;
        public RootParameter* pParameters;
        public int NumStaticSamplers;
        public StaticSamplerDescription* pStaticSamplers;
        public RootSignatureFlags Flags;

        internal void __MarshalFree()
        {
            if (pParameters != null)
            {
                NativeMemory.Free(pParameters);
            }

            if (pStaticSamplers != null)
            {
                NativeMemory.Free(pStaticSamplers);
            }
        }
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        @ref.__MarshalFree();
    }

    internal unsafe void __MarshalFrom(ref __Native @ref)
    {
        Parameters = new RootParameter[@ref.NumParameters];
        if (@ref.NumParameters > 0)
        {
            UnsafeUtilities.Read(@ref.pParameters, Parameters);
        }

        StaticSamplers = new StaticSamplerDescription[@ref.NumStaticSamplers];
        if (@ref.NumStaticSamplers > 0)
        {
            UnsafeUtilities.Read(@ref.pStaticSamplers, StaticSamplers);
        }

        Flags = @ref.Flags;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.NumParameters = Parameters?.Length ?? 0;
        @ref.pParameters = UnsafeUtilities.AllocToPointer(Parameters);
        @ref.NumStaticSamplers = StaticSamplers?.Length ?? 0;
        @ref.pStaticSamplers = UnsafeUtilities.AllocToPointer(StaticSamplers);
        @ref.Flags = Flags;
    }
    #endregion
}
