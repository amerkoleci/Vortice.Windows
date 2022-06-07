// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial struct RandomGeneratorOperatorDescription : IOperatorDescription, IOperatorDescriptionMarshal
{
    /// <summary>
    /// Gets the type of operator description.
    /// </summary>
    public OperatorType OperatorType => OperatorType.RandomGenerator;

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RANDOM_GENERATOR_OPERATOR_DESC::InputStateTensor']/*" />
    public TensorDescription InputStateTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RANDOM_GENERATOR_OPERATOR_DESC::OutputTensor']/*" />
    public TensorDescription OutputTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RANDOM_GENERATOR_OPERATOR_DESC::OutputStateTensor']/*" />
    public TensorDescription? OutputStateTensor { get; set; }

    /// <include file="Documentation.xml" path="/comments/comment[@id='DML_RANDOM_GENERATOR_OPERATOR_DESC::Type']/*" />
    public RandomGeneratorType Type { get; set; }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __Native
    {
        public IntPtr InputStateTensor;
        public IntPtr OutputTensor;
        public IntPtr OutputStateTensor;
        public RandomGeneratorType Type;
    }

    unsafe IntPtr IOperatorDescriptionMarshal.__MarshalAlloc()
    {
        __Native* @ref = UnsafeUtilities.Alloc<__Native>();

        @ref->InputStateTensor = InputStateTensor.__MarshalAlloc();
        @ref->OutputTensor = OutputTensor.__MarshalAlloc();
        @ref->OutputStateTensor = (OutputStateTensor != null) ? OutputStateTensor.Value.__MarshalAlloc() : IntPtr.Zero;
        @ref->Type = Type;

        return new(@ref);
    }

    unsafe void IOperatorDescriptionMarshal.__MarshalFree(ref IntPtr pDesc)
    {
        var @ref = (__Native*)pDesc;

        InputStateTensor.__MarshalFree(ref @ref->InputStateTensor);
        OutputTensor.__MarshalFree(ref @ref->OutputTensor);

        if (OutputStateTensor != null)
        {
            OutputStateTensor.Value.__MarshalFree(ref @ref->OutputStateTensor);
        }

        UnsafeUtilities.Free(@ref);
    }
    #endregion

    public static implicit operator OperatorDescription(RandomGeneratorOperatorDescription description)
    {
        return new(description);
    }
}
