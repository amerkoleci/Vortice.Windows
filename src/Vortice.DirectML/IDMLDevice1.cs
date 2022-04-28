// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLDevice1
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLDevice1::CompileGraph']/*" />
    public IDMLCompiledOperator CompileGraph(GraphDescription graphDescription, ExecutionFlags executionFlags)
    {
        CompileGraph(ref graphDescription, executionFlags, typeof(IDMLCompiledOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCompiledOperator(nativePtr);
    }
}
