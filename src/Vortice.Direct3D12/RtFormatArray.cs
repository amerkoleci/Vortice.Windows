// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Runtime.CompilerServices;
using Vortice.DXGI;

namespace Vortice.Direct3D12;

internal partial struct RtFormatArray
{
    public Format[] Formats
    {
        get => _RTFormats ??= new Format[D3D12.SimultaneousRenderTargetCount];
        set => _RTFormats = value;
    }

    private Format[] _RTFormats;

    public RtFormatArray(Format[] formats)
    {
        _RTFormats = formats;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Unicode)]
    internal partial struct __Native
    {
        public Format RTFormats;
        public Format __RTFormats1;
        public Format __RTFormats2;
        public Format __RTFormats3;
        public Format __RTFormats4;
        public Format __RTFormats5;
        public Format __RTFormats6;
        public Format __RTFormats7;
        public int NumRenderTargets;
    }

    internal unsafe void __MarshalTo(ref __Native @ref)
    {
        if (Formats.Length > 0)
        {
            @ref.NumRenderTargets = Math.Min(Formats.Length, D3D12.SimultaneousRenderTargetCount);
            fixed (void* renderTargetFormatsPtr = &Formats[0])
            {
                MemoryHelpers.CopyMemory(
                    (IntPtr)Unsafe.AsPointer(ref @ref.RTFormats),
                    (IntPtr)renderTargetFormatsPtr,
                    @ref.NumRenderTargets * sizeof(Format));
            }
        }
        else
        {
            @ref.NumRenderTargets = 0;
        }
    }
}
