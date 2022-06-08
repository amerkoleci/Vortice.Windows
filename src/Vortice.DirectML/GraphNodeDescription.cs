// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct GraphNodeDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_NODE_DESC::Desc']/*" />
    public IGraphNodeDescription Description { get; set; }

    public GraphNodeDescription(IGraphNodeDescription description)
    {
        Description = description;
    }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"{Description} as GraphNodeDescription";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public GraphNodeType Type;
        public IntPtr Description;
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((IGraphNodeDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Description.GraphNodeType;
        @ref.Description = ((IGraphNodeDescriptionMarshal)Description).__MarshalAlloc();
    }
    #endregion
}
