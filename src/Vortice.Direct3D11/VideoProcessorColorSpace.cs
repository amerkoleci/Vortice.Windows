// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;

namespace Vortice.Direct3D11;

/// <summary>
/// Specifies the color space for video processing.
/// </summary>
public partial struct VideoProcessorColorSpace
{
    private uint _bitfield;

    public uint Usage
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _bitfield & 0x1u;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _bitfield = (_bitfield & ~0x1u) | (value & 0x1u);
        }
    }

    public uint RGB_Range
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return (_bitfield >> 1) & 0x1u;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _bitfield = (_bitfield & ~(0x1u << 1)) | ((value & 0x1u) << 1);
        }
    }

    public uint YCbCr_Matrix
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return (_bitfield >> 2) & 0x1u;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _bitfield = (_bitfield & ~(0x1u << 2)) | ((value & 0x1u) << 2);
        }
    }

    public uint YCbCr_xvYCC
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return (_bitfield >> 3) & 0x1u;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _bitfield = (_bitfield & ~(0x1u << 3)) | ((value & 0x1u) << 3);
        }
    }

    public uint Nominal_Range
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return (_bitfield >> 4) & 0x3u;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _bitfield = (_bitfield & ~(0x3u << 4)) | ((value & 0x3u) << 4);
        }
    }

    public uint Reserved
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return (_bitfield >> 6) & 0x3FFFFFFu;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            _bitfield = (_bitfield & ~(0x3FFFFFFu << 6)) | ((value & 0x3FFFFFFu) << 6);
        }
    }
}
