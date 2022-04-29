// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ActivationReluOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ActivationRelu;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ACTIVATION_RELU_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription? InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ACTIVATION_RELU_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription? OutputTensor { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = (InputTensor != null) ? InputTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = (OutputTensor != null) ? OutputTensor.Value.__MarshalAlloc() : IntPtr.Zero;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        if (InputTensor != null)
        {
            InputTensor.Value.__MarshalFree(ref @ref->InputTensor);
        }

        if (OutputTensor != null)
        {
            OutputTensor.Value.__MarshalFree(ref @ref->OutputTensor);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(ActivationReluOperatorDescription description)
    {
        return new(description);
    }
}
