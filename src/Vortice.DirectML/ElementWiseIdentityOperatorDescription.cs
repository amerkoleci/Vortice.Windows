﻿// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ElementWiseIdentityOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    public TensorDescription InputTensor { get; }

    public TensorDescription OutputTensor { get; }

    public ScaleBias? ScaleBias { get; }

    public OperatorType OperatorType => OperatorType.ElementWiseIdentity;

    public ElementWiseIdentityOperatorDescription(TensorDescription inputTensor, TensorDescription outputTensor) : this(inputTensor, outputTensor, null) { }

    public ElementWiseIdentityOperatorDescription(TensorDescription inputTensor, TensorDescription outputTensor, ScaleBias? scaleBias)
    {
        InputTensor = inputTensor;
        OutputTensor = outputTensor;
        ScaleBias = scaleBias;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public IntPtr ScaleBias;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = InputTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->ScaleBias = (ScaleBias != null) ? new(UnsafeUtilities.AllocWithData(ScaleBias.Value)) : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputTensor.__MarshalFree(ref @ref->InputTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        if (@ref->ScaleBias != IntPtr.Zero)
        {
            UnsafeUtilities.Free(@ref->ScaleBias);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ElementWiseIdentityOperatorDescription description)
    {
        return new(description);
    }
}
