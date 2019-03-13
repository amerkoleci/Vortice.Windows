// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Vortice;
using Vortice.Direct3D;
using Vortice.Direct3D11;
using Vortice.Direct3D12;
using Vortice.DXGI;
using Vortice.Win32;
using static Vortice.Win32.Kernel32;
using static Vortice.Win32.User32;
using static Vortice.DXGI.DXGI;
using static Vortice.Direct3D12.D3D12;

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
        private readonly int _rtvDescriptorSize;
        private readonly ID3D12Resource[] _renderTargets;
        private readonly ID3D12CommandAllocator _commandAllocator;
        private readonly ID3D12GraphicsCommandList _commandList;
        private readonly ID3D12Fence _d3d12Fence;
        private ulong _fenceValue;
        private readonly AutoResetEvent _fenceEvent;
        private int _frameIndex;

        public IDXGIFactory2 DXGIFactory => _dxgiFactory;
        public ID3D12Device D3D12Device => _d3d12Device;
        public ID3D11Device D3D11Device => _d3d11Device;
        public IDXGISwapChain1 SwapChain { get; }
        public IDXGISwapChain3 SwapChain3 { get; }

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
                if (D3D12GetDebugInterface<ID3D12Debug>(out var debug).Success)
                {
                    debug.EnableDebugLayer();
                    debugFactory = true;
                }
            }
#endif

            if (useDirect3D12)
            {
                if (CreateDXGIFactory2(debugFactory, out IDXGIFactory4 dxgiFactory4).Failure)
                {
                    throw new InvalidOperationException("Cannot create IDXGIFactory4");
                }

                _dxgiFactory = dxgiFactory4;
            }
            else
            {
                if (CreateDXGIFactory2(debugFactory, out _dxgiFactory).Failure)
                {
                    throw new InvalidOperationException("Cannot create IDXGIFactory4");
                }
            }

            if (useDirect3D12)
            {
                Debug.Assert(D3D12CreateDevice(null, FeatureLevel.Level_11_0, out _d3d12Device).Success);

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

            if (useDirect3D12)
            {
                SwapChain3 = SwapChain.QueryInterface<IDXGISwapChain3>();
                _frameIndex = SwapChain3.GetCurrentBackBufferIndex();
            }

            _rtvHeap = _d3d12Device.CreateDescriptorHeap(new DescriptorHeapDescription(DescriptorHeapType.RenderTargetView, FrameCount));
            _rtvDescriptorSize = _d3d12Device.GetDescriptorHandleIncrementSize(DescriptorHeapType.RenderTargetView);

            // Create frame resources.
            {
                var rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();

                // Create a RTV for each frame.
                _renderTargets = new ID3D12Resource[FrameCount];
                for (var i = 0; i < FrameCount; i++)
                {
                    _renderTargets[i] = SwapChain.GetBuffer<ID3D12Resource>(i);
                    _d3d12Device.CreateRenderTargetView(_renderTargets[i], null, rtvHandle);
                    rtvHandle += _rtvDescriptorSize;
                }
            }

            _commandAllocator = _d3d12Device.CreateCommandAllocator(CommandListType.Direct);
            _commandList = _d3d12Device.CreateCommandList(CommandListType.Direct, _commandAllocator);
            _commandList.Close();

            // Create synchronization objects.
            _d3d12Fence = _d3d12Device.CreateFence(0);
            _fenceValue = 1;
            _fenceEvent = new AutoResetEvent(false);
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
            _commandAllocator.Reset();
            _commandList.Reset(_commandAllocator, null);

            // Indicate that the back buffer will be used as a render target.
            _commandList.ResourceBarrierTransition(_renderTargets[_frameIndex], ResourceStates.Present, ResourceStates.RenderTarget);

            var rtvHandle = _rtvHeap.GetCPUDescriptorHandleForHeapStart();
            rtvHandle += _frameIndex * _rtvDescriptorSize;

            // Record commands.
            Color4 clearColor = new Color4(0.0f, 0.2f, 0.4f, 1.0f);
            _commandList.ClearRenderTargetView(rtvHandle, clearColor);

            // Indicate that the back buffer will now be used to present.
            _commandList.ResourceBarrierTransition(_renderTargets[_frameIndex], ResourceStates.RenderTarget, ResourceStates.Present);
            _commandList.Close();

            // Execute the command list.
            _d3d12CommandQueue.ExecuteCommandList(_commandList);

            var result = SwapChain.Present(1, PresentFlags.None);
            if (result.Failure
                && result.Code == Vortice.DXGI.ResultCode.DeviceRemoved.Code)
            {
            }

            WaitForPreviousFrame();
        }

        private void WaitForPreviousFrame()
        {
            // WAITING FOR THE FRAME TO COMPLETE BEFORE CONTINUING IS NOT BEST PRACTICE.
            // This is code implemented as such for simplicity. The D3D12HelloFrameBuffering
            // sample illustrates how to use fences for efficient resource usage and to
            // maximize GPU utilization.

            // Signal and increment the fence value.
            var fenceValueToSignal = _fenceValue;
            _d3d12CommandQueue.Signal(_d3d12Fence, fenceValueToSignal);
            _fenceValue++;

            // Wait until the previous frame is finished.
            if (_d3d12Fence.CompletedValue < fenceValueToSignal)
            {
                _d3d12Fence.SetEventOnCompletion(fenceValueToSignal, _fenceEvent);
                _fenceEvent.WaitOne();
            }

            _frameIndex = SwapChain3.GetCurrentBackBufferIndex();
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
