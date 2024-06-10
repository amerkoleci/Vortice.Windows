// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public unsafe partial class IDXGIOutput5
{
    public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, Format[] supportedFormats)
    {
        fixed (Format* pSupportedFormats = supportedFormats)
        {
            return DuplicateOutput1_(device, 0, supportedFormats.Length, pSupportedFormats);
        }
    }

    public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, int supportedFormatsCount, Format[] supportedFormats)
    {
        fixed (Format* pSupportedFormats = supportedFormats)
        {
            return DuplicateOutput1_(device, 0, supportedFormatsCount, pSupportedFormats);
        }
    }

    public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, Span<Format> supportedFormats)
    {
        fixed (Format* pSupportedFormats = supportedFormats)
        {
            return DuplicateOutput1_(device, 0, supportedFormats.Length, pSupportedFormats);
        }
    }

    public IDXGIOutputDuplication DuplicateOutput1(IUnknown device, int supportedFormatsCount, Span<Format> supportedFormats)
    {
        fixed (Format* pSupportedFormats = supportedFormats)
        {
            return DuplicateOutput1_(device, 0, supportedFormatsCount, pSupportedFormats);
        }
    }
}
