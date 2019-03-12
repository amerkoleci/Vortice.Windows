// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vortice;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.Direct3D12;
using Vortice.DXGI;
using Vortice.Win32;
using static Vortice.Win32.Kernel32;
using static Vortice.Win32.User32;

namespace HelloDirect3D11
{
    public abstract class Application : IDisposable
    {
        public static readonly string WndClassName = "VorticeWindow";
        public readonly IntPtr HInstance = GetModuleHandle(null);
        private readonly WNDPROC _wndProc;
        private bool _paused;
        private bool _exitRequested;

        private const int FrameCount = 2;

        private readonly IDXGIFactory2 _dxgiFactory;
        private readonly ID3D12Device _d3d12Device;
        private readonly ID3D12CommandQueue _d3d12CommandQueue;
        private readonly ID3D11Device _d3d11Device;
        private readonly ID3D11DeviceContext _d3d11DeviceContext;
        private readonly ID3D12DescriptorHeap _rtvHeap;

        public IDXGIFactory2 DXGIFactory => _dxgiFactory;
        public ID3D12Device D3D12Device => _d3d12Device;
        public ID3D11Device D3D11Device => _d3d11Device;
        public IDXGISwapChain1 SwapChain { get; }

        public readonly Window Window;

        protected Application(bool useDirect3D12)
        {
            _wndProc = ProcessWindowMessage;
            var wndClassEx = new WNDCLASSEX
            {
                Size = Unsafe.SizeOf<WNDCLASSEX>(),
                Styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW | WindowClassStyles.CS_OWNDC,
                WindowProc = _wndProc,
                InstanceHandle = HInstance,
                CursorHandle = LoadCursor(IntPtr.Zero, SystemCursor.IDC_ARROW),
                BackgroundBrushHandle = IntPtr.Zero,
                IconHandle = IntPtr.Zero,
                ClassName = WndClassName,
            };

            var atom = RegisterClassEx(ref wndClassEx);

            if (atom == 0)
            {
                throw new InvalidOperationException(
                    $"Failed to register window class. Error: {Marshal.GetLastWin32Error()}"
                    );
            }

            Window = new Window("Vortice", 800, 600);

            if (useDirect3D12
                && !ID3D12Device.IsSupported(null, FeatureLevel.Level_11_0))
            {
                useDirect3D12 = false;
            }

            var debugFactory = false;
#if DEBUG
            if (useDirect3D12)
            {
                if (ID3D12Debug.TryCreate(out var debug).Success)
                {
                    debug.EnableDebugLayer();
                    debugFactory = true;
                }
            }
#endif

            if (useDirect3D12)
            {
                if (IDXGIFactory4.TryCreate(debugFactory, out var dxgiFactory4).Failure)
                {
                    throw new InvalidOperationException("Cannot create IDXGIFactory4");
                }
                _dxgiFactory = dxgiFactory4;
            }
            else
            {
                if (IDXGIFactory2.TryCreate(debugFactory, out _dxgiFactory).Failure)
                {
                    throw new InvalidOperationException("Cannot create IDXGIFactory4");
                }
            }

            if (useDirect3D12)
            {
                Debug.Assert(ID3D12Device.TryCreate(null, FeatureLevel.Level_11_0, out _d3d12Device).Success);

                _d3d12CommandQueue = _d3d12Device.CreateCommandQueue(new CommandQueueDescription(CommandListType.Direct, CommandQueuePriority.Normal));
            }
            else
            {
                var featureLevels = new FeatureLevel[]
                {
                    FeatureLevel.Level_11_1,
                    FeatureLevel.Level_11_0
                };
                Debug.Assert(ID3D11Device.TryCreate(
                    null,
                    DriverType.Hardware,
                    DeviceCreationFlags.BgraSupport,
                    featureLevels,
                    out _d3d11Device,
                    out _d3d11DeviceContext).Success);
            }

            var swapChainDesc = new SwapChainDescription1
            {
                BufferCount = FrameCount,
                Width = Window.Width,
                Height = Window.Height,
                Format = Format.B8G8R8A8_UNorm,
                Usage = Vortice.Usage.RenderTargetOutput,
                SwapEffect = SwapEffect.FlipDiscard,
                SampleDescription = new SampleDescription(1, 0)
            };

            SwapChain = DXGIFactory.CreateSwapChainForHwnd(_d3d12CommandQueue, Window.Handle, swapChainDesc);
            DXGIFactory.MakeWindowAssociation(Window.Handle, WindowAssociationFlags.IgnoreAltEnter);

            _rtvHeap = _d3d12Device.CreateDescriptorHeap(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, FrameCount));
            var _commandAllocator = _d3d12Device.CreateCommandAllocator(CommandListType.Direct);
            var _commandList = _d3d12Device.CreateCommandList(CommandListType.Direct, _commandAllocator);
            _commandList.Close();
        }

        public void Dispose()
        {
            _d3d11DeviceContext?.Dispose();
            _d3d11Device?.Dispose();
            _d3d12Device?.Dispose();
            _dxgiFactory.Dispose();
        }

        public void Tick()
        {
            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code)
            {
            }
        }

        public void Run()
        {
            while (!_exitRequested)
            {
                if (!_paused)
                {
                    const uint PM_REMOVE = 1;
                    if (PeekMessage(out var msg, IntPtr.Zero, 0, 0, PM_REMOVE))
                    {
                        TranslateMessage(ref msg);
                        DispatchMessage(ref msg);

                        if (msg.Value == (uint)WindowMessage.Quit)
                        {
                            _exitRequested = true;
                            break;
                        }
                    }

                    Tick();
                }
                else
                {
                    var ret = GetMessage(out var msg, IntPtr.Zero, 0, 0);
                    if (ret == 0)
                    {
                        _exitRequested = true;
                        break;
                    }
                    else if (ret == -1)
                    {
                        //Log.Error("[Win32] - Failed to get message");
                        _exitRequested = true;
                        break;
                    }
                    else
                    {
                        TranslateMessage(ref msg);
                        DispatchMessage(ref msg);
                    }
                }
            }
        }

        protected virtual void OnActivated()
        {
        }

        protected virtual void OnDeactivated()
        {
        }

        private IntPtr ProcessWindowMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == (uint)WindowMessage.ActivateApp)
            {
                _paused = IntPtrToInt32(wParam) == 0;
                if (IntPtrToInt32(wParam) != 0)
                {
                    OnActivated();
                }
                else
                {
                    OnDeactivated();
                }

                return DefWindowProc(hWnd, msg, wParam, lParam);
            }

            switch ((WindowMessage)msg)
            {
                case WindowMessage.Destroy:
                    PostQuitMessage(0);
                    break;
            }

            return DefWindowProc(hWnd, msg, wParam, lParam);
        }

        private static int SignedLOWORD(int n)
        {
            return (short)(n & 0xFFFF);
        }

        private static int SignedHIWORD(int n)
        {
            return (short)(n >> 16 & 0xFFFF);
        }

        private static int SignedLOWORD(IntPtr intPtr)
        {
            return SignedLOWORD(IntPtrToInt32(intPtr));
        }

        private static int SignedHIWORD(IntPtr intPtr)
        {
            return SignedHIWORD(IntPtrToInt32(intPtr));
        }

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return (int)intPtr.ToInt64();
        }

        private static Point MakePoint(IntPtr lparam)
        {
            var lp = lparam.ToInt32();
            var x = lp & 0xff;
            var y = (lp >> 16) & 0xff;
            return new Point(x, y);
        }
    }
}
