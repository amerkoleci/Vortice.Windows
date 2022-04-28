// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLBindingTable
{
    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLBindingTable::BindInputs']/*" />
    public void BindInputs(params BindingDescription[] descriptions)
    {
        BindInputs(descriptions.Length, descriptions);
    }

    /// <include file="Documentation.xml" path="/comments/comment[@id='IDMLBindingTable::BindOutputs']/*" />
    public void BindOutputs(params BindingDescription[] descriptions)
    {
        BindOutputs(descriptions.Length, descriptions);
    }
}
