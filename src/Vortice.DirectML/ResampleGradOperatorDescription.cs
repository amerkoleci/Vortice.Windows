// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ResampleGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.ResampleGrad;

    public TensorDescription InputGradientTensor { get; set; }

    public TensorDescription OutputGradientTensor { get; set; }

    public InterpolationMode InterpolationMode { get; set; }

    public int DimensionCount { get; set; }

    public float[] Scales { get; set; }

    public float[] InputPixelOffsets { get; set; }

    public float[] OutputPixelOffsets { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public InterpolationMode InterpolationMode;
        public int DimensionCount;
        public IntPtr Scales;
        public IntPtr InputPixelOffsets;
        public IntPtr OutputPixelOffsets;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->InterpolationMode = InterpolationMode;
        @ref->DimensionCount = DimensionCount;
        @ref->Scales = new(UnsafeUtilities.AllocWithData(Scales));
        @ref->InputPixelOffsets = new(UnsafeUtilities.AllocWithData(InputPixelOffsets));
        @ref->OutputPixelOffsets = new(UnsafeUtilities.AllocWithData(OutputPixelOffsets));

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);
        UnsafeUtilities.Free(@ref->Scales);
        UnsafeUtilities.Free(@ref->InputPixelOffsets);
        UnsafeUtilities.Free(@ref->OutputPixelOffsets);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ResampleGradOperatorDescription description)
    {
        return new(description);
    }
}
