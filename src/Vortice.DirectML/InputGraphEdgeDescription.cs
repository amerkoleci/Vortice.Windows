// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct InputGraphEdgeDescription : IGraphEdgeDescription, IGraphEdgeDescriptionMarshal
{
    public GraphEdgeType GraphEdgeType => GraphEdgeType.Input;

    public int GraphInputIndex { get; set; }

    public int ToNodeIndex { get; set; }

    public int ToNodeInputIndex { get; set; }

    public string? Name { get; set; }

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
