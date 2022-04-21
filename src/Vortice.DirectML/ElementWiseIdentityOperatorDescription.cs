// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;
public partial class ElementWiseIdentityOperatorDescription : OperatorDescription
{
    public TensorDescription InputTensor { get; }
    public TensorDescription OutputTensor { get; }
    public ScaleBias? ScaleBias { get; }

    public ElementWiseIdentityOperatorDescription(TensorDescription inputTensor, TensorDescription outputTensor) : this(inputTensor, outputTensor, null) { }

    public ElementWiseIdentityOperatorDescription(TensorDescription inputTensor, TensorDescription outputTensor, ScaleBias? scaleBias)
    {
        InputTensor = inputTensor;
        OutputTensor = outputTensor;
        ScaleBias = scaleBias;
    }

    #region Marshal
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct __ElementWiseIdentityOperatorDescriptionNative
    {
        public IntPtr InputTensor;
        public IntPtr OutputTensor;
        public IntPtr ScaleBias;
    }

    internal unsafe override void __MarshalFree(ref __Native @ref)
    {
        var operatorDesc = (__ElementWiseIdentityOperatorDescriptionNative*)@ref.Description;
        InputTensor.__MarshalFree(ref *(TensorDescription.__Native*)(*operatorDesc).InputTensor);
        OutputTensor.__MarshalFree(ref *(TensorDescription.__Native*)(*operatorDesc).OutputTensor);

        UnsafeUtilities.Free((*operatorDesc).InputTensor);
        UnsafeUtilities.Free(operatorDesc);
    }

    internal unsafe override void __MarshalTo(ref __Native @ref)
    {
        var tensorDescs = UnsafeUtilities.Alloc<TensorDescription.__Native>(2);
        InputTensor.__MarshalTo(ref tensorDescs[0]);
        OutputTensor.__MarshalTo(ref tensorDescs[1]);

        var operatorDesc = UnsafeUtilities.Alloc<__ElementWiseIdentityOperatorDescriptionNative>();
        (*operatorDesc).InputTensor = new IntPtr(&tensorDescs[0]);
        (*operatorDesc).OutputTensor = new IntPtr(&tensorDescs[1]);
        (*operatorDesc).ScaleBias = IntPtr.Zero;

        @ref.Type = OperatorType.ElementWiseIdentity;
        @ref.Description = new IntPtr(operatorDesc);
    }
    #endregion
}
