// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutput1
{
    public ModeDescription1[] GetDisplayModeList1(Format format, DisplayModeEnumerationFlags flags)
    {
        uint count = 0;
        GetDisplayModeList1(format, (uint)flags, ref count, null);
        var result = new ModeDescription1[count];
        if (count > 0)
        {
            GetDisplayModeList1(format, (uint)flags, ref count, result);
        }

        return result;
    }
}
