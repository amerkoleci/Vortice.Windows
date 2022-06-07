// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ConvolutionIntegerOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ConvolutionInteger;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::InputZeroPointTensor']/*" />
    public TensorDescription? InputZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::FilterTensor']/*" />
    public TensorDescription FilterTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::FilterZeroPointTensor']/*" />
    public TensorDescription? FilterZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::Strides']/*" />
    public int[] Strides { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::Dilations']/*" />
    public int[] Dilations { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::StartPadding']/*" />
    public int[] StartPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::EndPadding']/*" />
    public int[] EndPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_INTEGER_OPERATOR_DESC::GroupCount']/*" />
    public int GroupCount { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputZeroPointTensor;
        public IntPtr FilterTensor;
        public IntPtr FilterZeroPointTensor;
        public IntPtr OutputTensor;
        public int DimensionCount;
        public IntPtr Strides;
        public IntPtr Dilations;
        public IntPtr StartPadding;
        public IntPtr EndPadding;
        public int GroupCount;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->InputZeroPointTensor = (InputZeroPointTensor != null) ? InputZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->FilterTensor = FilterTensor.__MarshalAlloc();
        @ref->FilterZeroPointTensor = (FilterZeroPointTensor != null) ? FilterZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        var dimensionCount = Strides.Length;
        if (Dilations.Length != dimensionCount) { throw new IndexOutOfRangeException("Dilations must have the same length as Strides."); }
        if (StartPadding.Length != dimensionCount) { throw new IndexOutOfRangeException("StartPadding must have the same length as Strides."); }
        if (EndPadding.Length != dimensionCount) { throw new IndexOutOfRangeException("EndPadding must have the same length as Strides."); }
        @ref->DimensionCount = dimensionCount;

        @ref->Strides = new(UnsafeUtilities.AllocWithData(Strides));
        @ref->Dilations = new(UnsafeUtilities.AllocWithData(Dilations));
        @ref->StartPadding = new(UnsafeUtilities.AllocWithData(StartPadding));
        @ref->EndPadding = new(UnsafeUtilities.AllocWithData(EndPadding));
        @ref->GroupCount = GroupCount;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);

        if (InputZeroPointTensor != null)
        {
            InputZeroPointTensor.Value.__MarshalFree(ref @ref->InputZeroPointTensor);
        }

        FilterTensor.__MarshalFree(ref @ref->FilterTensor);

        if (FilterZeroPointTensor != null)
        {
            FilterZeroPointTensor.Value.__MarshalFree(ref @ref->FilterZeroPointTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Strides);
        UnsafeUtilities.Free(@ref->Dilations);
        UnsafeUtilities.Free(@ref->StartPadding);
        UnsafeUtilities.Free(@ref->EndPadding);

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
