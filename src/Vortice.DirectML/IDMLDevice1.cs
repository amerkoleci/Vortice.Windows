// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLDevice1
{
    /// <summary>
    /// Compiles a graph of DirectML operators into an object that can be dispatched to the GPU.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice1-compilegraph"/>
    /// </para>
    /// </remarks>
    /// <param name="graphDescription">A description of the graph to compile.</param>
    /// <param name="executionFlags">Any flags to control the execution of this operator.</param>
    /// <returns></returns>
    public IDMLCompiledOperator CompileGraph(GraphDescription graphDescription, ExecutionFlags executionFlags)
    {
        CompileGraph(ref graphDescription, executionFlags, typeof(IDMLCompiledOperator).GUID, out IntPtr nativePtr).CheckError();

        return new IDMLCompiledOperator(nativePtr);
    }
}
