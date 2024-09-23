// Copyright © Aaron Sun, Amer Koleci, and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DirectML;

public partial class IDMLBindingTable
{
    /// <summary>
    /// Binds a set of resources as input tensors.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmlbindingtable-bindinputs"/>
    /// </para>
    /// </remarks>
    /// <param name="descriptions">
    /// A constant array of DML_BINDING_DESC containing descriptions of the tensor resources to bind.
    /// </param>
    public void BindInputs(params BindingDescription[]? descriptions)
    {
        BindInputs((uint)(descriptions?.Length ?? 0), descriptions);
    }

    /// <summary>
    /// Binds a set of resources as output tensors.
    /// </summary>
    /// <remarks>
    /// <para>
    /// See Microsoft Docs:
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/directml/nf-directml-idmldevice-createoperatorinitializer"/>
    /// </para>
    /// </remarks>
    /// <param name="descriptions">
    /// A constant array of DML_BINDING_DESC containing descriptions of the tensor resources to bind.
    /// </param>
    public void BindOutputs(params BindingDescription[]? descriptions)
    {
        BindOutputs((uint)(descriptions?.Length ?? 0), descriptions);
    }
}
