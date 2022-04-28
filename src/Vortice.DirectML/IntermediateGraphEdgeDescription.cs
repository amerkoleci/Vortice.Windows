// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct IntermediateGraphEdgeDescription : IGraphEdgeDescription, IGraphEdgeDescriptionMarshal
{
    public GraphEdgeType GraphEdgeType => GraphEdgeType.Intermediate;

    public int FromNodeIndex { get; set; }

    public int FromNodeOutputIndex { get; set; }

    public int ToNodeIndex { get; set; }

    public int ToNodeInputIndex { get; set; }

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
