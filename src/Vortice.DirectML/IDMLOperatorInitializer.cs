// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLOperatorInitializer
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLOperatorInitializer::Reset']/*" />
    public Result Reset(IDMLCompiledOperator[] operators)
    {
        return Reset(operators.Length, operators);
    }
}
