// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct LocalResponseNormalizationGradOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public OperatorType OperatorType => OperatorType.LocalResponseNormalizationGrad;

    public TensorDescription InputTensor { get; set; }

    public TensorDescription InputGradientTensor { get; set; }

    public TensorDescription OutputGradientTensor { get; set; }

    public bool CrossChannel { get; set; }

    public uint LocalSize { get; set; }

    public float Alpha { get; set; }

    public float Beta { get; set; }

    public float Bias { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr InputGradientTensor;
        public IntPtr OutputGradientTensor;
        public bool CrossChannel;
        public uint LocalSize;
        public float Alpha;
        public float Beta;
        public float Bias;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->InputGradientTensor = InputGradientTensor.__MarshalAlloc();
        @ref->OutputGradientTensor = OutputGradientTensor.__MarshalAlloc();
        @ref->CrossChannel = CrossChannel;
        @ref->LocalSize = LocalSize;
        @ref->Alpha = Alpha;
        @ref->Beta = Beta;
        @ref->Bias = Bias;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        InputGradientTensor.__MarshalFree(ref @ref->InputGradientTensor);
        OutputGradientTensor.__MarshalFree(ref @ref->OutputGradientTensor);

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(LocalResponseNormalizationGradOperatorDescription description)
    {
        return new(description);
    }
}
