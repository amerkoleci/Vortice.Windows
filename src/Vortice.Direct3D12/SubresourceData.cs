// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D12;

public unsafe partial struct SubresourceData
{
    public void* pData;

    public nint RowPitch;

    public nint SlicePitch;

    /// <summary>
    /// Initializes a new instance of the <see cref="SubresourceData"/> struct.
    /// </summary>
    /// <param name="dataPointer">The dataPointer.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="slicePitch">The slice pitch.</param>
    public SubresourceData(IntPtr dataPointer, nint rowPitch = 0, nint slicePitch = 0)
    {
        pData = dataPointer.ToPointer();
        RowPitch = rowPitch;
        SlicePitch = slicePitch;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SubresourceData"/> struct.
    /// </summary>
    /// <param name="dataPointer">The dataPointer.</param>
    /// <param name="rowPitch">The row pitch.</param>
    /// <param name="slicePitch">The slice pitch.</param>
    public SubresourceData(void* dataPointer, nint rowPitch = 0, nint slicePitch = 0)
    {
        pData = dataPointer;
        RowPitch = rowPitch;
        SlicePitch = slicePitch;
    }
}

