// Copyright (c) Amer Koleci and contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.DXGI;

public partial class IDXGIOutput
{
    public ModeDescription[] GetDisplayModeList(Format format, DisplayModeEnumerationFlags flags)
    {
        uint count = 0;
        GetDisplayModeList(format, (uint)flags, ref count, null);
        var result = new ModeDescription[count];
        if (count > 0)
        {
            GetDisplayModeList(format, (uint)flags, ref count, result);
        }
        return result;
    }
}
