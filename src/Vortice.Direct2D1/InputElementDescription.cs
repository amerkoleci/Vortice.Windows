// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using Vortice.DXGI;

namespace Vortice.Direct2D1;

/// <summary>
/// Describes a single element for the input-assembler stage of the graphics pipeline.
/// </summary>
public partial struct InputElementDescription : IEquatable<InputElementDescription>
{
    /// <unmanaged>D2D1_APPEND_ALIGNED_ELEMENT</unmanaged>
    public const uint AppendAligned = (0xffffffff);

    /// <summary>
    /// Initializes a new instance of the <see cref="InputElementDescription"/> struct.
    /// </summary>
    /// <param name="semanticName">The HLSL semantic associated with this element in a shader input-signature.</param>
    /// <param name="semanticIndex">The semantic index for the element. A semantic index modifies a semantic, with an integer index number. A semantic index is only needed in a case where there is more than one element with the same semantic.</param>
    /// <param name="format">The <see cref="DXGI.Format"/> value that specifies the format of the element data.</param>
    /// <param name="slot">The input-assembler slot.</param>
    /// <param name="offset">Offset, in bytes, between each element. Use <see cref="AppendAligned"/> (0xffffffff) for convenience to define the current element directly after the previous one, including any packing if necessary.</param>
    public InputElementDescription(string semanticName, uint semanticIndex, Format format, uint slot, uint offset)
    {
        SemanticName = semanticName;
        SemanticIndex = semanticIndex;
        Format = format;
        Slot = slot;
        AlignedByteOffset = offset;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InputElementDescription"/> struct.
    /// </summary>
    /// <param name="semanticName">The HLSL semantic associated with this element in a shader input-signature.</param>
    /// <param name="semanticIndex">The semantic index for the element. A semantic index modifies a semantic, with an integer index number. A semantic index is only needed in a case where there is more than one element with the same semantic.</param>
    /// <param name="format">The <see cref="DXGI.Format"/> value that specifies the format of the element data.</param>
    /// <param name="slot">The input-assembler slot.</param>
    public InputElementDescription(string semanticName, uint semanticIndex, Format format, uint slot)
    {
        SemanticName = semanticName;
        SemanticIndex = semanticIndex;
        Format = format;
        Slot = slot;
        AlignedByteOffset = AppendAligned;
    }

    /// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is InputElementDescription value && Equals(value);

    /// <summary>
    /// Determines whether the specified <see cref="InputElementDescription"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="InputElementDescription"/> to compare with this instance.</param>
    public bool Equals(InputElementDescription other)
    {
        return Equals(other.SemanticName, SemanticName)
            && other.SemanticIndex == SemanticIndex
            && other.Format.Equals(Format)
            && other.Slot == Slot
            && other.AlignedByteOffset == AlignedByteOffset;
    }

    /// <summary>
    /// Compares two <see cref="InputElementDescription"/> objects for equality.
    /// </summary>
    /// <param name="left">The <see cref="InputElementDescription"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="InputElementDescription"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is equal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    public static bool operator ==(InputElementDescription left, InputElementDescription right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="InputElementDescription"/> objects for inequality.
    /// </summary>
    /// <param name="left">The <see cref="InputElementDescription"/> on the left hand of the operand.</param>
    /// <param name="right">The <see cref="InputElementDescription"/> on the right hand of the operand.</param>
    /// <returns>
    /// True if the current left is unequal to the <paramref name="right"/> parameter; otherwise, false.
    /// </returns>
    public static bool operator !=(InputElementDescription left, InputElementDescription right) => !left.Equals(right);

    /// <inheritdoc/>
    public override readonly int GetHashCode() => HashCode.Combine(SemanticName, SemanticIndex, Format, Slot, AlignedByteOffset);
}
