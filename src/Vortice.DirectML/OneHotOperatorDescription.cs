// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct OneHotOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.OneHot;

    public TensorDescription IndicesTensor { get; set; }

    public TensorDescription ValuesTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public int Axis { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr IndicesTensor;
        public IntPtr ValuesTensor;
        public IntPtr OutputTensor;
        public int Axis;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->IndicesTensor = IndicesTensor.__MarshalAlloc();
        @ref->ValuesTensor = ValuesTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Axis = Axis;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        IndicesTensor.__MarshalFree(ref @ref->IndicesTensor);
        ValuesTensor.__MarshalFree(ref @ref->ValuesTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(OneHotOperatorDescription description)
    {
        return new(description);
    }
}
