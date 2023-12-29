// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public partial class IDirect3D9Ex
{
    public IDirect3DDevice9Ex CreateDeviceEx(int adapter, DeviceType deviceType, IntPtr focusWindow, CreateFlags createFlags, PresentParameters presentationParameters)
    {
        return CreateDeviceEx(adapter, deviceType, focusWindow, (int)createFlags, new[] { presentationParameters }, null);
    }

    public IDirect3DDevice9Ex CreateDeviceEx(int adapter, DeviceType deviceType, IntPtr focusWindow, CreateFlags createFlags, PresentParameters presentationParameters, DisplayModeEx fullScreenDisplayMode)
    {
        return CreateDeviceEx(adapter, deviceType, focusWindow, (int)createFlags, new[] { presentationParameters }, new[] { fullScreenDisplayMode });
    }

    public IDirect3DDevice9Ex CreateDeviceEx(int adapter, DeviceType deviceType, IntPtr focusWindow, CreateFlags createFlags, params PresentParameters[] presentationParameters)
    {
        return CreateDeviceEx(adapter, deviceType, focusWindow, (int)createFlags, presentationParameters, null);
    }

    public IDirect3DDevice9Ex CreateDeviceEx(int adapter, DeviceType deviceType, IntPtr focusWindow, CreateFlags createFlags, PresentParameters[] presentationParameters, DisplayModeEx[] fullScreenDisplayModes)
    {
        return CreateDeviceEx(adapter, deviceType, focusWindow, (int)createFlags, presentationParameters, fullScreenDisplayModes);
    }
}
