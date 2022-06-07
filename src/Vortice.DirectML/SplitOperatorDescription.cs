// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct SplitOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.Split;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SPLIT_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SPLIT_OPERATOR_DESC::OutputTensors']/*" />
    public TensorDescription[] OutputTensors { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_SPLIT_OPERATOR_DESC::Axis']/*" />
    public int Axis { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public int OutputCount;
        public IntPtr OutputTensors;
        public int Axis;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputCount = OutputTensors.Length;

        @ref->OutputTensors = IntPtr.Zero;
        if (OutputTensors.Length != 0)
        {
            var outputTensorsPtr = UnsafeUtilities.Alloc<TensorDescription.__Native>(OutputTensors.Length);
            for (int i = 0; i < OutputTensors.Length; i++)
            {
                OutputTensors[i].__MarshalTo(ref outputTensorsPtr[i]);
            }
            @ref->OutputTensors = new(outputTensorsPtr);
        }

        @ref->Axis = Axis;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);

        if (@ref->OutputTensors != IntPtr.Zero)
        {
            var outputTensorsPtr = (TensorDescription.__Native*)@ref->OutputTensors;
            for (int i = 0; i < OutputTensors.Length; i++)
            {
                OutputTensors[i].__MarshalFree(ref outputTensorsPtr[i]);
            }
            UnsafeUtilities.Free(@ref->OutputTensors);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion
}
