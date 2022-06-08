// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct GraphEdgeDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_EDGE_DESC::Desc']/*" />
    public IGraphEdgeDescription Description { get; set; }

    public GraphEdgeDescription(IGraphEdgeDescription description)
    {
        Description = description;
    }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"{Description} as GraphEdgeDescription";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public GraphEdgeType Type;
        public IntPtr Description;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((IGraphEdgeDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Description.GraphEdgeType;
        @ref.Description = ((IGraphEdgeDescriptionMarshal)Description).__MarshalAlloc();
    }
    #endregion
}
