// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

/// <summary>
/// Describes a JPEG quantization table.
/// </summary>
public unsafe partial struct JpegQuantizationTable
{
    /// <summary>
    /// An array of bytes containing the elements of the quantization table.
    /// </summary>
    public fixed byte Elements[64];
}
