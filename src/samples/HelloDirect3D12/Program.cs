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

        using (var queue = Vortice.DXGI.DXGI.DXGIGetDebugInterface1<Vortice.DXGI.Debug.IDXGIInfoQueue>())
        {
            queue?.SetBreakOnSeverity(Vortice.DXGI.DXGI.DebugAll, Vortice.DXGI.Debug.InfoQueueMessageSeverity.Corruption, true);
            queue?.SetBreakOnSeverity(Vortice.DXGI.DXGI.DebugAll, Vortice.DXGI.Debug.InfoQueueMessageSeverity.Error, true);
            queue?.SetBreakOnSeverity(Vortice.DXGI.DXGI.DebugAll, Vortice.DXGI.Debug.InfoQueueMessageSeverity.Warning, true);
        }

        using (var debug = Vortice.Direct3D12.D3D12.D3D12GetDebugInterface<Vortice.Direct3D12.Debug.ID3D12Debug>())
        {
            debug?.EnableDebugLayer();
        }

        using (var factory = Vortice.DXGI.DXGI.CreateDXGIFactory2<Vortice.DXGI.IDXGIFactory5>(true))
        {
            var adapters = GetAdapters(factory);

            var adapter = adapters
                .Where(a => !a.Description1.Flags.HasFlag(Vortice.DXGI.AdapterFlags.Software))
                .MaxBy(a => a.Description1.DedicatedVideoMemory)
                ;

            using (var device = Vortice.Direct3D12.D3D12.D3D12CreateDevice<Vortice.Direct3D12.ID3D12Device5>(adapter, Vortice.Direct3D.FeatureLevel.Level_12_0))
            using (var infoQueue = device.QueryInterfaceOrNull<Vortice.Direct3D12.Debug.ID3D12InfoQueue>())
            {
                infoQueue?.SetBreakOnSeverity(Vortice.Direct3D12.Debug.MessageSeverity.Corruption, true);
                infoQueue?.SetBreakOnSeverity(Vortice.Direct3D12.Debug.MessageSeverity.Error, true);
                infoQueue?.SetBreakOnSeverity(Vortice.Direct3D12.Debug.MessageSeverity.Warning, true);

                infoQueue?.PushStorageFilter(new Vortice.Direct3D12.Debug.InfoQueueFilter
                {
                    AllowList = new Vortice.Direct3D12.Debug.InfoQueueFilterDescription
                    {
                    },
                    DenyList = new Vortice.Direct3D12.Debug.InfoQueueFilterDescription
                    {
                        Ids = new[] { Vortice.Direct3D12.Debug.MessageId.ClearRenderTargetViewMismatchingClearValue, Vortice.Direct3D12.Debug.MessageId.CreateResourceStateIgnored },
                        Severities = new[] { Vortice.Direct3D12.Debug.MessageSeverity.Info }
                    }
                });

                var parameters = new List<Vortice.Direct3D12.RootParameter1>();
                parameters.Add(new Vortice.Direct3D12.RootParameter1(new Vortice.Direct3D12.RootConstants(0, 0, Marshal.SizeOf<Material>() / 4), Vortice.Direct3D12.ShaderVisibility.All));

                var flags = Vortice.Direct3D12.RootSignatureFlags.LocalRootSignature | Vortice.Direct3D12.RootSignatureFlags.ConstantBufferViewShaderResourceViewUnorderedAccessViewHeapDirectlyIndexed;
                device.CreateRootSignature(new Vortice.Direct3D12.RootSignatureDescription1(flags, parameters.ToArray()));
            }
        }

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

    [StructLayout(LayoutKind.Explicit, Size = 28)]
    struct Material
    {
        [FieldOffset(0)]
        public required Vector3 Colour;
    }
}
