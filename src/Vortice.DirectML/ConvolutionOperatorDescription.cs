// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ConvolutionOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Convolution;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::FilterTensor']/*" />
    public TensorDescription FilterTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::BiasTensor']/*" />
    public TensorDescription? BiasTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::Mode']/*" />
    public ConvolutionMode Mode { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::Direction']/*" />
    public ConvolutionDirection Direction { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::Strides']/*" />
    public int[] Strides { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::Dilations']/*" />
    public int[] Dilations { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::StartPadding']/*" />
    public int[] StartPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::EndPadding']/*" />
    public int[] EndPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::OutputPadding']/*" />
    public int[] OutputPadding { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::GroupCount']/*" />
    public int GroupCount { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_CONVOLUTION_OPERATOR_DESC::FusedActivation']/*" />
    public OperatorDescription? FusedActivation { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr FilterTensor;
        public IntPtr BiasTensor;
        public IntPtr OutputTensor;
        public ConvolutionMode Mode;
        public ConvolutionDirection Direction;
        public int DimensionCount;
        public IntPtr Strides;
        public IntPtr Dilations;
        public IntPtr StartPadding;
        public IntPtr EndPadding;
        public IntPtr OutputPadding;
        public int GroupCount;
        public IntPtr FusedActivation;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->FilterTensor = FilterTensor.__MarshalAlloc();
        @ref->BiasTensor = (BiasTensor != null) ? BiasTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Mode = Mode;
        @ref->Direction = Direction;

        var dimensionCount = Strides.Length;
        if (Dilations.Length != dimensionCount) { throw new IndexOutOfRangeException("Dilations must have the same length as Strides."); }
        if (StartPadding.Length != dimensionCount) { throw new IndexOutOfRangeException("StartPadding must have the same length as Strides."); }
        if (EndPadding.Length != dimensionCount) { throw new IndexOutOfRangeException("EndPadding must have the same length as Strides."); }
        if (OutputPadding.Length != dimensionCount) { throw new IndexOutOfRangeException("OutputPadding must have the same length as Strides."); }
        @ref->DimensionCount = dimensionCount;

        @ref->Strides = new(UnsafeUtilities.AllocWithData(Strides));
        @ref->Dilations = new(UnsafeUtilities.AllocWithData(Dilations));
        @ref->StartPadding = new(UnsafeUtilities.AllocWithData(StartPadding));
        @ref->EndPadding = new(UnsafeUtilities.AllocWithData(EndPadding));
        @ref->OutputPadding = new(UnsafeUtilities.AllocWithData(OutputPadding));
        @ref->GroupCount = GroupCount;
        @ref->FusedActivation = (FusedActivation != null) ? FusedActivation.Value.__MarshalAlloc() : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        FilterTensor.__MarshalFree(ref @ref->FilterTensor);

        if (BiasTensor != null)
        {
            BiasTensor.Value.__MarshalFree(ref @ref->BiasTensor);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Strides);
        UnsafeUtilities.Free(@ref->Dilations);
        UnsafeUtilities.Free(@ref->StartPadding);
        UnsafeUtilities.Free(@ref->EndPadding);
        UnsafeUtilities.Free(@ref->OutputPadding);

        if (FusedActivation != null)
        {
            FusedActivation.Value.__MarshalFree(ref @ref->FusedActivation);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ConvolutionOperatorDescription description)
    {
        return new(description);
    }
}
