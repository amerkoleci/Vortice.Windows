// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct SplitOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.Split;

    public TensorDescription InputTensor { get; set; }

    public uint OutputCount { get; set; }

    public TensorDescription OutputTensors { get; set; }

    public uint Axis { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public uint OutputCount;
        public IntPtr OutputTensors;
        public uint Axis;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputCount = OutputCount;
        @ref->OutputTensors = OutputTensors.__MarshalAlloc();
        @ref->Axis = Axis;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensors.__MarshalFree(ref @ref->OutputTensors);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(SplitOperatorDescription description)
    {
        return new(description);
    }
}
