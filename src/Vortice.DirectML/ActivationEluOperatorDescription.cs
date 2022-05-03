// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct ActivationEluOperatorDescription : IFusableActivationOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.ActivationElu;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ACTIVATION_ELU_OPERATOR_DESC::InputTensor']/*" />
    public TensorDescription? InputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ACTIVATION_ELU_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription? OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_ACTIVATION_ELU_OPERATOR_DESC::Alpha']/*" />
    public float Alpha { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public float Alpha;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputTensor = (InputTensor != null) ? InputTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->OutputTensor = (OutputTensor != null) ? OutputTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->Alpha = Alpha;

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

    public static implicit operator OperatorDescription(ActivationEluOperatorDescription description)
    {
        return new(description);
    }
}
