// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

/// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC']/*" />
public partial struct QuantizedLinearConvolutionOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.QuantizedLinearConvolution;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::InputScaleTensor']/*" />
    public TensorDescription InputScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::InputZeroPointTensor']/*" />
    public TensorDescription? InputZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::FilterTensor']/*" />
    public TensorDescription FilterTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::FilterScaleTensor']/*" />
    public TensorDescription FilterScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::FilterZeroPointTensor']/*" />
    public TensorDescription? FilterZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::BiasTensor']/*" />
    public TensorDescription? BiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::OutputScaleTensor']/*" />
    public TensorDescription OutputScaleTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::OutputZeroPointTensor']/*" />
    public TensorDescription? OutputZeroPointTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::Strides']/*" />
    public int[] Strides { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::Dilations']/*" />
    public int[] Dilations { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::StartPadding']/*" />
    public int[] StartPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::EndPadding']/*" />
    public int[] EndPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_QUANTIZED_LINEAR_CONVOLUTION_OPERATOR_DESC::GroupCount']/*" />
    public int GroupCount { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputScaleTensor;
        public IntPtr InputZeroPointTensor;
        public IntPtr FilterTensor;
        public IntPtr FilterScaleTensor;
        public IntPtr FilterZeroPointTensor;
        public IntPtr BiasTensor;
        public IntPtr OutputScaleTensor;
        public IntPtr OutputZeroPointTensor;
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
        @ref->InputScaleTensor = InputScaleTensor.__MarshalAlloc();
        @ref->InputZeroPointTensor = (InputZeroPointTensor != null) ? InputZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->FilterTensor = FilterTensor.__MarshalAlloc();
        @ref->FilterScaleTensor = FilterScaleTensor.__MarshalAlloc();
        @ref->FilterZeroPointTensor = (FilterZeroPointTensor != null) ? FilterZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->BiasTensor = (BiasTensor != null) ? BiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputScaleTensor = OutputScaleTensor.__MarshalAlloc();
        @ref->OutputZeroPointTensor = (OutputZeroPointTensor != null) ? OutputZeroPointTensor.Value.__MarshalAlloc() : IntPtr.Zero;
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
        InputScaleTensor.__MarshalFree(ref @ref->InputScaleTensor);

        if (InputZeroPointTensor != null)
        {
            InputZeroPointTensor.Value.__MarshalFree(ref @ref->InputZeroPointTensor);
        }

        FilterTensor.__MarshalFree(ref @ref->FilterTensor);
        FilterScaleTensor.__MarshalFree(ref @ref->FilterScaleTensor);

        if (FilterZeroPointTensor != null)
        {
            FilterZeroPointTensor.Value.__MarshalFree(ref @ref->FilterZeroPointTensor);
        }

        if (BiasTensor != null)
        {
            BiasTensor.Value.__MarshalFree(ref @ref->BiasTensor);
        }

        OutputScaleTensor.__MarshalFree(ref @ref->OutputScaleTensor);

        if (OutputZeroPointTensor != null)
        {
            OutputZeroPointTensor.Value.__MarshalFree(ref @ref->OutputZeroPointTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Strides);
        UnsafeUtilities.Free(@ref->Dilations);
        UnsafeUtilities.Free(@ref->StartPadding);
        UnsafeUtilities.Free(@ref->EndPadding);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(QuantizedLinearConvolutionOperatorDescription description)
    {
        return new(description);
    }
}
