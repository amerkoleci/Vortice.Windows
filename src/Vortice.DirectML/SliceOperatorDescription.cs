// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct SliceOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.Slice;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription OutputTensor { get; set; }

    public uint DimensionCount { get; set; }

    public uint[] Offsets { get; set; }

    public uint[] Sizes { get; set; }

    public uint[] Strides { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public uint DimensionCount;
        public IntPtr Offsets;
        public IntPtr Sizes;
        public IntPtr Strides;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->DimensionCount = DimensionCount;
        @ref->Offsets = new(UnsafeUtilities.AllocWithData(Offsets));
        @ref->Sizes = new(UnsafeUtilities.AllocWithData(Sizes));
        @ref->Strides = new(UnsafeUtilities.AllocWithData(Strides));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);
        UnsafeUtilities.Free(@ref->Offsets);
        UnsafeUtilities.Free(@ref->Sizes);
        UnsafeUtilities.Free(@ref->Strides);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(SliceOperatorDescription description)
    {
        return new(description);
    }
}
