// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct PaddingOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.Padding;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public PaddingMode PaddingMode { get; set; }

    public float PaddingValue { get; set; }

    public int DimensionCount { get; set; }

    public int[] StartPadding { get; set; }

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
        @ref->DimensionCount = DimensionCount;
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
