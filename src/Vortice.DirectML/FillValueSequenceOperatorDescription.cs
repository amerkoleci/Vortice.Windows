// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct FillValueSequenceOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.FillValueSequence;

    public TensorDescription OutputTensor { get; set; }

    public TensorDataType ValueDataType { get; set; }

    public ScalarUnion ValueStart { get; set; }

    public ScalarUnion ValueDelta { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr OutputTensor;
        public TensorDataType ValueDataType;
        public ScalarUnion ValueStart;
        public ScalarUnion ValueDelta;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ValueDataType = ValueDataType;
        @ref->ValueStart = ValueStart;
        @ref->ValueDelta = ValueDelta;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(FillValueSequenceOperatorDescription description)
    {
        return new(description);
    }
}
