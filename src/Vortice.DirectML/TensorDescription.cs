// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct TensorDescription
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_TENSOR_DESC::Desc']/*" />
    public ITensorDescription Description { get; set; }

    public TensorDescription(ITensorDescription description)
    {
        Description = description;
    }

    public override readonly string ToString() => $"{Description} as {nameof(TensorDescription)}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public TensorType Type;
        public IntPtr Description;
    }

    internal unsafe IntPtr __MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->Type = Description.TensorType;
        @ref->Description = ((ITensorDescriptionMarshal)Description).__MarshalAlloc();

        return new(@ref);
    }

    internal void __MarshalTo(ref __Native @ref)
    {
        @ref.Type = Description.TensorType;
        @ref.Description = ((ITensorDescriptionMarshal)Description).__MarshalAlloc();
    }

    internal void __MarshalFree(ref __Native @ref)
    {
        ((ITensorDescriptionMarshal)Description).__MarshalFree(ref @ref.Description);
        @ref.Description = IntPtr.Zero;
    }

    internal unsafe void __MarshalFree(ref IntPtr pDesc)
    {
        __Native* @ref = (__Native*)pDesc;

        ((ITensorDescriptionMarshal)Description).__MarshalFree(ref @ref->Description);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
