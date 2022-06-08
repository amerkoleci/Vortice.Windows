// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct GraphDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_DESC::InputCount']/*" />
    public int InputCount { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_DESC::OutputCount']/*" />
    public int OutputCount { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_DESC::Nodes']/*" />
    public OperatorGraphNodeDescription[] Nodes { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_DESC::InputEdges']/*" />
    public InputGraphEdgeDescription[]? InputEdges { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_DESC::OutputEdges']/*" />
    public OutputGraphEdgeDescription[] OutputEdges { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_GRAPH_DESC::IntermediateEdges']/*" />
    public IntermediateGraphEdgeDescription[]? IntermediateEdges { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"GraphDescription: Inputs={InputCount} Outputs={OutputCount} Nodes={Nodes.Length} InputEdges={InputEdges?.Length ?? 0} OutputEdges={OutputEdges.Length} IntermediateEdges={IntermediateEdges?.Length ?? 0}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public int InputCount;
        public int OutputCount;
        public int NodeCount;
        public IntPtr Nodes;
        public int InputEdgeCount;
        public IntPtr InputEdges;
        public int OutputEdgeCount;
        public IntPtr OutputEdges;
        public int IntermediateEdgeCount;
        public IntPtr IntermediateEdges;
    }

    internal unsafe void __MarshalFree(ref __Native @ref)
    {
        if (@ref.Nodes != IntPtr.Zero)
        {
            var nodes = (GraphNodeDescription.__Native*)@ref.Nodes;
            for (int i = 0; i < Nodes.Length; i++)
            {
                ((GraphNodeDescription)Nodes[i]).__MarshalFree(ref nodes[i]);
            }
            UnsafeUtilities.Free(@ref.Nodes);
        }

        if (@ref.InputEdges != IntPtr.Zero)
        {
            var inputEdges = (GraphEdgeDescription.__Native*)@ref.InputEdges;
            for (int i = 0; i < InputEdges!.Length; i++)
            {
                ((GraphEdgeDescription)InputEdges[i]).__MarshalFree(ref inputEdges[i]);
            }
            UnsafeUtilities.Free(@ref.InputEdges);
        }

        if (@ref.OutputEdges != IntPtr.Zero)
        {
            var outputEdges = (GraphEdgeDescription.__Native*)@ref.OutputEdges;
            for (int i = 0; i < OutputEdges.Length; i++)
            {
                ((GraphEdgeDescription)OutputEdges[i]).__MarshalFree(ref outputEdges[i]);
            }
            UnsafeUtilities.Free(@ref.OutputEdges);
        }

        if (@ref.IntermediateEdges != IntPtr.Zero)
        {
            var intermediateEdges = (GraphEdgeDescription.__Native*)@ref.IntermediateEdges;
            for (int i = 0; i < IntermediateEdges!.Length; i++)
            {
                ((GraphEdgeDescription)IntermediateEdges[i]).__MarshalFree(ref intermediateEdges[i]);
            }
            UnsafeUtilities.Free(@ref.IntermediateEdges);
        }
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        @ref.InputCount = InputCount;
        @ref.OutputCount = OutputCount;

        @ref.NodeCount = Nodes.Length;
        @ref.Nodes = IntPtr.Zero;
        @ref.InputEdgeCount = InputEdges?.Length ?? 0;
        @ref.InputEdges = IntPtr.Zero;
        @ref.OutputEdgeCount = OutputEdges.Length;
        @ref.OutputEdges = IntPtr.Zero;
        @ref.IntermediateEdgeCount = IntermediateEdges?.Length ?? 0;
        @ref.IntermediateEdges = IntPtr.Zero;

        if (Nodes.Length != 0)
        {
            var nodes = UnsafeUtilities.Alloc<GraphNodeDescription.__Native>(Nodes.Length);
            for (int i = 0; i < Nodes.Length; i++)
            {
                ((GraphNodeDescription)Nodes[i]).__MarshalTo(ref nodes[i]);
            }
            @ref.Nodes = new(nodes);
        }

        if (InputEdges != null && InputEdges.Length != 0)
        {
            var inputEdges = UnsafeUtilities.Alloc<GraphEdgeDescription.__Native>(InputEdges.Length);
            for (int i = 0; i < InputEdges.Length; i++)
            {
                ((GraphEdgeDescription)InputEdges[i]).__MarshalTo(ref inputEdges[i]);
            }
            @ref.InputEdges = new(inputEdges);
        }

        if (OutputEdges.Length != 0)
        {
            var outputEdges = UnsafeUtilities.Alloc<GraphEdgeDescription.__Native>(OutputEdges.Length);
            for (int i = 0; i < OutputEdges.Length; i++)
            {
                ((GraphEdgeDescription)OutputEdges[i]).__MarshalTo(ref outputEdges[i]);
            }
            @ref.OutputEdges = new(outputEdges);
        }

        if (IntermediateEdges != null && IntermediateEdges.Length != 0)
        {
            var intermediateEdges = UnsafeUtilities.Alloc<GraphEdgeDescription.__Native>(IntermediateEdges.Length);
            for (int i = 0; i < IntermediateEdges.Length; i++)
            {
                ((GraphEdgeDescription)IntermediateEdges[i]).__MarshalTo(ref intermediateEdges[i]);
            }
            @ref.IntermediateEdges = new(intermediateEdges);
        }
    }

    #endregion
}
