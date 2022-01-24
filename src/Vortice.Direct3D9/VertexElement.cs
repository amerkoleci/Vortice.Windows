// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

/// <summary>
/// Defines the vertex data layout. Each vertex can contain one or more data types, and each data type is described by a vertex element.
/// </summary>
public partial struct VertexElement
{
    /// <summary>
    /// Used for closing a VertexElement declaration.
    /// </summary>
    public static readonly VertexElement VertexDeclarationEnd = new(0xff, 0, DeclarationType.Unused, DeclarationMethod.Default, DeclarationUsage.Position, 0);

    /// <summary>
    /// Initializes a new instance of the <see cref="VertexElement"/> struct.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="type">The type.</param>
    /// <param name="method">The method.</param>
    /// <param name="usage">The usage.</param>
    /// <param name="usageIndex">Index of the usage.</param>
    public VertexElement(short stream, short offset, DeclarationType type, DeclarationMethod method, DeclarationUsage usage, byte usageIndex = 0)
    {
        Stream = stream;
        Offset = offset;
        Type = type;
        Method = method;
        Usage = usage;
        UsageIndex = usageIndex;
    }
}
