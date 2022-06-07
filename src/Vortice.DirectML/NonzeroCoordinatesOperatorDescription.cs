// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct NonzeroCoordinatesOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.NonzeroCoordinates;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_NONZERO_COORDINATES_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_NONZERO_COORDINATES_OPERATOR_DESC::OutputCountTensor']/*" />
    public TensorDescription OutputCountTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_NONZERO_COORDINATES_OPERATOR_DESC::OutputCoordinatesTensor']/*" />
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
