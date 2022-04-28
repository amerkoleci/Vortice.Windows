// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct OperatorGraphNodeDescription : IGraphNodeDescription, IGraphNodeDescriptionMarshal
{
    public GraphNodeType GraphNodeType => GraphNodeType.Operator;

    public IDMLOperator Operator { get; set; }

    public string? Name { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr Operator;
        public IntPtr Name;
    }

    unsafe IntPtr IGraphNodeDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->Operator = Operator.NativePointer;
        @ref->Name = string.IsNullOrEmpty(Name) ? IntPtr.Zero : Marshal.StringToHGlobalAnsi(Name);

        return new(@ref);
    }

    unsafe void IGraphNodeDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        if (@ref->Name != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(@ref->Name);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator GraphNodeDescription(OperatorGraphNodeDescription description)
    {
        return new(description);
    }
}
