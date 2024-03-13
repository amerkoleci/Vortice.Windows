// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using static Vortice.UnsafeUtilities;

namespace Vortice.Direct3D12;

/// <summary>
/// Describes the arguments (parameters) of a command signature.
/// </summary>
public partial class CommandSignatureDescription
{
    public CommandSignatureDescription(int byteStride, IndirectArgumentDescription[] indirectArguments, uint nodeMask = 0)
    {
        ByteStride = byteStride;
        IndirectArguments = indirectArguments;
        NodeMask = nodeMask;
    }

    public CommandSignatureDescription(params IndirectArgumentDescription[] indirectArguments)
    {
        IndirectArguments = indirectArguments;
    }

    public int ByteStride { get; set; }

    /// <summary>	
    /// An array of <see cref="InputElementDescription"/> that describe the command signature.
    /// </summary>	
    public IndirectArgumentDescription[] IndirectArguments { get; set; }

    public uint NodeMask { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal unsafe struct __Native
    {
        public int ByteStride;
        public int NumArgumentDescs;
        public IndirectArgumentDescription* pArgumentDescs;
        public uint NodeMask;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.pArgumentDescs != null)
        {
            Free(@ref.pArgumentDescs);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.ByteStride = ByteStride;
        if (IndirectArguments?.Length > 0)
        {
            @ref.NumArgumentDescs = IndirectArguments.Length;
            @ref.pArgumentDescs = AllocWithData(IndirectArguments);
        }
        else
        {
            @ref.NumArgumentDescs = 0;
            @ref.pArgumentDescs = null;
        }
        @ref.NodeMask = NodeMask;
    }
    #endregion
}
