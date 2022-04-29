// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct PaddingOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Padding;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_PADDING_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_PADDING_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_PADDING_OPERATOR_DESC::PaddingMode']/*" />
    public PaddingMode PaddingMode { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_PADDING_OPERATOR_DESC::PaddingValue']/*" />
    public float PaddingValue { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_PADDING_OPERATOR_DESC::StartPadding']/*" />
    public int[] StartPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_PADDING_OPERATOR_DESC::EndPadding']/*" />
    public int[] EndPadding { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public PaddingMode PaddingMode;
        public float PaddingValue;
        public int DimensionCount;
        public IntPtr StartPadding;
        public IntPtr EndPadding;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->PaddingMode = PaddingMode;
        @ref->PaddingValue = PaddingValue;

        var dimensionCount = StartPadding.Length;
        if (EndPadding.Length != dimensionCount) { throw new IndexOutOfRangeException("EndPadding must have the same length as StartPadding."); }
        @ref->DimensionCount = dimensionCount;

        @ref->StartPadding = new(UnsafeUtilities.AllocWithData(StartPadding));
        @ref->EndPadding = new(UnsafeUtilities.AllocWithData(EndPadding));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->StartPadding);
        UnsafeUtilities.Free(@ref->EndPadding);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(PaddingOperatorDescription description)
    {
        return new(description);
    }
}
