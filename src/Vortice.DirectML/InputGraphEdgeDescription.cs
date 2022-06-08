// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct InputGraphEdgeDescription : IGraphEdgeDescription, IGraphEdgeDescriptionMarshal
{
    /// <summary>
    /// Gets the type of graph edge description.
    /// </summary>
    public GraphEdgeType GraphEdgeType => GraphEdgeType.Input;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INPUT_GRAPH_EDGE_DESC::GraphInputIndex']/*" />
    public int GraphInputIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INPUT_GRAPH_EDGE_DESC::ToNodeIndex']/*" />
    public int ToNodeIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INPUT_GRAPH_EDGE_DESC::ToNodeInputIndex']/*" />
    public int ToNodeInputIndex { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_INPUT_GRAPH_EDGE_DESC::Name']/*" />
    public string? Name { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"Input:{(string.IsNullOrEmpty(Name) ? "" : $" Name={Name}")} ToNode={ToNodeIndex}[{ToNodeInputIndex}]";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public int GraphInputIndex;
        public int ToNodeIndex;
        public int ToNodeInputIndex;
        public IntPtr Name;
    }

    unsafe IntPtr IGraphEdgeDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->GraphInputIndex = GraphInputIndex;
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

    public static implicit operator GraphEdgeDescription(InputGraphEdgeDescription description)
    {
        return new(description);
    }
}
