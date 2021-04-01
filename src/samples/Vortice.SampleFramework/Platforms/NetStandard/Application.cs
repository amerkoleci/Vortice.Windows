// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Vortice.Win32;
using static Vortice.Win32.Kernel32;
using static Vortice.Win32.User32;

namespace Vortice
{
    public abstract partial class Application : IDisposable
    {
        public const string WindowClassName = "VorticeWindow";
        public readonly IntPtr HInstance = GetModuleHandle(null);

        private unsafe void PlatformConstruct()
        {
            fixed (char* lpszClassName = WindowClassName)
            {
                var wndClassEx = new WNDCLASSEX
                {
                    Size = Unsafe.SizeOf<WNDCLASSEX>(),
                    Styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW | WindowClassStyles.CS_OWNDC,
                    WindowProc = &ProcessWindowMessage,
                    InstanceHandle = HInstance,
                    CursorHandle = LoadCursorW(IntPtr.Zero, IDC_ARROW),
                    BackgroundBrushHandle = IntPtr.Zero,
                    IconHandle = IntPtr.Zero,
                    ClassName = (ushort*)lpszClassName
                };

                ushort atom = RegisterClassExW(&wndClassEx);

                if (atom == 0)
                {
                    throw new InvalidOperationException(
                        $"Failed to register window class. Error: {Marshal.GetLastWin32Error()}"
                        );
                }
            }

            // Create main window.
            MainWindow = new Window("Vortice", 800, 600);
        }

        private unsafe void PlatformRun()
        {
            InitializeBeforeRun();

            Message msg;

            while (!_exitRequested)
            {
                if (!_paused)
                {
                    const uint PM_REMOVE = 1;
                    if (PeekMessageW(&msg, IntPtr.Zero, 0, 0, PM_REMOVE) != 0)
                    {
                        TranslateMessage(&msg);
                        DispatchMessageW(&msg);

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
                    var ret = GetMessageW(&msg, IntPtr.Zero, 0, 0);
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
                        TranslateMessage(&msg);
                        DispatchMessageW(&msg);
                    }
                }
            }
        }

        [UnmanagedCallersOnly]
        private static nint ProcessWindowMessage(IntPtr hWnd, uint message, nuint wParam, nint lParam)
        {
            if (message == (uint)WindowMessage.ActivateApp)
            {
                if (wParam != 0)
                {
                    Application.Current?.OnActivated();
                }
                else
                {
                    Application.Current?.OnDeactivated();
                }

                return DefWindowProcW(hWnd, message, wParam, lParam);
            }

            switch ((WindowMessage)message)
            {
                case WindowMessage.Destroy:
                    PostQuitMessage(0);
                    break;
            }

            return DefWindowProcW(hWnd, message, wParam, lParam);
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
