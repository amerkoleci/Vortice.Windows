// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct RoiAlign1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.RoiAlign1;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription RoiTensor { get; set; }

    public TensorDescription BatchIndicesTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public ReduceFunction ReductionFunction { get; set; }

    public InterpolationMode InterpolationMode { get; set; }

    public float SpatialScaleX { get; set; }

    public float SpatialScaleY { get; set; }

    public float InputPixelOffset { get; set; }

    public float OutputPixelOffset { get; set; }

    public float OutOfBoundsInputValue { get; set; }

    public int MinimumSamplesPerOutput { get; set; }

    public int MaximumSamplesPerOutput { get; set; }

    public bool AlignRegionsToCorners { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr RoiTensor;
        public IntPtr BatchIndicesTensor;
        public IntPtr OutputTensor;
        public ReduceFunction ReductionFunction;
        public InterpolationMode InterpolationMode;
        public float SpatialScaleX;
        public float SpatialScaleY;
        public float InputPixelOffset;
        public float OutputPixelOffset;
        public float OutOfBoundsInputValue;
        public int MinimumSamplesPerOutput;
        public int MaximumSamplesPerOutput;
        public bool AlignRegionsToCorners;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->RoiTensor = RoiTensor.__MarshalAlloc();
        @ref->BatchIndicesTensor = BatchIndicesTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ReductionFunction = ReductionFunction;
        @ref->InterpolationMode = InterpolationMode;
        @ref->SpatialScaleX = SpatialScaleX;
        @ref->SpatialScaleY = SpatialScaleY;
        @ref->InputPixelOffset = InputPixelOffset;
        @ref->OutputPixelOffset = OutputPixelOffset;
        @ref->OutOfBoundsInputValue = OutOfBoundsInputValue;
        @ref->MinimumSamplesPerOutput = MinimumSamplesPerOutput;
        @ref->MaximumSamplesPerOutput = MaximumSamplesPerOutput;
        @ref->AlignRegionsToCorners = AlignRegionsToCorners;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        RoiTensor.__MarshalFree(ref @ref->RoiTensor);
        BatchIndicesTensor.__MarshalFree(ref @ref->BatchIndicesTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(RoiAlign1OperatorDescription description)
    {
        return new(description);
    }
}
