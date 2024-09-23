// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLOperatorInitializer
{
    /// <summary>
    /// Resets the initializer to handle initialization of a new set of operators.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmloperatorinitializer-reset"/>
    /// </para>
    /// </remarks>
    /// <param name="operators"></param>
    /// <returns></returns>
    public Result Reset(params IDMLCompiledOperator[]? operators)
    {
        return Reset((uint)(operators?.Length ?? 0), operators);
    }
}
