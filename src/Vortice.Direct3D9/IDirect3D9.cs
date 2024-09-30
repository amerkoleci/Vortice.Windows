// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace Vortice.Direct3D9;

public partial class IDirect3D9
{
    public IDirect3DDevice9 CreateDevice(uint adapter, DeviceType deviceType, IntPtr focusWindow, CreateFlags createFlags, PresentParameters presentationParameters)
    {
        return CreateDevice(adapter, deviceType, focusWindow, (int)createFlags, ref presentationParameters);
    }

    /// <summary>
    /// Get the physical display adapters present in the system when this <see cref="IDirect3D9"/> was instantiated.
    /// </summary>
    /// <param name="adapter">The adapter.</param>
    /// <returns>The physical display adapters</returns>
    public AdapterIdentifier GetAdapterIdentifier(uint adapter)
    {
        return GetAdapterIdentifier(adapter, 0);
    }
}
