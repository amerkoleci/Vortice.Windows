// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Windows;
using System.Windows.Interop;
using Vortice.Direct3D9;
using static Vortice.Direct3D9.D3D9;

namespace Vortice.Wpf;

internal static class D3D9DeviceService
{
    private static int _activeClients;
    private static IDirect3D9Ex? _d3dContext;
    private static IDirect3DDevice9Ex? _device;

    public static IDirect3DDevice9Ex? D3DDevice => _device!;

    public static void Start(Window parentWindow)
    {
        _activeClients++;

        if (_activeClients > 1)
            return;

        _d3dContext = Direct3DCreate9Ex();

        PresentParameters presentParameters = new()
        {
            Windowed = true,
            SwapEffect = SwapEffect.Discard,
            DeviceWindowHandle = new WindowInteropHelper(parentWindow).Handle,
            PresentationInterval = PresentInterval.Default
        };

        _device = _d3dContext.CreateDeviceEx(0, DeviceType.Hardware, IntPtr.Zero,
            CreateFlags.HardwareVertexProcessing | CreateFlags.Multithreaded | CreateFlags.FpuPreserve,
            presentParameters);
    }

    public static void End()
    {
        _activeClients--;
        if (_activeClients < 0)
            throw new InvalidOperationException();

        if (_activeClients != 0)
            return;

        if(_device != null)
        {
            _device.Dispose();
            _device = default;
        }

        if (_d3dContext != null)
        {
            _d3dContext.Dispose();
            _d3dContext = default;
        }
    }
}
