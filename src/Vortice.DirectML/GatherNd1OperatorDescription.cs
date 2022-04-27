// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct GatherNd1OperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.GatherNd1;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription IndicesTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public uint InputDimensionCount { get; set; }

    public uint IndicesDimensionCount { get; set; }

    public uint BatchDimensionCount { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr IndicesTensor;
        public IntPtr OutputTensor;
        public uint InputDimensionCount;
        public uint IndicesDimensionCount;
        public uint BatchDimensionCount;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->IndicesTensor = IndicesTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->InputDimensionCount = InputDimensionCount;
        @ref->IndicesDimensionCount = IndicesDimensionCount;
        @ref->BatchDimensionCount = BatchDimensionCount;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        IndicesTensor.__MarshalFree(ref @ref->IndicesTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(GatherNd1OperatorDescription description)
    {
        return new(description);
    }
}
