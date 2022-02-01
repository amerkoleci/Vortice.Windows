// Copyright © Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutput
{
    public ModeDescription[] GetDisplayModeList(Format format, DisplayModeEnumerationFlags flags)
    {
        int count = 0;
        GetDisplayModeList(format, (int)flags, ref count, null);
        var result = new ModeDescription[count];
        if (count > 0)
        {
            GetDisplayModeList(format, (int)flags, ref count, result);
        }
        return result;
    }
}
