// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct NonzeroCoordinatesOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.NonzeroCoordinates;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription OutputCountTensor { get; set; }

    public TensorDescription OutputCoordinatesTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputCountTensor;
        public IntPtr OutputCoordinatesTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputCountTensor = OutputCountTensor.__MarshalAlloc();
        @ref->OutputCoordinatesTensor = OutputCoordinatesTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputCountTensor.__MarshalFree(ref @ref->OutputCountTensor);
        OutputCoordinatesTensor.__MarshalFree(ref @ref->OutputCoordinatesTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(NonzeroCoordinatesOperatorDescription description)
    {
        return new(description);
    }
}
