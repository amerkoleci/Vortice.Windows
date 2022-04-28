// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct IntermediateGraphEdgeDescription : IGraphEdgeDescription, IGraphEdgeDescriptionMarshal
{
    /// <summary>
    /// Gets the type of graph edge description.
    /// </summary>
    public GraphEdgeType GraphEdgeType => GraphEdgeType.Intermediate;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INTERMEDIATE_GRAPH_EDGE_DESC::FromNodeIndex']/*" />
    public int FromNodeIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INTERMEDIATE_GRAPH_EDGE_DESC::FromNodeOutputIndex']/*" />
    public int FromNodeOutputIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INTERMEDIATE_GRAPH_EDGE_DESC::ToNodeIndex']/*" />
    public int ToNodeIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INTERMEDIATE_GRAPH_EDGE_DESC::ToNodeInputIndex']/*" />
    public int ToNodeInputIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INTERMEDIATE_GRAPH_EDGE_DESC::Name']/*" />
    public string? Name { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public int FromNodeIndex;
        public int FromNodeOutputIndex;
        public int ToNodeIndex;
        public int ToNodeInputIndex;
        public IntPtr Name;
    }

    unsafe IntPtr IGraphEdgeDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->FromNodeIndex = FromNodeIndex;
        @ref->FromNodeOutputIndex = FromNodeOutputIndex;
        @ref->ToNodeIndex = ToNodeIndex;
        @ref->ToNodeInputIndex = ToNodeInputIndex;
        @ref->Name = string.IsNullOrEmpty(Name) ? IntPtr.Zero : Marshal.StringToHGlobalAnsi(Name);

        return new(@ref);
    }

    unsafe void IGraphEdgeDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        if (@ref->Name != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(@ref->Name);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator GraphEdgeDescription(IntermediateGraphEdgeDescription description)
    {
        return new(description);
    }
}
