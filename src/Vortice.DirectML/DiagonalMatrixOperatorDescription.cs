// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct DiagonalMatrixOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.DiagonalMatrix;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DIAGONAL_MATRIX_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DIAGONAL_MATRIX_OPERATOR_DESC::Offset']/*" />
    public int Offset { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DIAGONAL_MATRIX_OPERATOR_DESC::Value']/*" />
    public float Value { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr OutputTensor;
        public int Offset;
        public float Value;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Offset = Offset;
        @ref->Value = Value;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(DiagonalMatrixOperatorDescription description)
    {
        return new(description);
    }
}
