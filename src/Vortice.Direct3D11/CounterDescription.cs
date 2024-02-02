// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D11;

/// <summary>
/// Describes a counter.
/// </summary>
public partial struct CounterDescription
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CounterDescription"/> struct.
    /// </summary>
    /// <param name="counterKind">Type of query (see <see cref="CounterKind"/>).</param>
    public CounterDescription(CounterKind counterKind)
    {
        CounterKind = counterKind;
        MiscFlags = 0;
    }
}
