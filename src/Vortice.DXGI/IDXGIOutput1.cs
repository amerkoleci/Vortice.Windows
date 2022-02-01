// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutput1
{
    public ModeDescription1[] GetDisplayModeList1(Format format, DisplayModeEnumerationFlags flags)
    {
        int count = 0;
        GetDisplayModeList1(format, (int)flags, ref count, null);
        var result = new ModeDescription1[count];
        if (count > 0)
        {
            GetDisplayModeList1(format, (int)flags, ref count, result);
        }

        return result;
    }
}
