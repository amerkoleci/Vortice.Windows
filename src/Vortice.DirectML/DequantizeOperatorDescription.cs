// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct DequantizeOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Dequantize;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEQUANTIZE_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEQUANTIZE_OPERATOR_DESC::QuantizationType']/*" />
    public QuantizationType QuantizationType { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEQUANTIZE_OPERATOR_DESC::QuantizationTensors']/*" />
    public TensorDescription[] QuantizationTensors { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_DEQUANTIZE_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"Dequantize: QuantizationType={QuantizationType}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public QuantizationType QuantizationType;
        public uint QuantizationTensorCount;
        public IntPtr QuantizationTensors;
        public IntPtr OutputTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->QuantizationType = QuantizationType;
        @ref->QuantizationTensorCount = (uint)QuantizationTensors.Length;

        @ref->QuantizationTensors = IntPtr.Zero;
        if (QuantizationTensors.Length != 0)
        {
            var quantizationTensorsPtr = UnsafeUtilities.Alloc<TensorDescription.__Native>(QuantizationTensors.Length);
            for (int i = 0; i < QuantizationTensors.Length; i++)
            {
                QuantizationTensors[i].__MarshalTo(ref quantizationTensorsPtr[i]);
            }
            @ref->QuantizationTensors = new(quantizationTensorsPtr);
        }

        @ref->OutputTensor = OutputTensor.__MarshalAlloc();

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);

        if (@ref->QuantizationTensors != IntPtr.Zero)
        {
            var quantizationTensorsPtr = (TensorDescription.__Native*)@ref->QuantizationTensors;
            for (int i = 0; i < QuantizationTensors.Length; i++)
            {
                QuantizationTensors[i].__MarshalFree(ref quantizationTensorsPtr[i]);
            }
            UnsafeUtilities.Free(@ref->QuantizationTensors);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(DequantizeOperatorDescription description)
    {
        return new(description);
    }
}
