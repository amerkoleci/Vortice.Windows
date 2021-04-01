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

            if (!Headless)
            {
                // Create main window.
                MainWindow = new Window("Vortice", 800, 600);
            }
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
                        _ = TranslateMessage(&msg);
                        _ = DispatchMessageW(&msg);

                        if (msg.Value == WM_QUIT)
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
                        _ = TranslateMessage(&msg);
                        _ = DispatchMessageW(&msg);
                    }
                }
            }
        }

        [UnmanagedCallersOnly]
        private static nint ProcessWindowMessage(IntPtr hWnd, uint message, nuint wParam, nint lParam)
        {
            if (message == WM_ACTIVATEAPP)
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

            switch (message)
            {
                case WM_KEYDOWN:
                case WM_KEYUP:
                case WM_SYSKEYDOWN:
                case WM_SYSKEYUP:
                    OnKey(message, wParam, lParam);
                    break;

                case WM_DESTROY:
                    PostQuitMessage(0);
                    break;
            }

            return DefWindowProcW(hWnd, message, wParam, lParam);
        }

        private static void OnKey(uint message, nuint wParam, nint lParam)
        {
            if (message == WM_KEYDOWN || message == WM_SYSKEYDOWN)
                Current?.OnKeyboardEvent(ConvertKeyCode(lParam, wParam), true);
            else if (message == WM_KEYUP || message == WM_SYSKEYUP)
                Current?.OnKeyboardEvent(ConvertKeyCode(lParam, wParam), false);
        }

        private static KeyboardKey ConvertKeyCode(nint lParam, nuint wParam)
        {
            switch (wParam)
            {
                // virtual key codes
                case VK_CLEAR: return KeyboardKey.clear;
                case VK_MODECHANGE: return KeyboardKey.modeChange;
                case VK_SELECT: return KeyboardKey.select;
                case VK_EXECUTE: return KeyboardKey.execute;
                case VK_HELP: return KeyboardKey.help;
                case VK_PAUSE: return KeyboardKey.pause;
                case VK_NUMLOCK: return KeyboardKey.numLock;

                case VK_F13: return KeyboardKey.f13;
                case VK_F14: return KeyboardKey.f14;
                case VK_F15: return KeyboardKey.f15;
                case VK_F16: return KeyboardKey.f16;
                case VK_F17: return KeyboardKey.f17;
                case VK_F18: return KeyboardKey.f18;
                case VK_F19: return KeyboardKey.f19;
                case VK_F20: return KeyboardKey.f20;
                case VK_F21: return KeyboardKey.f21;
                case VK_F22: return KeyboardKey.f22;
                case VK_F23: return KeyboardKey.f23;
                case VK_F24: return KeyboardKey.f24;

                case VK_OEM_NEC_EQUAL: return KeyboardKey.numpadEqual;
                case VK_BROWSER_BACK: return KeyboardKey.back;
                case VK_BROWSER_FORWARD: return KeyboardKey.forward;
                case VK_BROWSER_REFRESH: return KeyboardKey.refresh;
                case VK_BROWSER_STOP: return KeyboardKey.stop;
                case VK_BROWSER_SEARCH: return KeyboardKey.search;
                case VK_BROWSER_FAVORITES: return KeyboardKey.bookmarks;
                case VK_BROWSER_HOME: return KeyboardKey.home;
                case VK_VOLUME_MUTE: return KeyboardKey.mute;
                case VK_VOLUME_DOWN: return KeyboardKey.volumeDown;
                case VK_VOLUME_UP: return KeyboardKey.volumeUp;

                case VK_MEDIA_NEXT_TRACK: return KeyboardKey.audioNext;
                case VK_MEDIA_PREV_TRACK: return KeyboardKey.audioPrevious;
                case VK_MEDIA_STOP: return KeyboardKey.audioStop;
                case VK_MEDIA_PLAY_PAUSE: return KeyboardKey.audioPlay;
                case VK_LAUNCH_MAIL: return KeyboardKey.mail;
                case VK_LAUNCH_MEDIA_SELECT: return KeyboardKey.mediaSelect;

                case VK_OEM_102: return KeyboardKey.intlBackslash;

                case VK_ATTN: return KeyboardKey.printScreen;
                case VK_CRSEL: return KeyboardKey.crsel;
                case VK_EXSEL: return KeyboardKey.exsel;
                case VK_OEM_CLEAR: return KeyboardKey.clear;

                case VK_LAUNCH_APP1: return KeyboardKey.app1;
                case VK_LAUNCH_APP2: return KeyboardKey.app2;

                // scan codes
                default:
                    {
                        nint scanCode = (lParam >> 16) & 0xFF;
                        if (scanCode <= 127)
                        {
                            bool isExtended = (lParam & (1 << 24)) != 0;

                            switch (scanCode)
                            {
                                case 0x01: return KeyboardKey.escape;
                                case 0x02: return KeyboardKey.num1;
                                case 0x03: return KeyboardKey.num2;
                                case 0x04: return KeyboardKey.num3;
                                case 0x05: return KeyboardKey.num4;
                                case 0x06: return KeyboardKey.num5;
                                case 0x07: return KeyboardKey.num6;
                                case 0x08: return KeyboardKey.num7;
                                case 0x09: return KeyboardKey.num8;
                                case 0x0A: return KeyboardKey.num9;
                                case 0x0B: return KeyboardKey.num0;
                                case 0x0C: return KeyboardKey.minus;
                                case 0x0D: return KeyboardKey.equal;
                                case 0x0E: return KeyboardKey.Backspace;
                                case 0x0F: return KeyboardKey.Tab;
                                case 0x10: return KeyboardKey.Q;
                                case 0x11: return KeyboardKey.W;
                                case 0x12: return KeyboardKey.e;
                                case 0x13: return KeyboardKey.R;
                                case 0x14: return KeyboardKey.T;
                                case 0x15: return KeyboardKey.Y;
                                case 0x16: return KeyboardKey.U;
                                case 0x17: return KeyboardKey.i;
                                case 0x18: return KeyboardKey.O;
                                case 0x19: return KeyboardKey.P;
                                case 0x1A: return KeyboardKey.leftBracket;
                                case 0x1B: return KeyboardKey.rightBracket;
                                case 0x1C: return isExtended ? KeyboardKey.numpadEnter : KeyboardKey.enter;
                                case 0x1D: return isExtended ? KeyboardKey.rightControl : KeyboardKey.leftControl;
                                case 0x1E: return KeyboardKey.a;
                                case 0x1F: return KeyboardKey.S;
                                case 0x20: return KeyboardKey.d;
                                case 0x21: return KeyboardKey.f;
                                case 0x22: return KeyboardKey.g;
                                case 0x23: return KeyboardKey.h;
                                case 0x24: return KeyboardKey.j;
                                case 0x25: return KeyboardKey.k;
                                case 0x26: return KeyboardKey.l;
                                case 0x27: return KeyboardKey.semicolon;
                                case 0x28: return KeyboardKey.quote;
                                case 0x29: return KeyboardKey.grave;
                                case 0x2A: return KeyboardKey.leftShift;
                                case 0x2B: return KeyboardKey.backslash;
                                case 0x2C: return KeyboardKey.Z;
                                case 0x2D: return KeyboardKey.X;
                                case 0x2E: return KeyboardKey.c;
                                case 0x2F: return KeyboardKey.V;
                                case 0x30: return KeyboardKey.b;
                                case 0x31: return KeyboardKey.n;
                                case 0x32: return KeyboardKey.m;
                                case 0x33: return KeyboardKey.comma;
                                case 0x34: return KeyboardKey.period;
                                case 0x35: return isExtended ? KeyboardKey.numpadDivide : KeyboardKey.slash;
                                case 0x36: return KeyboardKey.rightShift;
                                case 0x37: return isExtended ? KeyboardKey.printScreen : KeyboardKey.numpadMultiply;
                                case 0x38: return isExtended ? KeyboardKey.rightAlt : KeyboardKey.leftAlt;
                                case 0x39: return KeyboardKey.space;
                                case 0x3A: return isExtended ? KeyboardKey.numpadPlus : KeyboardKey.capsLock;
                                case 0x3B: return KeyboardKey.f1;
                                case 0x3C: return KeyboardKey.f2;
                                case 0x3D: return KeyboardKey.f3;
                                case 0x3E: return KeyboardKey.f4;
                                case 0x3F: return KeyboardKey.f5;
                                case 0x40: return KeyboardKey.f6;
                                case 0x41: return KeyboardKey.f7;
                                case 0x42: return KeyboardKey.f8;
                                case 0x43: return KeyboardKey.f9;
                                case 0x44: return KeyboardKey.f10;
                                case 0x45: return KeyboardKey.numLock;
                                case 0x46: return KeyboardKey.scrollLock;
                                case 0x47: return isExtended ? KeyboardKey.home : KeyboardKey.numpad7;
                                case 0x48: return isExtended ? KeyboardKey.up : KeyboardKey.numpad8;
                                case 0x49: return isExtended ? KeyboardKey.pageUp : KeyboardKey.numpad9;
                                case 0x4A: return KeyboardKey.numpadMinus;
                                case 0x4B: return isExtended ? KeyboardKey.left : KeyboardKey.numpad4;
                                case 0x4C: return KeyboardKey.numpad5;
                                case 0x4D: return isExtended ? KeyboardKey.right : KeyboardKey.numpad6;
                                case 0x4E: return KeyboardKey.numpadPlus;
                                case 0x4F: return isExtended ? KeyboardKey.end : KeyboardKey.numpad1;
                                case 0x50: return isExtended ? KeyboardKey.down : KeyboardKey.numpad2;
                                case 0x51: return isExtended ? KeyboardKey.pageDown : KeyboardKey.numpad3;
                                case 0x52: return isExtended ? KeyboardKey.insert : KeyboardKey.numpad0;
                                case 0x53: return isExtended ? KeyboardKey.del : KeyboardKey.numpadDecimal;
                                case 0x54: return KeyboardKey.None;
                                case 0x55: return KeyboardKey.None;
                                case 0x56: return KeyboardKey.intlBackslash;
                                case 0x57: return KeyboardKey.f11;
                                case 0x58: return KeyboardKey.f12;
                                case 0x59: return KeyboardKey.pause;
                                case 0x5A: return KeyboardKey.None;
                                case 0x5B: return KeyboardKey.leftSuper;
                                case 0x5C: return KeyboardKey.rightSuper;
                                case 0x5D: return KeyboardKey.menu;
                                case 0x5E: return KeyboardKey.None;
                                case 0x5F: return KeyboardKey.None;
                                case 0x60: return KeyboardKey.None;
                                case 0x61: return KeyboardKey.None;
                                case 0x62: return KeyboardKey.None;
                                case 0x63: return KeyboardKey.None;
                                case 0x64: return KeyboardKey.f13;
                                case 0x65: return KeyboardKey.f14;
                                case 0x66: return KeyboardKey.f15;
                                case 0x67: return KeyboardKey.f16;
                                case 0x68: return KeyboardKey.f17;
                                case 0x69: return KeyboardKey.f18;
                                case 0x6A: return KeyboardKey.f19;
                                case 0x6B: return KeyboardKey.None;
                                case 0x6C: return KeyboardKey.None;
                                case 0x6D: return KeyboardKey.None;
                                case 0x6E: return KeyboardKey.None;
                                case 0x6F: return KeyboardKey.None;
                                case 0x70: return KeyboardKey.katakanaHiragana;
                                case 0x71: return KeyboardKey.None;
                                case 0x72: return KeyboardKey.None;
                                case 0x73: return KeyboardKey.ro;
                                case 0x74: return KeyboardKey.None;
                                case 0x75: return KeyboardKey.None;
                                case 0x76: return KeyboardKey.None;
                                case 0x77: return KeyboardKey.None;
                                case 0x78: return KeyboardKey.None;
                                case 0x79: return KeyboardKey.henkan;
                                case 0x7A: return KeyboardKey.None;
                                case 0x7B: return KeyboardKey.muhenkan;
                                case 0x7C: return KeyboardKey.None;
                                case 0x7D: return KeyboardKey.yen;
                                case 0x7E: return KeyboardKey.None;
                                case 0x7F: return KeyboardKey.None;
                                default: return KeyboardKey.None;
                            }
                        }
                        else
                            return KeyboardKey.None;
                    }
            }
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
