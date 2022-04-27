// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseDequantizeLinearOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.ElementWiseDequantizeLinear;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription ScaleTensor { get; set; }

    public TensorDescription ZeroPointTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr ScaleTensor;
        public IntPtr ZeroPointTensor;
        public IntPtr OutputTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->ScaleTensor = ScaleTensor.__MarshalAlloc();
        @ref->ZeroPointTensor = ZeroPointTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        ScaleTensor.__MarshalFree(ref @ref->ScaleTensor);
        ZeroPointTensor.__MarshalFree(ref @ref->ZeroPointTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ElementWiseDequantizeLinearOperatorDescription description)
    {
        return new(description);
    }
}
