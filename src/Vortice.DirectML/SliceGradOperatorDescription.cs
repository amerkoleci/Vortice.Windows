// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct SliceGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.SliceGrad;

    public TensorDescription InputGradientTensor { get; set; }

    public TensorDescription OutputGradientTensor { get; set; }

    public uint DimensionCount { get; set; }

    public uint[] InputWindowOffsets { get; set; }

    public uint[] InputWindowSizes { get; set; }

    public int[] InputWindowStrides { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public uint DimensionCount;
        public IntPtr InputWindowOffsets;
        public IntPtr InputWindowSizes;
        public IntPtr InputWindowStrides;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->DimensionCount = DimensionCount;
        @ref->InputWindowOffsets = new(UnsafeUtilities.AllocWithData(InputWindowOffsets));
        @ref->InputWindowSizes = new(UnsafeUtilities.AllocWithData(InputWindowSizes));
        @ref->InputWindowStrides = new(UnsafeUtilities.AllocWithData(InputWindowStrides));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);
           UnsafeUtilities.Free(@ref->InputWindowOffsets);
           UnsafeUtilities.Free(@ref->InputWindowSizes);
           UnsafeUtilities.Free(@ref->InputWindowStrides);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(SliceGradOperatorDescription description)
    {
        return new(description);
    }
}
