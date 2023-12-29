// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

/// <summary>
/// Describes a JPEG AC huffman table.
/// </summary>
public unsafe partial struct JpegDcHuffmanTable
{
    /// <summary>
    /// The number of codes for each code length.
    /// </summary>
    public fixed byte CodeCounts[16];
    /// <summary>
    /// The Huffman code values, in order of increasing code length.
    /// </summary>
    public fixed byte CodeValues[162];
}
