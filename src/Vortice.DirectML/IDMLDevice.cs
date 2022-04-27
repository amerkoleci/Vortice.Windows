// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Vortice.DirectML;

public partial class IDMLDevice
{
    public IDMLOperator CreateOperator(OperatorDescription operatorDescription)
    {
        CreateOperator(operatorDescription, typeof(IDMLOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLOperator(nativePtr);
    }

    public IDMLCompiledOperator CompileOperator(IDMLOperator @operator, ExecutionFlags executionFlags)
    {
        CompileOperator(@operator, executionFlags, typeof(IDMLCompiledOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCompiledOperator(nativePtr);
    }

    public IDMLOperatorInitializer CreateOperatorInitializer(IDMLCompiledOperator[] operators)
    {
        CreateOperatorInitializer(operators.Length, operators, typeof(IDMLOperatorInitializer).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLOperatorInitializer(nativePtr);
    }

    public IDMLCommandRecorder CreateCommandRecorder()
    {
        CreateCommandRecorder(typeof(IDMLCommandRecorder).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCommandRecorder(nativePtr);
    }

    public IDMLBindingTable CreateBindingTable(in BindingTableDescription description)
    {
        CreateBindingTable(description, typeof(IDMLBindingTable).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLBindingTable(nativePtr);
    }
}
