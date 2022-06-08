// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct JoinOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Join;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_JOIN_OPERATOR_DESC::InputTensors']/*" />
    public TensorDescription[] InputTensors { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_JOIN_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_JOIN_OPERATOR_DESC::Axis']/*" />
    public int Axis { get; set; }

    /// <inheritdoc></inheritdoc>/>
    public override string ToString() => $"Join: Axis={Axis}";

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public int InputCount;
        public IntPtr InputTensors;
        public IntPtr OutputTensor;
        public int Axis;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputCount = InputTensors.Length;

        @ref->InputTensors = IntPtr.Zero;
        if (InputTensors.Length != 0)
        {
            var inputTensorsPtr = UnsafeUtilities.Alloc<TensorDescription.__Native>(InputTensors.Length);
            for (int i = 0; i < InputTensors.Length; i++)
            {
                InputTensors[i].__MarshalTo(ref inputTensorsPtr[i]);
            }
            @ref->InputTensors = new(inputTensorsPtr);
        }

        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->Axis = Axis;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;


        if (@ref->InputTensors != IntPtr.Zero)
        {
            var inputTensorsPtr = (TensorDescription.__Native*)@ref->InputTensors;
            for (int i = 0; i < InputTensors.Length; i++)
            {
                InputTensors[i].__MarshalFree(ref inputTensorsPtr[i]);
            }
            UnsafeUtilities.Free(@ref->InputTensors);
        }

        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(JoinOperatorDescription description)
    {
        return new(description);
    }
}
