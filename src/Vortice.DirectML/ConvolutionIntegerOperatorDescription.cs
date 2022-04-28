// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ConvolutionIntegerOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.ConvolutionInteger;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription? InputZeroPointTensor { get; set; }

    public TensorDescription FilterTensor { get; set; }

    public TensorDescription? FilterZeroPointTensor { get; set; }

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

    public static implicit operator OperatorDescription(ConvolutionIntegerOperatorDescription description)
    {
        return new(description);
    }
}
