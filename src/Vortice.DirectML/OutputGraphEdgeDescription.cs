// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct OutputGraphEdgeDescription : IGraphEdgeDescription, IGraphEdgeDescriptionMarshal
{
    public GraphEdgeType GraphEdgeType => GraphEdgeType.Output;

    public int FromNodeIndex { get; set; }

    public int FromNodeOutputIndex { get; set; }

    public int GraphOutputIndex { get; set; }

    public string? Name { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public int FromNodeIndex;
        public int FromNodeOutputIndex;
        public int GraphOutputIndex;
        public IntPtr Name;
    }

    unsafe IntPtr IGraphEdgeDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->FromNodeIndex = FromNodeIndex;
        @ref->FromNodeOutputIndex = FromNodeOutputIndex;
        @ref->GraphOutputIndex = GraphOutputIndex;
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

    public static implicit operator GraphEdgeDescription(OutputGraphEdgeDescription description)
    {
        return new(description);
    }
}
