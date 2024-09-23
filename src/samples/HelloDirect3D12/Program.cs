// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System.Numerics;
using System.Runtime.InteropServices;
using SharpGen.Runtime;
using Vortice;

namespace HelloDirect3D12;

public static class Program
{
    private class TestApplication : Application
    {
        protected override void InitializeBeforeRun()
        {
            var validation = false;
#if DEBUG
            validation = true;
#endif

            _graphicsDevice = new D3D12GraphicsDevice(validation, MainWindow!);
        }

        protected override void OnKeyboardEvent(KeyboardKey key, bool pressed)
        {
            if (key == KeyboardKey.Space && pressed)
            {
                ((D3D12GraphicsDevice)_graphicsDevice).UseRenderPass = !((D3D12GraphicsDevice)_graphicsDevice).UseRenderPass;
            }
        }
    }

    public static void Main()
    {
#if DEBUG
        Configuration.EnableObjectTracking = true;
#endif
        using TestApplication app = new();
        app.Run();
    }

    public static Vortice.DXGI.IDXGIAdapter1[] GetAdapters(Vortice.DXGI.IDXGIFactory5 factory)
    {
        var result = new List<Vortice.DXGI.IDXGIAdapter1>();
        for (int index = 0; factory.EnumAdapters1(index, out var adapter).Success; index++)
        {
            result.Add(adapter);
        }

        return result.ToArray();
    }
}
