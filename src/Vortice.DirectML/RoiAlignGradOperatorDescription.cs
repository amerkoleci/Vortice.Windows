// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct RoiAlignGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.RoiAlignGrad;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription? InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::InputGradientTensor']/*" />
    public TensorDescription InputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::ROITensor']/*" />
    public TensorDescription RoiTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::BatchIndicesTensor']/*" />
    public TensorDescription BatchIndicesTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::OutputGradientTensor']/*" />
    public TensorDescription? OutputGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::OutputROIGradientTensor']/*" />
    public TensorDescription? OutputROIGradientTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::ReductionFunction']/*" />
    public ReduceFunction ReductionFunction { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::InterpolationMode']/*" />
    public InterpolationMode InterpolationMode { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::SpatialScaleX']/*" />
    public float SpatialScaleX { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::SpatialScaleY']/*" />
    public float SpatialScaleY { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::InputPixelOffset']/*" />
    public float InputPixelOffset { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::OutputPixelOffset']/*" />
    public float OutputPixelOffset { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::MinimumSamplesPerOutput']/*" />
    public int MinimumSamplesPerOutput { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::MaximumSamplesPerOutput']/*" />
    public int MaximumSamplesPerOutput { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ROI_ALIGN_GRAD_OPERATOR_DESC::AlignRegionsToCorners']/*" />
    public bool AlignRegionsToCorners { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputGradientTensor;
        public IntPtr RoiTensor;
        public IntPtr BatchIndicesTensor;
        public IntPtr OutputGradientTensor;
        public IntPtr OutputROIGradientTensor;
        public ReduceFunction ReductionFunction;
        public InterpolationMode InterpolationMode;
        public float SpatialScaleX;
        public float SpatialScaleY;
        public float InputPixelOffset;
        public float OutputPixelOffset;
        public int MinimumSamplesPerOutput;
        public int MaximumSamplesPerOutput;
        public bool AlignRegionsToCorners;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = (InputTensor != null) ? InputTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->RoiTensor = RoiTensor.__MarshalAlloc();
        @ref->BatchIndicesTensor = BatchIndicesTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = (OutputGradientTensor != null) ? OutputGradientTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputROIGradientTensor = (OutputROIGradientTensor != null) ? OutputROIGradientTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->ReductionFunction = ReductionFunction;
        @ref->InterpolationMode = InterpolationMode;
        @ref->SpatialScaleX = SpatialScaleX;
        @ref->SpatialScaleY = SpatialScaleY;
        @ref->InputPixelOffset = InputPixelOffset;
        @ref->OutputPixelOffset = OutputPixelOffset;
        @ref->MinimumSamplesPerOutput = MinimumSamplesPerOutput;
        @ref->MaximumSamplesPerOutput = MaximumSamplesPerOutput;
        @ref->AlignRegionsToCorners = AlignRegionsToCorners;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        if (InputTensor != null)
        {
            InputTensor.Value.__MarshalFree(ref @ref->InputTensor);
        }

        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        RoiTensor.__MarshalFree(ref @ref->RoiTensor);
        BatchIndicesTensor.__MarshalFree(ref @ref->BatchIndicesTensor);

        if (OutputGradientTensor != null)
        {
            OutputGradientTensor.Value.__MarshalFree(ref @ref->OutputGradientTensor);
        }

        if (OutputROIGradientTensor != null)
        {
            OutputROIGradientTensor.Value.__MarshalFree(ref @ref->OutputROIGradientTensor);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(RoiAlignGradOperatorDescription description)
    {
        return new(description);
    }
}
