// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct LpPoolingOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.LpPooling;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public int DimensionCount { get; set; }

    public int[] Strides { get; set; }

    public int[] WindowSize { get; set; }

    public int[] StartPadding { get; set; }

    public int[] EndPadding { get; set; }

    public int P { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public int DimensionCount;
        public IntPtr Strides;
        public IntPtr WindowSize;
        public IntPtr StartPadding;
        public IntPtr EndPadding;
        public int P;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->DimensionCount = DimensionCount;
        @ref->Strides = new(UnsafeUtilities.AllocWithData(Strides));
        @ref->WindowSize = new(UnsafeUtilities.AllocWithData(WindowSize));
        @ref->StartPadding = new(UnsafeUtilities.AllocWithData(StartPadding));
        @ref->EndPadding = new(UnsafeUtilities.AllocWithData(EndPadding));
        @ref->P = P;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Strides);
        UnsafeUtilities.Free(@ref->WindowSize);
        UnsafeUtilities.Free(@ref->StartPadding);
        UnsafeUtilities.Free(@ref->EndPadding);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(LpPoolingOperatorDescription description)
    {
        return new(description);
    }
}
