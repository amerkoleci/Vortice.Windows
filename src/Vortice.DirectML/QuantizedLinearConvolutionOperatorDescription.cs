// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct QuantizedLinearConvolutionOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.QuantizedLinearConvolution;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription InputScaleTensor { get; set; }

    public TensorDescription? InputZeroPointTensor { get; set; }

    public TensorDescription FilterTensor { get; set; }

    public TensorDescription FilterScaleTensor { get; set; }

    public TensorDescription? FilterZeroPointTensor { get; set; }

    public TensorDescription? BiasTensor { get; set; }

    public TensorDescription OutputScaleTensor { get; set; }

    public TensorDescription? OutputZeroPointTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public int DimensionCount { get; set; }

    public int[] Strides { get; set; }

    public int[] Dilations { get; set; }

    public int[] StartPadding { get; set; }

    public int[] EndPadding { get; set; }

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
        @ref->DimensionCount = DimensionCount;
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
