// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLDevice
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLDevice::CreateOperator']/*" />
    public IDMLOperator CreateOperator(OperatorDescription operatorDescription)
    {
        CreateOperator(ref operatorDescription, typeof(IDMLOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLOperator(nativePtr);
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLDevice::CompileOperator']/*" />
    public IDMLCompiledOperator CompileOperator(IDMLOperator @operator, ExecutionFlags executionFlags)
    {
        CompileOperator(@operator, executionFlags, typeof(IDMLCompiledOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCompiledOperator(nativePtr);
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLDevice::CreateOperatorInitializer']/*" />
    public IDMLOperatorInitializer CreateOperatorInitializer(IDMLCompiledOperator[] operators)
    {
        CreateOperatorInitializer(operators.Length, operators, typeof(IDMLOperatorInitializer).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLOperatorInitializer(nativePtr);
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLDevice::CreateCommandRecorder']/*" />
    public IDMLCommandRecorder CreateCommandRecorder()
    {
        CreateCommandRecorder(typeof(IDMLCommandRecorder).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCommandRecorder(nativePtr);
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLDevice::CreateBindingTable']/*" />
    public IDMLBindingTable CreateBindingTable(in BindingTableDescription description)
    {
        CreateBindingTable(description, typeof(IDMLBindingTable).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLBindingTable(nativePtr);
    }
}
