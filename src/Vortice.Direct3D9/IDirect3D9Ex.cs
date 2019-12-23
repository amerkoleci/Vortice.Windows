// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;

namespace Vortice.Direct3D9
{
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
}
