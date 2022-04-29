// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct SliceOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Slice;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_OPERATOR_DESC::Offsets']/*" />
    public int[] Offsets { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_OPERATOR_DESC::Sizes']/*" />
    public int[] Sizes { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SLICE_OPERATOR_DESC::Strides']/*" />
    public int[] Strides { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public int DimensionCount;
        public IntPtr Offsets;
        public IntPtr Sizes;
        public IntPtr Strides;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        var dimensionCount = Offsets.Length;
        if (Sizes.Length != dimensionCount) { throw new IndexOutOfRangeException("Sizes must have the same length as Offsets."); }
        if (Strides.Length != dimensionCount) { throw new IndexOutOfRangeException("Strides must have the same length as Offsets."); }
        @ref->DimensionCount = dimensionCount;

        @ref->Offsets = new(UnsafeUtilities.AllocWithData(Offsets));
        @ref->Sizes = new(UnsafeUtilities.AllocWithData(Sizes));
        @ref->Strides = new(UnsafeUtilities.AllocWithData(Strides));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Offsets);
        UnsafeUtilities.Free(@ref->Sizes);
        UnsafeUtilities.Free(@ref->Strides);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(SliceOperatorDescription description)
    {
        return new(description);
    }
}
