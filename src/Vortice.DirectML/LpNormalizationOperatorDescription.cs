// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct LpNormalizationOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.LpNormalization;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public uint Axis { get; set; }

    public float Epsilon { get; set; }

    public uint P { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public uint Axis;
        public float Epsilon;
        public uint P;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Axis = Axis;
        @ref->Epsilon = Epsilon;
        @ref->P = P;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(LpNormalizationOperatorDescription description)
    {
        return new(description);
    }
}
