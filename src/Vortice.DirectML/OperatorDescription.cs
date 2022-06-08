// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct OperatorDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_OPERATOR_DESC::Desc']/*" />
    public IOperatorDescription Description { get; set; }

    public OperatorDescription(IOperatorDescription description)
    {
        Description = description;
    }

    public override string ToString() => $"{Description} as {nameof(OperatorDescription)}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public OperatorType Type;
        public IntPtr Description;
    }

    internal unsafe IntPtr __MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->Description = ((IOperatorDescriptionMarshal)Description).__MarshalAlloc();

        return new(@ref);
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Description.OperatorType;
        @ref.Description = ((IOperatorDescriptionMarshal)Description).__MarshalAlloc();
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((IOperatorDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal unsafe void __MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        ((IOperatorDescriptionMarshal)Description).__MarshalFree(ref @ref->Description);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
